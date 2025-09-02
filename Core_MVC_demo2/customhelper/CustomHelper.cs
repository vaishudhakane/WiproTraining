using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core_MVC_demo2.customhelper
{
    [HtmlTargetElement("my-first-tag-helper")]
    public class CustomHelper: TagHelper
    {
        public string Name { get; set; } = "Default Name";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Set the tag name to a custom name
            output.TagName = "div"; // Change the tag to a <div>
            // Add a class to the output
            output.Attributes.SetAttribute("class", "custom-helper");
            // Set the content of the tag
            output.Content.SetContent($"Hello, {Name}!");
        }
    }
}
