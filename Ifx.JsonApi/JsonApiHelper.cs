using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ifx.JsonApi
{
    internal static class JsonApiHelper
    {
        public static void WriteValuesToDictionary<T>(this Dictionary<string, PropertyDescriptor> propertyDescriptors, Dictionary<string, object> dictionary, T record)
        {
            dictionary.Clear();

            foreach (var descriptor in propertyDescriptors.Values)
            {
                dictionary.Add(descriptor.Name, descriptor.GetValue(record));
            }
        }

        public static void ReadValuesFromDictionary<T>(this Dictionary<string, PropertyDescriptor> propertyDescriptors, Dictionary<string, object> dictionary, T record)
        {
            foreach (var attribute in dictionary)
            {
                PropertyDescriptor propertyDescriptor;
                if (propertyDescriptors.ContainsKey(attribute.Key))
                {
                    propertyDescriptor = propertyDescriptors[attribute.Key];
                }
                else
                {
                    propertyDescriptor = propertyDescriptors
                        .Where(pd => pd.Key.Equals(attribute.Key, StringComparison.CurrentCultureIgnoreCase))
                        .Select(pd => pd.Value)
                        .FirstOrDefault();
                }

                propertyDescriptor?.SetValue(record, attribute.Value);
            }

        }
    }
}
