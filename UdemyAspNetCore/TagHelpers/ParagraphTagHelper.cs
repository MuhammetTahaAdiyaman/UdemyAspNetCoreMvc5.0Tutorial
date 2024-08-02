using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UdemyAspNetCore.TagHelpers
{
    [HtmlTargetElement("paragraph")]
    public class ParagraphTagHelper : TagHelper
    {
        public string ShortDescription { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent($"<b>{ShortDescription}</b>");
        }
    }
}
