using System.Threading.Tasks;
using EtcdNet;
using Newtonsoft.Json;

namespace Elenktis.Assessment
{
    public class EtcdAssessmentPlanStore : IAssessmentPlanStore
    {
        public EtcdAssessmentPlanStore(string hostname, int port)
        {
            _hostname = hostname;
            _port = port;

            var options = new EtcdClientOpitions() {
                Urls = new string[] { $"http://{_hostname}:{_port}" },
            };

            _etcd = new EtcdClient(options);
        }

        public async Task TestConn()
        {
            await _etcd.CreateNodeAsync("/defaultsvc/sub/iaasantimalware", "on");

            string value = await _etcd.GetNodeValueAsync("defaultsvc/sub/iaasantimalware");
            
        }

        private string _hostname;
        private int _port;
        private EtcdClient _etcd;
    }
}