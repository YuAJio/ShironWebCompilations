using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YrsMQTTNet.Core.Model;

namespace YrsMQTTNet.Core
{
    /// <summary>
    /// MQTT供Web端口调用的服务
    /// </summary>
    public class MQService
    {
        private readonly MQServer mQServer;

        public MQService(MQServer qServer)
        {
            mQServer = qServer;
        }

        public async Task<ReuquestResult> StartMQService()
        {
            return await mQServer.StartMQServer();
        }

        public async Task<ReuquestResult> StopMQSerive()
        {
            return await mQServer.StopServer();
        }

    }
}
