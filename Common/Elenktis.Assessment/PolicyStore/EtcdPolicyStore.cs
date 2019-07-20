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

namespace Elenktis.Assessment
{
    public class EtcdPolicyStore : IPolicyStore
    {
        public EtcdPolicyStore
            (string hostname, int port, IPolicyStoreKeyMapper keyMapper)
        {
            _hostname = hostname;
            _port = port;

            _etcd = new EtcdClient(hostname, port); //($"{hostname}:{port.ToString()}");

            _keyMapper = keyMapper; 
        }

        public async Task SetPolicyAsync<T>(T policy) where T : Policy
        {
           IEnumerable<PolicyKeyMeasureMap> kmvs = _keyMapper.MapPolicytoKeys(policy);

           foreach(var kmv in kmvs)
           {
               await _etcd.PutAsync(kmv.PolicyKey, kmv.MeasureValue);
           }
        }

        public async Task<T> GetPolicyAsync<T>() where T : Policy
        {
            T policy = (T)Activator.CreateInstance(typeof(T));

            IEnumerable<PolicyKeyMeasureMap> kmvs = _keyMapper.MapPolicytoKeys(policy);

            var props = policy.GetType().GetProperties();

            foreach(var kmv in kmvs)
            {
                string policyMeasureValue = await _etcd.GetValAsync(kmv.PolicyKey);

                 foreach(var prop in props)
                 {
                     if(prop.IsPolicyMeasure() && prop.Name == kmv.MeasureName)
                     {
                         prop.SetValue(policy, policyMeasureValue);
                     }
                 }
            }

            return policy;
        }

        public void WatchPolicyChange<TPolicy>
            (Expression<Func<TPolicy,object>> property, Action<string> onValueChanged)
                where TPolicy : Policy
        {
            
            var measureProp = ((MemberExpression)property.Body).Member as PropertyInfo;

           string key = _keyMapper.MapKeyFromMeasureProperty(measureProp);

            WatchRequest request = new WatchRequest()
                {
                    CreateRequest = new WatchCreateRequest()
                    {
                        Key = ByteString.CopyFromUtf8(key)
                    }
                };

            _etcd.Watch(request, (resp) =>{

                    string value = resp.Events[0].Kv.Value.ToStringUtf8();

                    onValueChanged(value);
            } );
        }

        private string _hostname;
        private int _port;
        private EtcdClient _etcd;

        private IPolicyStoreKeyMapper _keyMapper;
    }
}