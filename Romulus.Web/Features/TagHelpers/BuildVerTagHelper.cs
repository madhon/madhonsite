namespace Romulus.Web.Features.TagHelpers;

[HtmlTargetElement("BuildVer", TagStructure = TagStructure.NormalOrSelfClosing)]
public sealed class BuildVerTagHelper : TagHelper
{
    private static readonly Lazy<string> Comment = new(() =>
    {
        var info = AppVersionInfo.GetBuildInfo();
        return $"""
                <!--
                BuildNumber: {info.BuildNumber}
                BuildId: {info.BuildId}
                CommitHash: {info.CommitHash}
                -->
                """;
    });

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(output);
        output.TagName = string.Empty;
        output.Content.SetHtmlContent(Comment.Value);
    }
}
