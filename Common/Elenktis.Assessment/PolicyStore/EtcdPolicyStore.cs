using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using Newtonsoft.Json;
using dotnet_etcd;
using Etcdserverpb;
using System.Linq.Expressions;
using System.Reflection;

using Elenktis.Configuration;

namespace Elenktis.Assessment
{
    public class EtcdPolicyStore : IPolicyStore
    {
        public EtcdPolicyStore
            (PolicyStoreConnInfo storeConnInfo, IPolicyStoreKeyMapper keyMapper)
        {
            _keyMapper = keyMapper;
            
             _etcd = new EtcdClient(storeConnInfo.Hostname, storeConnInfo.Port);
        }

        public async Task SetPolicyAsync<TAssessmentPlan>
            (string subscriptionId, 
             Expression<Action<Policy>> measure, object value) where TAssessmentPlan : AssessmentPlan
        {
            string assessmentPlanName = typeof(TAssessmentPlan).Name;

            var policyType = ((ParameterExpression)measure.Parameters[0]).Type;
            string policyName = policyType.Name;

            var measureProperty =
                ((MemberExpression)measure.Body).Member as PropertyInfo;
            
            string measureName = measureProperty.Name;
            
            string etcKey = _keyMapper.CreatePolicyStoreKey(subscriptionId, assessmentPlanName,
                policyName, measureName);

            await _etcd.PutAsync(etcKey, value.ToString());
        }

        public async Task<TPolicy> GetPolicyAsync<TPolicy,TAssessmentPlan>
            (string subscriptionId) where TPolicy : Policy where TAssessmentPlan : AssessmentPlan
        {
            TAssessmentPlan plan = (TAssessmentPlan)Activator.CreateInstance(typeof(TAssessmentPlan));

            TPolicy policy = (TPolicy)Activator.CreateInstance(typeof(TPolicy), plan);

            IEnumerable<PolicyKeyMeasureMap> kmvs =
                _keyMapper.GetKeyMeasureMap(subscriptionId, policy);

            var props = policy.GetType().GetProperties();

            foreach(var kmv in kmvs)
            {
                string measureValue = await _etcd.GetValAsync(kmv.PolicyKey);

                 foreach(var prop in props)
                 {
                     //e.g if(propertyName == ToAssess)
                     if(prop.IsPolicyMeasure() && prop.Name == kmv.MeasureName)
                         prop.SetValue(policy, measureValue);

                 }
            }

            return policy;
        }

        public void WatchPolicyChange<TPolicy,TAssessmentPlan>
            (string subscriptionId,
                Expression<Func<Policy, object>> measure,
                Action<string> onPolicyChanged)
                where TPolicy : Policy
                where TAssessmentPlan : AssessmentPlan
        {
            string planName = typeof(TAssessmentPlan).GetType().Name;
            string policyName = typeof(TPolicy).GetType().Name;

            var measureProp = ((MemberExpression)measure.Body).Member as PropertyInfo;
            string measureName = measureProp.Name;

            string key =
                _keyMapper.CreatePolicyStoreKey
                    (subscriptionId, planName, policyName, measureName);

            WatchRequest request = new WatchRequest()
                {
                    CreateRequest = new WatchCreateRequest()
                    {
                        Key = ByteString.CopyFromUtf8(key)
                    }
                };

            _etcd.Watch(request, (resp) =>{

                    string value = resp.Events[0].Kv.Value.ToStringUtf8();

                    onPolicyChanged(value);
            });
        }

        
        private ControllerSecret _secrets;
        private EtcdClient _etcd;
        private IPolicyStoreKeyMapper _keyMapper;
        private ISecretHydrator _secretHydrator;
    }
}