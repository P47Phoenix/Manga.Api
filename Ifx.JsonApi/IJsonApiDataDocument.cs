using System;

namespace Ifx.JsonApi
{
    public interface IJsonApiDataDocument
    {
        Object Id { get; set; }
        string Type { get; }
    }
}