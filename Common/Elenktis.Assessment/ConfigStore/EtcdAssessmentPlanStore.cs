using System.Threading.Tasks;
using ETCD.V3;
using Etcdserverpb;
using Newtonsoft.Json;

namespace Elenktis.Assessment
{
    public class EtcdAssessmentPlanStore : IAssessmentPlanStore
    {
        public EtcdAssessmentPlanStore(string hostname, int port)
        {
            _hostname = hostname;
            _port = port;

            _etcd = new Client($"{hostname}:{port.ToString()}");
        }

        public async Task TestConn()
        {
            string key = "/defaultsvc/sub/iaasantimalware";

            await _etcd.PutAsync(key, "on");

            RangeResponse resp = await _etcd.RangeAsync(key);
            string value = resp.Kvs[0].Value.ToStringUtf8();

            _etcd.DeleteRange(key);
            
        }

        private string _hostname;
        private int _port;
        private Client _etcd;
    }
}