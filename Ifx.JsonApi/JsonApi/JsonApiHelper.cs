using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Ifx.JsonApi.JsonApi
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


        public static void WriteValuesToDictionary<T>(this Dictionary<string, RelationPropertyDescriptor> relationPropertyDescriptors, Dictionary<string, PropertyDescriptor> attributePropertyDescriptors, Dictionary<string, JsonApiRelationValue> dictionary, IUrlHelper urlHelper, T record)
        {
            dictionary.Clear();

            var relations = GetRelation(relationPropertyDescriptors, attributePropertyDescriptors, urlHelper, record);

            foreach (var relation in relations)
            {
                if (dictionary.ContainsKey(relation.key))
                {
                    var jsonApiRelationValue = dictionary[relation.key];

                    jsonApiRelationValue.Data.AddRange(relation.Item2.Data);
                }
                else
                {
                    dictionary.Add(relation.key, relation.jsonApiRelationValue);
                }

            }

        }

        private static IEnumerable<(string key, JsonApiRelationValue jsonApiRelationValue)> GetRelation<T>(
            Dictionary<string, RelationPropertyDescriptor> relationPropertyDescriptors,
            Dictionary<string, PropertyDescriptor> attributePropertyDescriptors,
            IUrlHelper helper,
            T record)
        {
            foreach (var keyValuePair in relationPropertyDescriptors)
            {
                var attribute = keyValuePair.Value.JsonApiRelationAttribute;
                var descriptor = keyValuePair.Value.PropertyDescriptor;

                var routeArguments = GetRouteArguments(attributePropertyDescriptors, attribute, record);

                var value = descriptor.GetValue(record);

                var link = helper.Link(attribute.ControllerRouteName, routeArguments);

                var jsonApiRelationValue = new JsonApiRelationValue
                {
                    Links = GetLinks(link),
                    Data = GetData(attribute.ModelName, value).ToList()
                };

                var name = attribute.ModelName;

                yield return (name, jsonApiRelationValue);
            }
        }

        private static Dictionary<string, string> GetLinks(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return new Dictionary<string, string>();
            }

            return new Dictionary<string, string>()
            {
                {
                    "related",
                    link
                }
            };
        }

        private static IEnumerable<JsonApiDataDocument> GetData(string modelName, object value)
        {
            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                var values = enumerable;

                foreach (var val in values)
                {
                    yield return new JsonApiDataDocument(modelName, val);
                }
            }
            else
            {
                yield return new JsonApiDataDocument(modelName, value);
            }
        }

        private static Dictionary<string, object> GetRouteArguments<T>(
            Dictionary<string, PropertyDescriptor> attributePropertyDescriptors,
            JsonApiRelationAttribute attribute,
            T record)
        {
            var routeArguments = new Dictionary<string, object>();

            if (attribute.ControllerArguments == null) return routeArguments;

            foreach (var controllerArgument in attribute.ControllerArguments)
            {
                if (attributePropertyDescriptors.ContainsKey(controllerArgument) == false) continue;

                var discriptor = attributePropertyDescriptors[controllerArgument];

                var value = discriptor.GetValue(record);

                var name = Char.ToLowerInvariant(controllerArgument[0]) + controllerArgument.Substring(1);

                routeArguments.Add(name, value);
            }
            return routeArguments;
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

        public static void ReadValuesFromDictionary<T>(
            this Dictionary<string, RelationPropertyDescriptor> propertyDescriptors,
            Dictionary<string, JsonApiRelationValue> dictionary, IUrlHelper urlHelper, T record)
        {
            foreach (var descriptor in propertyDescriptors)
            {

            }
        }
    }
}
