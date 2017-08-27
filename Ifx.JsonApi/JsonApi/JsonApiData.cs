using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Ifx.JsonApi.JsonApi
{
    public class JsonApiData<T> : IJsonApiDataDocument where T : class, new()
    {
        private T m_Record;
        private JsonApiId<T> m_JsonApiId;

        private readonly Dictionary<string, PropertyDescriptor> m_AttributePropertyDescriptor = new Dictionary<string, PropertyDescriptor>();
        private readonly Dictionary<string, RelationPropertyDescriptor> m_RelationPropertyDescriptor = new Dictionary<string, RelationPropertyDescriptor>();

        private readonly Dictionary<string, object> m_Attributes = new Dictionary<string, object>();
        private readonly Dictionary<string, JsonApiRelationValue> m_Relationships = new Dictionary<string, JsonApiRelationValue>();
        private readonly IUrlHelper m_UrlHelper;
        private readonly Dictionary<string, PropertyDescriptor> m_propertyDescriptors;

        public JsonApiData(T record, IUrlHelper urlHelper)
        {
            m_propertyDescriptors = TypeDescriptor
                .GetProperties(typeof(T))
                .OfType<PropertyDescriptor>()
                .ToDictionary(descriptor => descriptor.Name, value => value);

            m_UrlHelper = urlHelper;

            foreach (var keyValuePair in m_propertyDescriptors)
            {
                var jsonApiRelationAttribute = keyValuePair.Value.Attributes.OfType<JsonApiRelationAttribute>().FirstOrDefault();
                var jsonApiIdAttribute = keyValuePair.Value.Attributes.OfType<JsonApiIdAttribute>().FirstOrDefault();

                if (jsonApiRelationAttribute != null)
                {
                    m_RelationPropertyDescriptor.Add(keyValuePair.Key, new RelationPropertyDescriptor
                    {
                        PropertyDescriptor = keyValuePair.Value,
                        JsonApiRelationAttribute = jsonApiRelationAttribute
                    });
                }
                else if(jsonApiIdAttribute == null)
                {
                    m_AttributePropertyDescriptor.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            Set(record);
        }

        public JsonApiData(IUrlHelper urlHelper) : this(new T(), urlHelper) { }

        public Object Id
        {
            get => m_JsonApiId.Get();
            set => m_JsonApiId.Set(value);
        }

        public string Type => typeof(T).Name;

        public IDictionary<string, object> Attributes => m_Attributes;

        public IDictionary<string, JsonApiRelationValue> Relationships => m_Relationships;

        public T Get()
        {
            m_AttributePropertyDescriptor.ReadValuesFromDictionary(m_Attributes, m_Record);
            m_RelationPropertyDescriptor.ReadValuesFromDictionary(m_Relationships, m_UrlHelper, m_Record);

            return m_Record;
        }

        public void Set(T record)
        {
            m_Record = record;

            m_JsonApiId = new JsonApiId<T>(record);

            m_AttributePropertyDescriptor.WriteValuesToDictionary(m_Attributes, m_Record);

            m_RelationPropertyDescriptor.WriteValuesToDictionary(m_propertyDescriptors, m_Relationships, m_UrlHelper, m_Record);
        }
    }
}