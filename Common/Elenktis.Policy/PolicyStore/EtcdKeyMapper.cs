using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Elenktis.Policy
{
    public class EtcdKeyMapper : IPolicyStoreKeyMapper
    {
        public IEnumerable<PolicyKeyMeasureMap> GetKeyMeasureMap<T>
            (string subscriptionId, T policy) where T : Policy
        {
           string policyName = GetPolicyName(policy);
           var measures = GetPolicyMeasures(policy); //e.g ToAssess, ToRemediate

            string planName = GetPlanNameByAttribute<T>();

            var keyMeasureValues = new List<PolicyKeyMeasureMap>();

            foreach(var tuple in measures)
            {
                string measureName = tuple.Item1;

                string key =
                    GenerateEtcdKey(subscriptionId, planName, policyName, measureName);
                
                string value = tuple.Item2;

                keyMeasureValues.Add(new PolicyKeyMeasureMap()
                {
                    PolicyKey = key,
                    MeasureName = measureName,
                    MeasureValue = value
                });
            }

            return keyMeasureValues;
        }

        public string CreatePolicyStoreKey
            (string subscriptionId, string assessmentPlanName, string policyName, string measureName)
        {
           return GenerateEtcdKey(subscriptionId, assessmentPlanName, policyName, measureName);
        }

        public string CreatePlanKey<TPlan>(string subscriptionId) where TPlan : AssessmentPlan
        {
            string planName = typeof(TPlan).Name;
            
            return $"sub/{subscriptionId}/plan/{planName}";
        }

        
        private string GenerateEtcdKey
            (string subscriptionId, string planName, string policyName, string measureName)
        {
            return $"sub/{subscriptionId}/plan/{planName}/policy/{policyName}/measure/{measureName}";
        }

        public string GetPlanNameByAttribute<TPolicy>() where TPolicy : Policy
        {
            var configKeyAttr =
                typeof(TPolicy).GetCustomAttributes(typeof(PlanAttribute), false)
                .FirstOrDefault() as PlanAttribute;

            if(configKeyAttr == null)
                throw new ArgumentException("Missing ConfigStoreKeyAttribute on Policy");

            return configKeyAttr.AssessmenPlanType.Name;
        }

        #region helpers

        private string GetPolicyName<T>(T policy)
        {
            string className = policy.GetType().Name;
            string classNameWithoutPolicyName = null;

            int indexPolicy = className.IndexOf("Policy");

            if(indexPolicy != -1)
               classNameWithoutPolicyName =
                    className.Substring(0, ((className.Length - indexPolicy) - 1));
            else
                classNameWithoutPolicyName = className;

            return classNameWithoutPolicyName.ToLowerInvariant();
        }

        private IEnumerable<Tuple<string,string>> GetPolicyMeasures<T>(T policy)
        {
            var actions = new List<Tuple<string,string>>();

            var properties = policy.GetType().GetProperties();

            foreach(var prop in properties)
            {
               var policyActionAttr = prop.GetCustomAttribute(typeof(PolicyMeasureAttribute));

               if(policyActionAttr != null)
               {
                   string measureName = prop.Name.ToLowerInvariant();
                   string measureValue = prop.GetValue(policy).ToString();

                   actions.Add(new Tuple<string,string>(measureName, measureValue));
               }
            }

            return actions;
        }

        #endregion
    }
}