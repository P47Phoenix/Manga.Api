using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Ifx.JsonApi.JsonApi
{
    public class JsonApiBody<T> : IJsonApiLinks where T : class, new()
    {
        public JsonApiBody(IEnumerable<T> documents, IUrlHelper urlHelper)
        {
            Links = new Dictionary<string, string>();
            Body = documents.Select(d => new JsonApiDocument<T>(d, urlHelper)).ToList();
        }

        public Dictionary<string, string> Links { get; }

        public List<JsonApiDocument<T>> Body { get; }
    }
}
