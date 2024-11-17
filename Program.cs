// Dynamic DNS Record Updater for Cloudflare
// License: MIT license
// This tool was created to automatically update DNS records.
// Created by ChatGPT and Ryan Scott White 

// Instructions:
//   1. create an apiToken with the required permissions 
//   2. set the zoneID, apiToken, domains list, and frequency in config.json

using Microsoft.Extensions.Logging;  // Install-Package Microsoft.Extensions.Logging.Console
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // Install-Package Newtonsoft.Json
using System.Net.Http.Headers;
using System.Text;

public class Configuration
{
    public required string ZoneId { get; set; }
    public required string ApiToken { get; set; }
    public required List<string> Domains { get; set; }
    public int FrequencyToCheckInMinutes { get; set; }
}

public class Program
{
    private static ILogger<Program>? _logger;

    public static async Task Main(string[] args)
    {
        // Setup logging
        using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            _ = builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "hh:mm:ss ";
            });
            _ = builder.SetMinimumLevel(LogLevel.Information);
        });
        _logger = loggerFactory.CreateLogger<Program>();

        // Parse command-line arguments to get the configuration file path
        string configFilePath = "config.json"; // Default config file name
        if (args.Length > 0)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-config", StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
                {
                    configFilePath = args[i + 1];
                    break;
                }
            }
        }

        // Load configuration from file
        Configuration? config = LoadConfiguration(configFilePath);

        // Validate configuration
        if (config == null || !ValidateConfiguration(config))
        {
            _logger?.LogError("Configuration is missing or invalid. Exiting application.");
            return;
        }

        // Override ApiToken with environment variable if set
        string? envApiToken = Environment.GetEnvironmentVariable("CF_API_TOKEN");
        if (!string.IsNullOrEmpty(envApiToken))
        {
            config.ApiToken = envApiToken;
            _logger?.LogInformation("ApiToken loaded from environment variable.");
        }

        // Main loop
        string lastExternalIP = "";
        while (true)
        {
            if (lastExternalIP != "")
            {
                _logger?.LogInformation("Waiting {Minutes} minutes.", config.FrequencyToCheckInMinutes);
                await Task.Delay(TimeSpan.FromMinutes(config.FrequencyToCheckInMinutes));
            }

            using HttpClient client = new();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.ApiToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Get external IP address
            string newExternalIP = await GetExternalIPAddressAsync();
            if (string.IsNullOrEmpty(newExternalIP))
            {
                _logger?.LogWarning("Failed to get external IP address.");
                continue;
            }

            // Check if IP has changed
            if (lastExternalIP == newExternalIP)
            {
                _logger?.LogInformation("No changes detected in external IP ({IP}).", lastExternalIP);
                continue;
            }
            lastExternalIP = newExternalIP;

            // Check each domain
            foreach (string domain in config.Domains)
            {
                _logger?.LogInformation("Checking {Domain} to ensure it matches {IP}.", domain, newExternalIP);

                string dnsRecordId = await GetDnsRecordIdByNameAsync(client, config.ZoneId, domain);
                if (string.IsNullOrEmpty(dnsRecordId))
                {
                    _logger?.LogWarning("Failed to get DNS Record ID for domain ({Domain}).", domain);
                    continue;
                }

                string dnsRecordIp = await GetCloudflareDnsRecordIpAsync(client, config.ZoneId, dnsRecordId);

                if (string.IsNullOrEmpty(dnsRecordIp))
                {
                    _logger?.LogWarning("Failed to get IP address from DNS Record ID.");
                    continue;
                }

                if (newExternalIP != dnsRecordIp)
                {
                    _logger?.LogInformation("IP addresses do not match. Updating DNS record...");
                    _logger?.LogInformation("DNS Record IP: {DnsRecordIp}", dnsRecordIp);
                    _logger?.LogInformation("External IP: {ExternalIp}", newExternalIP);

                    bool updateSuccess = await UpdateCloudflareDnsRecordAsync(client, config.ZoneId, dnsRecordId, newExternalIP, domain);

                    _logger?.LogInformation("{Domain} DNS record update Success: {UpdateSuccess}.", domain, updateSuccess);
                }
                else
                {
                    _logger?.LogInformation("{Domain} IP address matches. No update needed.", domain);
                }
            }
        }
    }

    private static Configuration? LoadConfiguration(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                string jsonConfig = File.ReadAllText(filePath);
                if (JsonConvert.DeserializeObject<Configuration>(jsonConfig) is not Configuration config)
                {
                    _logger?.LogError("Error reading 'config.json'.");
                    return null;
                }

                // Set default frequency if not specified
                if (config.FrequencyToCheckInMinutes <= 0)
                {
                    config.FrequencyToCheckInMinutes = 120; // Default to 120 minutes
                }

                return config;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error reading 'config.json'.");
                return null;
            }
        }
        else
        {
            _logger?.LogError("Configuration file not found: {FilePath}", filePath);
            return null;
        }
    }

    private static bool ValidateConfiguration(Configuration config)
    {
        bool isValid = true;

        if (string.IsNullOrEmpty(config.ZoneId))
        {
            _logger?.LogError("ZoneId is missing in 'config.json'.");
            isValid = false;
        }

        if (config.ZoneId == "123abc123abc123abc123abc123abc12")
        {
            _logger?.LogError("The 'config.json' has not been updated yet. Please add your ZoneId from the Cloudflare dashboard.");
            isValid = false;
        }

        if (config.ZoneId.Length != 32)
        {
            _logger?.LogError("The ZoneId field in the 'config.json' does not appear correct. It should be a 32 digit hexadecimal digits. Please add your ZoneId from the Cloudflare dashboard.");
            isValid = false;
        }

        if (string.IsNullOrEmpty(config.ApiToken))
        {
            _logger?.LogError("ApiToken is missing in 'config.json'.");
            isValid = false;
        }

        if (config.ApiToken == "123abc123abc123abc123abc123abc123abc123a")
        {
            _logger?.LogError("The 'config.json' has not been updated yet. Please generate the correct Cloudflare API token.");
            isValid = false;
        }

        if (config.ApiToken.Length != 40)
        {
            _logger?.LogError("The ZoneId field in the 'config.json' does not appear correct. It should be a 40 digit hexadecimal digits. Please generate the correct Cloudflare API token.");
            isValid = false;
        }

        if (config.Domains == null || config.Domains.Count == 0)
        {
            _logger?.LogError("Domains list is missing or empty in 'config.json'. There should be at least one domain.");
            isValid = false;
        }

        return isValid;
    }

    private static async Task<string> GetExternalIPAddressAsync()
    {
        using HttpClient client = new();
        try
        {
            string ipAddress = (await client.GetStringAsync("https://api.ipify.org")).Trim();
            bool isValidIP = System.Net.IPAddress.TryParse(ipAddress, out System.Net.IPAddress? ip) && ip.ToString() == ipAddress;
            if (!isValidIP)
            {
                _logger?.LogWarning("Invalid IP address received: {IPAddress}", ipAddress);
                ipAddress = "";
            }

            return ipAddress;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error getting external IP address.");
            return "";
        }
    }

    private static async Task<string> GetDnsRecordIdByNameAsync(HttpClient client, string zoneId, string recordName)
    {
        string url = $"https://api.cloudflare.com/client/v4/zones/{zoneId}/dns_records?type=A&name={Uri.EscapeDataString(recordName)}";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogError("Error fetching DNS Record ID. Status Code: {StatusCode}", response.StatusCode);
                _logger?.LogError("Response Content: {Content}", content);
                return "";
            }

            JObject dnsRecords = JObject.Parse(content);
            JArray? resultArray = (JArray?)dnsRecords["result"];

            if (resultArray != null && resultArray.Count > 0)
            {
                JToken dnsRecord = resultArray[0];
                string dnsRecordId = dnsRecord["id"]?.ToString() ?? "";
                return dnsRecordId;
            }

            _logger?.LogWarning("DNS record not found for {RecordName}.", recordName);
            return "";
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error retrieving DNS Record ID.");
            return "";
        }
    }

    private static async Task<string> GetCloudflareDnsRecordIpAsync(HttpClient client, string zoneId, string dnsRecordId)
    {
        string url = $"https://api.cloudflare.com/client/v4/zones/{zoneId}/dns_records/{dnsRecordId}";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogError("Error fetching DNS Record IP. Status Code: {StatusCode}", response.StatusCode);
                _logger?.LogError("Response Content: {Content}", content);
                return "";
            }

            // Parse the JSON response to extract the IP address
            JObject dnsRecord = JObject.Parse(content);
            string currentIp = dnsRecord["result"]?["content"]?.ToString() ?? "";

            return currentIp;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error retrieving DNS Record IP.");
            return "";
        }
    }

    private static async Task<bool> UpdateCloudflareDnsRecordAsync(HttpClient client, string zoneId, string dnsRecordId, string newIp, string domain)
    {
        string url = $"https://api.cloudflare.com/client/v4/zones/{zoneId}/dns_records/{dnsRecordId}";

        var payload = new
        {
            type = "A",
            name = domain,
            content = newIp,
            ttl = 3600,
            proxied = true,
            comment = "Dynamic DNS update"
        };

        string jsonPayload = JsonConvert.SerializeObject(payload);

        StringContent requestContent = new(jsonPayload, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PutAsync(url, requestContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogError("Error updating DNS record. Status Code: {StatusCode}", response.StatusCode);
                _logger?.LogError("Response Content: {Content}", responseContent);
                return false;
            }

            // Check if the update was successful
            JObject result = JObject.Parse(responseContent);
            return result["success"]?.ToObject<bool>() ?? false;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error updating DNS record.");
            return false;
        }
    }
}
