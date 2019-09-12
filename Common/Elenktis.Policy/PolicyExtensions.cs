using System.Reflection;

namespace Elenktis.Policy
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

        public static bool ToBool(this string value)
        {
            if(value.ToLowerInvariant() == "true")
                return true;
            else if(value.ToLowerInvariant() == "false")
                return false;
            
            return true;
        }
    }
}