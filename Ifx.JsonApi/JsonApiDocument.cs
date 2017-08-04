using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Ifx.JsonApi
{
    public class JsonApiDocument<T> where T : class, new()
    {
        private T m_Record;
        private JsonApiId<T> m_JsonApiId;
        private readonly Dictionary<string, object> m_Attributes;
        private readonly Dictionary<string, PropertyDescriptor> m_propertyDescriptor;

        public JsonApiDocument(T record)
        {
            m_propertyDescriptor = TypeDescriptor
                .GetProperties(typeof(T))
                .OfType<PropertyDescriptor>()
                .Where(propertyDescriptor => propertyDescriptor.Attributes.OfType<JsonApiIdAttribute>().Any() == false)
                .ToDictionary(descriptor => descriptor.Name.ToLower(), value => value);
            m_Attributes = new Dictionary<string, object>();
            Set(record);
        }

        public JsonApiDocument() : this(new T()) { }

        public Object Id
        {
            get => m_JsonApiId.Get();
            set => m_JsonApiId.Set(value);
        }

        public IDictionary<string, object> Attributes => m_Attributes;

        public T Get()
        {
            foreach (var attribute in m_Attributes)
            {
                var propertyDescriptor = m_propertyDescriptor[attribute.Key.ToLower()];

                propertyDescriptor.SetValue(m_Record, attribute.Value);
            }

            return m_Record;
        }

        public void Set(T record)
        {
            m_Record = record;

            m_JsonApiId = new JsonApiId<T>(record);

            m_Attributes.Clear();

            foreach (var descriptor in m_propertyDescriptor.Values)
            {
                m_Attributes.Add(descriptor.Name.ToLower(), descriptor.GetValue(m_Record));
            }
        }
    }
}