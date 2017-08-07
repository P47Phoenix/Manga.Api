using System;

namespace Ifx.JsonApi.JsonApi
{
    public interface IJsonApiDataDocument
    {
        Object Id { get; set; }
        string Type { get; }
    }
}