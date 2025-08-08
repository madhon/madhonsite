namespace Romulus.Web.Features.TagHelpers;

using System.Runtime.InteropServices;

[HtmlTargetElement("DotNetCoreVersion", TagStructure = TagStructure.NormalOrSelfClosing)]
internal sealed class DotNetCoreVersionTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = string.Empty;
        var netcoreVer = RuntimeInformation.FrameworkDescription;
        output.Content.SetHtmlContent($"<!-- {netcoreVer} -->");
    }
}
