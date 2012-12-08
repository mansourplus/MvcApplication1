using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftSchool.Models;
using System.Web.Routing;
public static class ImageHelper
{
    [Authorize(Roles = "Administrateur")]
    public static string Image(this HtmlHelper helper, string id, string url, string alternateText)
    {
        return Image(helper, id, url, alternateText, null);
    }

    public static string Image(this HtmlHelper helper, string id, string url, string alternateText, object htmlAttributes)
    {
        // Instantiate a UrlHelper 
        var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

        // Create tag builder
        var builder = new TagBuilder("img");

        // Create valid id
        builder.GenerateId(id);

        // Add attributes
        builder.MergeAttribute("src", urlHelper.Content(url));
        builder.MergeAttribute("alt", alternateText);
        builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

        // Render tag
        var ret = new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        return ret.ToHtmlString();
    }

}