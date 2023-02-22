namespace Romulus.Web.Features.TagHelpers;

[HtmlTargetElement("BuildVer", TagStructure = TagStructure.NormalOrSelfClosing)]
public class BuildVerTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        string buildver = @$"<!--
BuildNumber: {AppVersionInfo.GetBuildInfo().BuildNumber}
BuildId: {AppVersionInfo.GetBuildInfo().BuildId}
CommitHash: {AppVersionInfo.GetBuildInfo().CommitHash}
-->
";


        output.TagName = string.Empty;
        output.Content.SetHtmlContent(buildver);
    }
}
