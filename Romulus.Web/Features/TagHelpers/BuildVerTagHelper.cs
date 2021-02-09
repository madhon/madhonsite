namespace Romulus.Web.Features.TagHelpers
{
	using Microsoft.AspNetCore.Razor.TagHelpers;

	[HtmlTargetElement("BuildVer", TagStructure = TagStructure.NormalOrSelfClosing)]
	public class BuildVerTagHelper : TagHelper
	{
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = string.Empty;
			//output.Content.SetHtmlContent($"<!-- {ThisAssembly.AssemblyInformationalVersion} -->");
		}
	}
}
