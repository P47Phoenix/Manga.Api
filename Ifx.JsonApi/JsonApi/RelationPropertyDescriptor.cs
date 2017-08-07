using System.ComponentModel;

namespace Ifx.JsonApi.JsonApi
{
    public class RelationPropertyDescriptor
    {
        public PropertyDescriptor PropertyDescriptor { get; set; }
        public JsonApiRelationAttribute JsonApiRelationAttribute { get; set; }
    }
}