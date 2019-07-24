﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Elenktis.Assessment
{
    public class EtcdKeyMapper : IPolicyStoreKeyMapper
    {
        public IEnumerable<PolicyKeyMeasureMap> MapPolicytoKeys<T>(T policy) where T : Policy
        {
           string subscriptionId = GetSubscriptionId(policy);
           string assessmentPlanName = GetAssessmentPlanNameFromAttribute(policy);
           string policyName = GetPolicyName(policy);
           var measures = GetPolicyMeasures(policy);

            var keyMeasureValues = new List<PolicyKeyMeasureMap>();

            foreach(var tuple in measures)
            {
                string measureName = tuple.Item1;

                string key =
                    GenerateEtcdKey(subscriptionId, assessmentPlanName, policyName, measureName);
                
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

        public string MapKeyFromMeasureProperty(PropertyInfo measure)
        {
            Type policyType = measure.ReflectedType;

           string subscriptionId = GetSubscriptionId(policyType);
           string assessmentPlanName = GetAssessmentPlanNameFromAttribute(policyType);
           string policyName = GetPolicyName(policyType);
           var measureName = measure.Name;

           return GenerateEtcdKey(subscriptionId, assessmentPlanName, policyName, measureName);
        }

        private string GenerateEtcdKey
            (string subscriptionId, string assessmentPlanName, string policyName, string measureName)
        {
            return $"/plan/{subscriptionId}/{assessmentPlanName}/{policyName}/{measureName}";
        }

        private string GetAssessmentPlanNameFromAttribute<T>(T policy)
        {
            var configKeyAttr =
            typeof(T).GetCustomAttributes(typeof(PolicyAssessmentPlanAttribute), false)
                .FirstOrDefault() as PolicyAssessmentPlanAttribute;

            if(configKeyAttr == null)
                throw new ArgumentException("Missing ConfigStoreKeyAttribute on Policy");

            return configKeyAttr.AssessmenPlanName.ToLowerInvariant();
        }

        private string GetSubscriptionId<T>(T policy)
        {
           PropertyInfo prop = policy.GetType().GetProperty("AssessmentPlan");
           var plan = (AssessmentPlan) prop.GetValue(policy);

           return plan.TenantSubscription.SubscriptionId;
        }

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
    }
}