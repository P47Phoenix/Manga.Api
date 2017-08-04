using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Ifx.JsonApi
{
    public class JsonApiBody<T> where T : class, new()
    {
        public JsonApiBody(IEnumerable<JsonApiDocument<T>> documents)
        {
            Links = new Dictionary<string, string>();
            Body = documents.ToList();
        }

        public Dictionary<string, string> Links { get; }

        public List<JsonApiDocument<T>> Body { get; }
    }
}
