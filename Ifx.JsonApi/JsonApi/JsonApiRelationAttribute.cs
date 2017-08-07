using System;

namespace Ifx.JsonApi.JsonApi
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonApiRelationAttribute : Attribute
    {
        public JsonApiRelationAttribute(Type relatedModelType, string controllerRouteName, string[] controllerArguments = null)
        {
            this.ModelName = relatedModelType.Name;
            this.ControllerRouteName = controllerRouteName;
            this.ControllerArguments = controllerArguments ?? new string[0];
        }

        internal string ModelName { get; }

        internal string ControllerRouteName { get; }

        internal string[] ControllerArguments { get; }
    }
}
