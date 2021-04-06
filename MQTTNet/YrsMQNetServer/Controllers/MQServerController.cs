using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YrsMQTTNet.Core;
using YrsMQTTNet.Core.Model;

namespace YrsMQNetServer.Controllers
{
    /// <summary>
    /// MQTT服务远程API端口控制
    /// </summary>
    [ApiController]
    public class MQServerController : ControllerBase
    {
        private readonly MQService mQService;

        public MQServerController(MQService mQService)
        {
            this.mQService = mQService;
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        [HttpPost]
        [Route("api/mqsv/start")]
        public async Task<ReuquestResult> StartMQTTServer()
        {
            return await mQService.StartMQService();
        }


        /// <summary>
        /// 关闭服务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/mqsv/stop")]
        public async Task<ReuquestResult> StopMQTTServer()
        {
            return await mQService.StopMQSerive();
        }

    }
}
