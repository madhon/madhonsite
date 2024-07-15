# Release Notes
**Year:** {{year}}
**Stage:** {{currentStage.identifier}}
**Date & Time:** {{buildDetails.startTime}}
**Version:** {{buildDetails.buildNumber}}
 
## Features
{{#if this.relatedWorkItems}}
{{#forEach this.relatedWorkItems}}
{{#if (eq this.fields.[System.WorkItemType] 'User Story')}}
- #{{this.id}}
{{/if}}
{{/forEach}}
{{else}}
None
{{/if}}
 
## Bugfixes
{{#if this.relatedWorkItems}}
{{#forEach this.relatedWorkItems}}
{{#if (eq this.fields.[System.WorkItemType] 'Bug')}}
- #{{this.id}}
{{/if}}
{{/forEach}}
{{else}}
None
{{/if}}