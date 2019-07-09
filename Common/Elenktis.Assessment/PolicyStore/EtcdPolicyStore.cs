using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETCD.V3;
using Etcdserverpb;
using Google.Protobuf;
using Grpc.Core;
using Newtonsoft.Json;
using static Etcdserverpb.Watch;

namespace Elenktis.Assessment
{
    public class EtcdPolicyStore : IPolicyStore
    {
        public EtcdPolicyStore
            (string hostname, int port, IPolicyStoreKeyMapper keyMapper)
        {
            _hostname = hostname;
            _port = port;

            _etcd = new Client($"{hostname}:{port.ToString()}");

            _keyMapper = keyMapper; 
        }

        public async Task SetPolicy<T>(T policy) where T : Policy
        {
           IEnumerable<KeyMeasureValue> kmvs = _keyMapper.MapPolicytoConfigKeys(policy);

           foreach(var kmv in kmvs)
           {
               await _etcd.PutAsync(kmv.Key, kmv.MeasureValue);
           }
        }

        public async Task<T> GetPolicy<T>() where T : Policy
        {
            T policy = (T)Activator.CreateInstance(typeof(T));

            IEnumerable<KeyMeasureValue> kmvs = _keyMapper.MapPolicytoConfigKeys(policy);

            var props = policy.GetType().GetProperties();

            foreach(var kmv in kmvs)
            {
                RangeResponse resp = await _etcd.RangeAsync(kmv.Key);

                string policyMeasureValue = resp.Kvs[0].Value.ToStringUtf8();

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

        // public T GetPoliciesByAssessmentPlan<T>(T plan) where T : AssessmentPlan
        // {

        // }

        // public Task WatchPolicyChange<T>(T policy) where T : Policy
        // {
        //     IEnumerable<KeyMeasureValue> kmvs  = _keyMapper.MapPolicytoConfigKeys(policy);

        //     foreach(var kmv in kmvs)
        //     {
        //         WatchRequest request = new WatchRequest()
        //             {
        //                 CreateRequest = new WatchCreateRequest()
        //                 {
        //                     Key = ByteString.CopyFromUtf8(kmv.Key)

        //                 }
                    
        //             };

                  
              
        //     }
            
        //}

        private string _hostname;
        private int _port;
        private Client _etcd;

        private IPolicyStoreKeyMapper _keyMapper;
    }
}