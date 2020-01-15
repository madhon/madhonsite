namespace Romulus.Web.Features.TagHelpers
{
  using System.Reflection;
  using System.Runtime.Versioning;
  using Microsoft.AspNetCore.Razor.TagHelpers;

  [HtmlTargetElement("DotNetCoreVersion", TagStructure = TagStructure.NormalOrSelfClosing)]
  public class DotNetCoreVersionTagHelper : TagHelper
  {
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = string.Empty;

      var framework = Assembly
        .GetEntryAssembly()?
        .GetCustomAttribute<TargetFrameworkAttribute>()?
        .FrameworkName;

      output.Content.SetHtmlContent($"<!-- {framework} -->");
    }
  }
}
