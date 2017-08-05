using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Ifx.JsonApi
{
    public class JsonApiBody<T> : IJsonApiLinks where T : class, new()
    {
        public JsonApiBody(IEnumerable<T> documents)
        {
            Links = new Dictionary<string, string>();
            Body = documents.Select(d => new JsonApiDocument<T>(d)).ToList();
        }

        public Dictionary<string, string> Links { get; }

        public List<JsonApiDocument<T>> Body { get; }
    }
}
