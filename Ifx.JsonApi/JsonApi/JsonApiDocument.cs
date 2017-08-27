using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Ifx.JsonApi.JsonApi
{
    public class JsonApiDocument<T> : IJsonApiLinks where T : class, new()
    {
        public JsonApiDocument(IEnumerable<T> documents, IUrlHelper urlHelper)
        {
            Links = new Dictionary<string, string>();
            Body = documents.Select(d => new JsonApiData<T>(d, urlHelper)).ToList();
        }

        public Dictionary<string, string> Links { get; }

        public List<JsonApiData<T>> Body { get; }
    }
}
