using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Elenktis.Policy
{
    public static class PolicyExtension
    {
        public static string AsString
            (this Policy policy, string subscriptionId,
            Expression<Func<Policy, object>> expr)
        {
            var unaryExpr =  expr.Body as UnaryExpression;
            var memberExpr = unaryExpr.Operand as MemberExpression;

            string measureName = memberExpr.Member.Name;

            Type policyType = policy.GetType();
            string policyName = policyType.Name;
            
            var assessmentPlan = (PlanAttribute)policyType
                .GetCustomAttributes(typeof(PlanAttribute), false).FirstOrDefault();
            
            string planName = assessmentPlan.AssessmenPlanType.Name;

            var mapper = new EtcdKeyMapper();

            return mapper.CreatePolicyStoreKey
                (subscriptionId, planName, policyName,  measureName);
        }

        public static string AsValueString
            (this Policy policy, Policy policyObject, Expression<Func<Policy, object>> expr)
        {
            var unaryExpr =  expr.Body as UnaryExpression;
            var memberExpr = unaryExpr.Operand as MemberExpression;

            string measureName = memberExpr.Member.Name;

            PropertyDescriptorCollection propDescs =
                TypeDescriptor.GetProperties(policyObject);
            
            string measureValue = "";

            foreach (PropertyDescriptor propDesc in propDescs)
            {
                if(propDesc.Name == measureName)
                {
                    measureValue = propDesc.GetValue(policyObject).ToString();
                    break;
                }
            }

            return measureValue;
        }
    
        public static string Name(this Policy policy)
        {
            return policy.GetType().Name;
        }

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