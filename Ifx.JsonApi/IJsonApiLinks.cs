using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Ifx.JsonApi
{
    public interface IJsonApiLinks
    {
        Dictionary<string, string> Links { get; }
    }

    public static class JsonApiLinkExtensions
    {
        public static void AddLink(IJsonApiLinks apiLinks, string name, Controller conttroller, string routeName,
            object routeArguments = null)
        {
            string url = conttroller.Url.Link(routeName, routeArguments);
            apiLinks.Links.Add(name, url);
        }
    }
}