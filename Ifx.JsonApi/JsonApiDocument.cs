using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Ifx.JsonApi
{
    public class JsonApiDocument<T> : IJsonApiDataDocument where T : class, new()
    {
        private T m_Record;
        private JsonApiId<T> m_JsonApiId;
        private readonly Dictionary<string, object> m_Attributes = new Dictionary<string, object>();
        private readonly Dictionary<string, PropertyDescriptor> m_AttributePropertyDescriptor = new Dictionary<string, PropertyDescriptor>();
        private readonly Dictionary<string, PropertyDescriptor> m_RelationPropertyDescriptor = new Dictionary<string, PropertyDescriptor>();

        public JsonApiDocument(T record)
        {
            var propertyDescriptors = TypeDescriptor
                .GetProperties(typeof(T))
                .OfType<PropertyDescriptor>()
                .Where(propertyDescriptor => propertyDescriptor.Attributes.OfType<JsonApiIdAttribute>().Any() == false)
                .ToDictionary(descriptor => descriptor.Name, value => value);
            ;
            foreach (var keyValuePair in propertyDescriptors)
            {
                if (keyValuePair.Value.Attributes.OfType<JsonApiRelation>().Any())
                {
                    m_RelationPropertyDescriptor.Add(keyValuePair.Key, keyValuePair.Value);
                }
                else
                {
                    m_AttributePropertyDescriptor.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            Set(record);
        }

        public JsonApiDocument() : this(new T()) { }

        public Object Id
        {
            get => m_JsonApiId.Get();
            set => m_JsonApiId.Set(value);
        }

        public string Type => typeof(T).Name;

        public IDictionary<string, object> Attributes => m_Attributes;

        public IDictionary<string, JsonApiRelationValue> Relationships { get; set; }

        public T Get()
        {
            m_AttributePropertyDescriptor.ReadValuesFromDictionary(m_Attributes, m_Record);

            return m_Record;
        }

        public void Set(T record)
        {
            m_Record = record;

            m_JsonApiId = new JsonApiId<T>(record);

            m_AttributePropertyDescriptor.WriteValuesToDictionary(m_Attributes, m_Record);
        }
    }
}