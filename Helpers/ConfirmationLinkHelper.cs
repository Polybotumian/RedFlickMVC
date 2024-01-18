using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

namespace RedFlickMVC.Helpers
{
    public static class ConfirmationLinkHelper
    {
        public static IHtmlContent ConfirmationLink(
            this IHtmlHelper htmlHelper,
            string linkText,
            string actionName,
            object routeValues,
            string confirmationMessage,
            object htmlAttributes)
        {
            var url = new UrlHelper(htmlHelper.ViewContext);
            var link = new TagBuilder("a");

            link.InnerHtml.Append(linkText);
            link.Attributes["href"] = url.Action(actionName, routeValues);
            link.Attributes["onclick"] = $"return confirm('{confirmationMessage.Replace("'", "\\'")}');";

            link.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return link;
        }
    }
}
