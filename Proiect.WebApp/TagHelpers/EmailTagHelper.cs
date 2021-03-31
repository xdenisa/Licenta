using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.WebApp.TagHelpers
{
    public class EmailTagHelper:TagHelper
    {
        public string Email { get; set; }
        public string Body { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + Email);
            output.Content.SetContent(Body);
        }
    }
}
