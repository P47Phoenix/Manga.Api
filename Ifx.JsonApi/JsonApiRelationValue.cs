using System.Collections.Generic;

namespace Ifx.JsonApi
{
    public class JsonApiRelationValue : IJsonApiLinks
    {
        public Dictionary<string, string> Links { get; set; }

        public List<JsonApiDataDocument> Data { get; set; }
    }
}