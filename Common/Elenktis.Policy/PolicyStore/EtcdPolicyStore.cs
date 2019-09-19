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

using Elenktis.Secret;

namespace Elenktis.Policy
{
    public class EtcdPolicyStore : IPolicyStore
    {
        public EtcdPolicyStore
            (PolicyStoreConnInfo storeConnInfo, IPolicyStoreKeyMapper keyMapper)
        {
            _keyMapper = keyMapper;
            
             _etcd = new EtcdClient(storeConnInfo.Hostname, storeConnInfo.Port);
        }

        public async Task CreateOrSetPolicyAsync<TPolicy>
            (string subscriptionId, 
             Expression<Func<TPolicy,object>> getMeasureExpr) where TPolicy : Policy
        {
            var policyType = ((ParameterExpression)getMeasureExpr.Parameters[0]).Type;
            string policyName = policyType.Name;

            string planName = _keyMapper.GetPlanNameByAttribute<TPolicy>();

            string measureName = GetMemberName(getMeasureExpr);
    
            string measureValue = GetMemberValue(getMeasureExpr);
            
            string etcKey =
                _keyMapper.CreatePolicyStoreKey
                    (subscriptionId, planName, policyName, measureName);

            await _etcd.PutAsync(etcKey, measureValue);
        }

        public async Task<TPolicy> GetPolicyAsync<TPolicy>
            (string subscriptionId) where TPolicy : Policy
        {
            TPolicy policy = (TPolicy)Activator.CreateInstance(typeof(TPolicy));

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

        public Task<IDictionary<string, string>> GetSubscriptions() {
            return _etcd.GetRangeValAsync("sub");
        }

        public void OnPolicyChanged<TPolicy>
            (   string subscriptionId,
                Expression<Func<TPolicy, object>> measureToWatchChange,
                Action<string> onPolicyChanged) where TPolicy : Policy
        {
            string policyName = _keyMapper.GetPlanNameByAttribute<TPolicy>();
            
            var measurePropInfo = ((MemberExpression)measureToWatchChange.Body).Member;

            string planName = _keyMapper.GetPlanNameByAttribute<TPolicy>();

            string measureName = measurePropInfo.Name;

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

        public async Task CreatePlanExistFlagAsync<TPlan>
            (string subscriptionId) where TPlan : AssessmentPlan
        {
            string key =  _keyMapper.CreatePlanKey<TPlan>(subscriptionId);

            await _etcd.PutAsync(key, "true");
        }

        public async Task<bool> IsPlanExistAsync<TPlan>(string subscriptionId) where TPlan : AssessmentPlan
        {
            string planExistFlagKey = _keyMapper.CreatePlanKey<TPlan>(subscriptionId);

            RangeResponse etcdResp = await _etcd.GetAsync(planExistFlagKey);

            if(etcdResp.Count == 0)
                return false;
            else
                return true;
        }

        private string GetMemberName<TPolicy>
            (Expression<Func<TPolicy, object>> expr) where TPolicy : Policy 
            {
                switch(expr.Body)
                {
                    case MemberExpression m:
                        return m.Member.Name;
                    case UnaryExpression u when u.Operand is BinaryExpression b:
                        var memberExpr = (MemberExpression)b.Left;
                        return memberExpr.Member.Name;
                    default:
                        throw new NotImplementedException(expr.GetType().ToString());
                }
            }

        private string GetMemberValue<TPolicy> 
            (Expression<Func<TPolicy, object>> expr) where TPolicy : Policy
            {
                MemberExpression memberExpr = null;

                switch(expr.Body)
                {
                    case MemberExpression m:
                        memberExpr = m;
                        break;
                    case UnaryExpression u when u.Operand is BinaryExpression b:
                       return ((ConstantExpression)b.Right).Value.ToString();
                    default:
                        throw new NotImplementedException(expr.GetType().ToString());
                }

                var constExpr = (ConstantExpression)memberExpr.Expression;

                string value =
                    ((FieldInfo)memberExpr.Member).GetValue(constExpr.Value).ToString();

                return value;
            }

        private ControllerSecret _secrets;
        private EtcdClient _etcd;
        private IPolicyStoreKeyMapper _keyMapper;
        private ISecretHydrator _secretHydrator;
    }
}