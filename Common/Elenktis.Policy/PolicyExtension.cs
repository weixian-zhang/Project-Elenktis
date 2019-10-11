using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Elenktis.Policy
{
    public static class PolicyExtension
    {
        public static string AsPolicyKeyString
            (this Policy policy, string subscriptionId,
            Expression<Func<Policy, object>> expr)
        {
            var memExpr =  expr.Body as MemberExpression;
            string measureName =  memExpr.Member.Name;

            Type derivedPolicyType = memExpr.Member.ReflectedType;

            string policyName = derivedPolicyType.Name;
            
            var assessmentPlan = (PlanAttribute) derivedPolicyType
                .GetCustomAttributes(typeof(PlanAttribute), false).FirstOrDefault();
            
            string planName = assessmentPlan.AssessmenPlanType.Name;

            var mapper = new EtcdKeyMapper();

            return mapper.CreatePolicyStoreKey
                (subscriptionId, planName, policyName,  measureName);
        }

        public static string AsPolicyValueString
            (this Policy policy,
             Policy policyObject, Expression<Func<Policy, object>> expr)
        {
            var memExpr = expr.Body as MemberExpression;
            string measureName = memExpr.Member.Name;

            PropertyDescriptorCollection propDescs = TypeDescriptor.GetProperties(policyObject);
            
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
    }
}