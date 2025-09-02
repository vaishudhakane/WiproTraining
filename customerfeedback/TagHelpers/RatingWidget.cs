using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace CustomerFeedback.TagHelpers
{
    [HtmlTargetElement("rating-widget")]
    public class RatingWidgetTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "star-rating");
            
            for (int i = 1; i <= 5; i++)
            {
                output.Content.AppendHtml($@"
                    <span class='star' data-value='{i}'>★</span>
                ");
            }
            
            output.Content.AppendHtml($@"
                <input type='hidden' id='ratingValue' name='{For.Name}' value='{For.Model}' />
            ");
        }
    }
}
