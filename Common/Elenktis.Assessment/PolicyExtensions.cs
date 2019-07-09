using System.Reflection;

namespace Elenktis.Assessment
{
    public static class PolicyExtensions
    {
        public static bool IsPolicyMeasure(this PropertyInfo property)
        {
            var policyMeasureAttr = property.GetCustomAttribute(typeof(PolicyMeasureAttribute));
            if(policyMeasureAttr != null)
                return true;
            else
                return false;
        }
    }
}