Pull request submitted to update Cloudflare-docs (11/10/2024)
=============================================================
https://github.com/cloudflare/cloudflare-docs/pull/18115
Topic: remove 'DNS-O-Matic' that is no longer available and post a replacement I created since there is no direct Cloudflare DDNS solution for windows customers.
Status: Closed by Cloudflare on 11/20/2024 - update request denied - no reason supplied

# Proposed Changes

> Most Internet service providers and some hosting providers dynamically update their customer's IP addresses. If this situation applies to you, you need an automated solution to dynamically update your DNS records in Cloudflare.
> 
> ## Cloudflare API
> 
> Create a script to monitor IP address changes and then have that script push changes to the [Cloudflare API](/api/operations/dns-records-for-a-zone-update-dns-record).
> 
> ## ddclient
> 
> [ddclient](https://github.com/ddclient/ddclient) is a third-party Perl client used to update dynamic DNS entries for accounts on various DNS providers. It works on Linux, macOS, or any other UNIX systems.
> 
> ## Dynamic-DNS-Record-Updater-For-Cloudflare
> 
> This [third-party C# client](https://github.com/SunsetQuest/Dynamic-DNS-Record-Updater-For-CloudFlare/) can update several DNS entries dynamicly for a zone.
> Configuration requires the following information:
> 
> * **Zone Id**: `<CLOUDFLARE ZONE ID>` (associated ZoneID to manage DNS)
> * **API Token**: `<CLOUDFLARE GLOBAL API KEY>` (for details refer to [API Keys](/fundamentals/api/get-started/keys/))
> * **Domains**: `["git.example.com", "example.com"]`
> * **FrequencyToCheckInMinutes**: `120` (Default)

# Original

> Most Internet service providers and some hosting providers dynamically update their customer's IP addresses. If this situation applies to you, you need an automated solution to dynamically update your DNS records in Cloudflare.
> 
> ## Cloudflare API
> 
> Create a script to monitor IP address changes and then have that script push changes to the [Cloudflare API](/api/operations/dns-records-for-a-zone-update-dns-record).
> 
> ## ddclient
> 
> [ddclient](https://github.com/ddclient/ddclient) is a third-party Perl client used to update dynamic DNS entries for accounts on various DNS providers.
> 
> ## DNS-O-Matic
> 
> [DNS-O-Matic](https://dnsomatic.com/docs/) is a third-party tool that announces dynamic IP changes to multiple services.
> 
> Configuration of DNS-O-Matic requires the following information:
> 
> * **Email**: `<CLOUDFLARE ACCOUNT EMAIL ADDRESS>` (associated account must have sufficient privileges to manage DNS)
> * **API Token**: `<CLOUDFLARE GLOBAL API KEY>` (for details refer to [API Keys](/fundamentals/api/get-started/keys/))
> * **Domain**: `<example.com>`
> * **Hostname**: *dynamic*

# Pull Request notes 

>##Update managing-dynamic-ip-addresses.mdx #18115
>
>Sunsetquest
>
>
>
>
>### 
>
>**SunsetQuest** commented [Nov 11, 2024](#issue-2651066840)
>
>Remove 'DNS-O-Matic' as it is no longer available and add 'Dynamic-DNS-Record-Updater-For-Cloudflare'.
>
>This was a Dynamic DNS updater I created because there is nothing for Windows anymore. The existing item is no longer available for download as it was purchased by another company. I think this will make people's lives easier - at least windows users. Thank you.
>
>Here is a possible replacement tool  
>[https://github.com/SunsetQuest/Dynamic-DNS-Record-Updater-For-CloudFlare/blob/master/README.md](https://github.com/SunsetQuest/Dynamic-DNS-Record-Updater-For-CloudFlare/blob/master/README.md)
>
>
>
>SunsetQuest
>
>`[Update managing-dynamic-ip-addresses.mdx](/cloudflare/cloudflare-docs/pull/18115/commits/14fdbfa03569c4f40ce4ad589210c51f84014f33 "Update managing-dynamic-ip-addresses.mdx Remove 'DNS-O-Matic' as it is not longer available. Add 'Dynamic-DNS-Record-Updater-For-CloudFlare'")` …
>
>
>This commit was created on GitHub.com and signed with GitHub’s **verified signature**.
>
>
>[Learn about vigilant mode](https://docs.github.com/github/authenticating-to-github/displaying-verification-statuses-for-all-of-your-commits)
>
>
>
>Loading status checks…
>
>`[14fdbfa](/cloudflare/cloudflare-docs/pull/18115/commits/14fdbfa03569c4f40ce4ad589210c51f84014f33)`
>
>Remove 'DNS-O-Matic' as it is not longer available.
>Add 'Dynamic-DNS-Record-Updater-For-CloudFlare'
>
>SunsetQuest requested review from Rebecca Tamachiro and a team as 'code owners' [November 11, 2024 21:25](#event-15258668322)
>
>github-actions bot added [product:dns](/cloudflare/cloudflare-docs/labels/product%3Adns) Issues or PRs related to DNS [size/s](/cloudflare/cloudflare-docs/labels/size%2Fs) labels [Nov 11, 2024](#event-15258670139)
>
>github-actions bot assigned Rebecca Tamachiro [Nov 11, 2024](#event-15258670247)
>
>hyperlint-ai bot
>
>**hyperlint-ai bot** reviewed [Nov 11, 2024](#pullrequestreview-2428601240)
>
>[View reviewed changes](/cloudflare/cloudflare-docs/pull/18115/files/14fdbfa03569c4f40ce4ad589210c51f84014f33)
>
> 1 files reviewed, 2 total issue(s) found. \_Originally posted by @hyperlint-ai\[bot\] in https://github.com/cloudflare/cloudflare-docs/pull/18115#pullrequestreview-2428601240\_  Attach files by dragging & dropping, selecting or pasting them. Loading Uploading your files… We don’t support that file type. Try again with a GIF, JPEG, JPG, MOV, MP4, PNG, SVG or WEBM. Attaching documents requires write permission to this repository. Try again with a GIF, JPEG, JPG, MOV, MP4, PNG, SVG or WEBM. We don’t support that file type. Try again with a GIF, JPEG, JPG, MOV, MP4, PNG, SVG or WEBM. This file is empty. Try again with a file that’s not empty. This file is hidden. Try again with another file. Something went really wrong, and we can’t process that file. Try again. [](https://docs.github.com/github/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax)Styling with Markdown is supported
>
>
>### 
>
>**hyperlint-ai bot** left a comment
>
>[](#pullrequestreview-2428601240)
>
>
>1 files reviewed, 2 total issue(s) found.
>
>
>
>[src/content/docs/dns/manage-dns-records/how-to/managing-dynamic-ip-addresses.mdx](/cloudflare/cloudflare-docs/pull/18115/files/14fdbfa03569c4f40ce4ad589210c51f84014f33#diff-b0f55097f0dcb76fc345fc29f759b5eb72266a7e1d5fa4350d3b7a32e9dd993f) Outdated Show resolved Hide resolved
>
>
>\## ddclient
>
>  
>
>\[ddclient\](https://github.com/ddclient/ddclient) is a third-party Perl client used to update dynamic DNS entries for accounts on various DNS providers. It works on Linux, macOS, or any other Unix systems.
>
>
>### 
>
>**hyperlint-ai bot** [Nov 11, 2024](#discussion_r1837500215)
>
>
>\[ddclient\](https://github.com/ddclient/ddclient) is a third-party Perl client used to update dynamic DNS entries for accounts on various DNS providers. It works on Linux, macOS, or any other UNIX systems.
>
>Suggestion applied Commit suggestion
>
>**Issues:**
>
>*   Style Guide - (Terms-error) Use 'UNIX' instead of 'Unix'.
>
>**Fix Explanation:**
>
>The term 'UNIX' should be capitalized as per the style guide to maintain consistency and adhere to standard conventions in technical writing.
>
>
>#### An unexpected error has occurred
>
>**SunsetQuest** marked this conversation as resolved.
>
>[src/content/docs/dns/manage-dns-records/how-to/managing-dynamic-ip-addresses.mdx](/cloudflare/cloudflare-docs/pull/18115/files/14fdbfa03569c4f40ce4ad589210c51f84014f33#diff-b0f55097f0dcb76fc345fc29f759b5eb72266a7e1d5fa4350d3b7a32e9dd993f) Outdated Show resolved Hide resolved
>
>  
>
>\## DNS-O-Matic
>
>\## Dynamic-DNS-Record-Updater-For-CloudFlare
>
>
>### 
>
>**hyperlint-ai bot** [Nov 11, 2024](#discussion_r1837500216)
>
>
>\## Dynamic-DNS-Record-Updater-For-Cloudflare
>
>Suggestion applied Commit suggestion
>
>**Issues:**
>
>*   Style Guide - (Terms-error) Use 'Cloudflare' instead of 'CloudFlare'.
>
>**Fix Explanation:**
>
>The term 'CloudFlare' should be corrected to 'Cloudflare' to adhere to the company's official branding and our style guide. This is a straightforward spelling correction.
>
>
>
>#### An unexpected error has occurred
>
>
>**SunsetQuest** marked this conversation as resolved.
>
>SunsetQuest and others added 2 commits [November 13, 2024 20:37](#commits-pushed-4504b2e)
>
>SunsetQuest
>
>`[Update src/content/docs/dns/manage-dns-records/how-to/managing-dynami…](/cloudflare/cloudflare-docs/pull/18115/commits/4504b2e8a8955a6cff0ecd90b78f110f10d8b06f "Update src/content/docs/dns/manage-dns-records/how-to/managing-dynamic-ip-addresses.mdx Changed Unix to UNIX. Co-authored-by: hyperlint-ai[bot] <154288675+hyperlint-ai[bot]@users.noreply.github.com>")` …
>
>
>
>Loading status checks…
>
>`[4504b2e](/cloudflare/cloudflare-docs/pull/18115/commits/4504b2e8a8955a6cff0ecd90b78f110f10d8b06f)`
>
>…c-ip-addresses.mdx
>
>
>Changed Unix to UNIX.
>
>Co-authored-by: hyperlint-ai\[bot\] <154288675+hyperlint-ai\[bot\]@users.noreply.github.com>
>
>SunsetQuest
>
>`[Update src/content/docs/dns/manage-dns-records/how-to/managing-dynami…](/cloudflare/cloudflare-docs/pull/18115/commits/bc46acde8770b021b4aa6fa0c32d0a6fde48a94e "Update src/content/docs/dns/manage-dns-records/how-to/managing-dynamic-ip-addresses.mdx Updated CloudFlare to Cloudflare. Co-authored-by: hyperlint-ai[bot] <154288675+hyperlint-ai[bot]@users.noreply.github.com>")` …
>
>
>This commit was created on GitHub.com and signed with GitHub’s **verified signature**.
>
>
>
>Loading status checks…
>
>`[bc46acd](/cloudflare/cloudflare-docs/pull/18115/commits/bc46acde8770b021b4aa6fa0c32d0a6fde48a94e)`
>
>…c-ip-addresses.mdx
>
>
>Updated CloudFlare to Cloudflare.
>
>Co-authored-by: hyperlint-ai\[bot\] <154288675+hyperlint-ai\[bot\]@users.noreply.github.com>
>
>Rebecca Tamachiro assigned angelampcosta and Rebecca Tamachiro and unassigned Rebecca Tamachiro and angelampcosta [Nov 14, 2024](#event-15296342728)
>
>
>**Rebecca Tamachiro** commented [Nov 20, 2024](#issuecomment-2489171599)
>
>Hi @SunsetQuest 👋 Thanks for reaching out.  
>Unfortunately, I will not be able to list your updater in our docs, but you can share it with other Cloudflare users via our [Community](https://community.cloudflare.com/).
> 
> 
>Rebecca Tamachiro closed this [Nov 20, 2024](#event-15370651378)
> 
> 
>### 
>
>**SunsetQuest** commented [Nov 20, 2024](#issuecomment-2489345792)
>
>@Rebecca Tamachiro - No concerns. It's advisable to update the page on managing dynamic IP addresses at Cloudflare's developer documentation. Currently, there is no Dynamic DNS updater available for Windows users without installing Perl. Additionally, the DNS-O-Matic service is no longer accessible.
>
>##Closed with unmerged commits
>This pull request is closed, but the SunsetQuest:patch-4 branch has unmerged commits. You can delete this branch if you wish.
>If you wish, you can also delete this fork of cloudflare/cloudflare-docs in the settings.
