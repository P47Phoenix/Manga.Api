using System;
using System.Collections.Generic;
using System.Text;

namespace Ifx.JsonApi
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonApiRelation : Attribute
    {
        public JsonApiRelation(Type relatedModelType, string controllerRoute, string[] controllerArguments = null)
        {
            this.ModelName = relatedModelType.Name;
            this.ControllerRoute = controllerRoute;
            this.ControllerArguments = controllerArguments ?? new string[0];
        }

        internal string ModelName { get; }

        internal string ControllerRoute { get; }

        internal string[] ControllerArguments { get; }
    }
}
