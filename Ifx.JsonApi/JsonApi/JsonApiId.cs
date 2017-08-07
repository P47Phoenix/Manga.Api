using System;
using System.ComponentModel;
using System.Linq;

namespace Ifx.JsonApi.JsonApi
{
    public class JsonApiId<T> where T : class, new()
    {
        private readonly T m_record;
        private readonly PropertyDescriptor m_idPropertyDescriptor;

        public JsonApiId(T record)
        {
            m_record = record;
            var propertyDescriptors = TypeDescriptor
                .GetProperties(typeof(T))
                .OfType<PropertyDescriptor>()
                .Where(propertyDescriptorWhere => propertyDescriptorWhere.Attributes.OfType<JsonApiIdAttribute>().Any())
                .ToList();

            if (propertyDescriptors.Count > 1)
            {
                throw new ArgumentException($"You can only have one property with a {typeof(JsonApiIdAttribute).Name}");
            }

            if (propertyDescriptors.Count < 1)
            {
                throw new ArgumentException($"At least one property needs to have a {typeof(JsonApiIdAttribute).Name}");
            }

            m_idPropertyDescriptor = propertyDescriptors.FirstOrDefault();
        }

        public JsonApiId() : this(new T()) { }

        public Object Get()
        {
            return m_idPropertyDescriptor.GetValue(m_record);
        }

        public void Set(Object obj)
        {
            if (m_idPropertyDescriptor.PropertyType != obj.GetType())
            {
                throw new ArgumentException($"Incoming type does not match property type. Incoming type was {obj.GetType().Name} and the expected type was {m_idPropertyDescriptor.PropertyType.Name} ");
            }

            m_idPropertyDescriptor.SetValue(m_record, obj);
        }
    }
}