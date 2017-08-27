using System;

namespace Ifx.JsonApi.JsonApi
{
    public class JsonApiDataDocument : IJsonApiDataDocument
    {
        public JsonApiDataDocument(string resourceType, object id)
        {
            Id = id;
            Type = resourceType;
        }

        public object Id { get; set; }
        public string Type { get; }
    }
}