# Hackathon Submission Entry form

> __Important__  
> 
> Copy and paste the content of this file into README.md or face automatic __disqualification__  
> All headlines and subheadlines shall be retained if not noted otherwise.  
> Fill in text in each section as instructed and then delete the existing text, including this blockquote.

You can find a very good reference to Github flavoured markdown reference in [this cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet). If you want something a bit more WYSIWYG for editing then could use [StackEdit](https://stackedit.io/app) which provides a more user friendly interface for generating the Markdown code. Those of you who are [VS Code fans](https://code.visualstudio.com/docs/languages/markdown#_markdown-preview) can edit/preview directly in that interface too.


## Team name
⟹ SitecoreDiser

## Category
⟹ The best enhancement to the Sitecore Admin (XP) for Content Editors & Marketers

## Description
⟹ Document uploaded to "Submission" Folder
- SitecoreDISER-Content-Report-Module.docx
- SitecoreDISER-ContentReportModule-1.0.0.zip (Sitecore installation package)
- ContentReport-TestContent-1.0.0.zip (Optional installation package to create content pages for testing)


## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository.

⟹ [Video link](https://youtu.be/Wojc_PU0upU)



## Pre-requisites and Dependencies

⟹ Does your module rely on other Sitecore modules or frameworks?

- Sitecore 10.1
- Sitecore Search Solr
- Content Workflow is enabled

## Installation instructions
⟹ Write a short clear step-wise instruction on how to install your module.  

1. Use the Sitecore Installation wizard to install the [package](https://github.com/Sitecore-Hackathon/2021-Sitecore-DISER/blob/main/Submission/SitecoreDISER-ContentReportModule-1.0.0.zip)
2. **Optional:** Use the Sitecore Installation wizard to install the [content package](https://github.com/Sitecore-Hackathon/2021-Sitecore-DISER/blob/main/Submission/ContentReport-TestContent-1.0.0.zip). This is not needed if site already got pages created.
3. Rebuild Solr indexes


### Configuration
⟹ If there are any custom configuration that has to be set manually then remember to add all details here.

Configuration setting values need to be updated as required below,
- After package installation, look for the config file at the path '\App_Config\Include\Feature\ContentReport\Search.Setting.config' and update three settings as per site setup. Default values are from Sitecore vanilla installation items.
- name="SitecoreContentIndex" value="sitecore_master_index" - Update if Solr core name changed
- name="HomeItemId" value="{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}" - Update to site root Home item
- name="WorkflowItemId" value="fca998c50cc34f9194d80a4e6caece88" - Update to Workflow Id used for site          

## Usage instructions
⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

Usage instruction can be found [here](https://github.com/Sitecore-Hackathon/2021-Sitecore-DISER/blob/main/Submission/SitecoreDISER-Content-Report-Module.docx)

## Comments
If you'd like to make additional comments that is important for your module entry.
