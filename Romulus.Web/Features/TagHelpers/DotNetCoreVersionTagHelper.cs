namespace Romulus.Web.Features.TagHelpers;

using System.Runtime.InteropServices;

[HtmlTargetElement("DotNetCoreVersion", TagStructure = TagStructure.NormalOrSelfClosing)]
public class DotNetCoreVersionTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = string.Empty;
        var netcoreVer = RuntimeInformation.FrameworkDescription;
        output.Content.SetHtmlContent($"<!-- {netcoreVer} -->");
    }
}
