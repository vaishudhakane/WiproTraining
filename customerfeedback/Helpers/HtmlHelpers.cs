using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerFeedback.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent StyledInput(this IHtmlHelper htmlHelper, string name, string placeholder, string cssClass)
        {
            var input = new TagBuilder("input");
            input.Attributes.Add("type", "text");
            input.Attributes.Add("name", name);
            input.Attributes.Add("id", name);
            input.Attributes.Add("placeholder", placeholder);
            input.AddCssClass(cssClass);
            return input;
        }
    }
}
