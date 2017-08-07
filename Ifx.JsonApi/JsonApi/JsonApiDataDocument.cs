using System;

namespace Ifx.JsonApi.JsonApi
{
    public class JsonApiDataDocument : IJsonApiDataDocument
    {
        public JsonApiDataDocument(Type resourceType, object id)
        {
            Id = id;
            Type = resourceType.Name;
        }

        public object Id { get; set; }
        public string Type { get; }
    }
}