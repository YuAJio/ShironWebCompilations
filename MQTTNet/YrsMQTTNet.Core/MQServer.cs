using MQTTnet;
using MQTTnet.Client.Receiving;
using MQTTnet.Server;
using System;
using System.Threading.Tasks;
using YrsMQTTNet.Core.Model;

namespace YrsMQTTNet.Core
{
    /// <summary>
    /// MQTT的服务对象
    /// </summary>
    public class MQServer
    {
        #region 基础配置信息
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerHost_IP { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerHost_Port { get; set; }
        /// <summary>
        /// 认证用户名
        /// </summary>
        public string ServerAuth_Username { get; set; }
        /// <summary>
        /// 认证密码
        /// </summary>
        public string ServerAuth_Password { get; set; }
        /// <summary>
        /// 连接超时时间
        /// </summary>
        public long ConnectionTimeOut { get; set; }
        #endregion

        public MQServer(string serverHost_IP, int serverHost_Port, string serverAuth_Username, string serverAuth_Password, long connectionTimeOut)
        {
            ServerHost_IP = serverHost_IP;
            ServerHost_Port = serverHost_Port;
            ServerAuth_Username = serverAuth_Username;
            ServerAuth_Password = serverAuth_Password;
            ConnectionTimeOut = connectionTimeOut;

            InitService();
        }

        private MQEventHanlder EventHanlder
        {
            get
            {
                if (_eventHandler == null)
                    _eventHandler = new MQEventHanlder(ServerAuth_Username, ServerAuth_Password);
                return _eventHandler;
            }
        }
        private MQEventHanlder _eventHandler;


        private MqttServer mqttServer = null;
        private MqttServerOptionsBuilder mqOptionsBuilder = null;
        private void InitService()
        {
            try
            {
                if (mqttServer == null)
                {
                    mqOptionsBuilder = new MqttServerOptionsBuilder()
                        .WithDefaultEndpoint()
                        .WithDefaultEndpointBoundIPAddress(System.Net.IPAddress.Parse(ServerHost_IP))
                        .WithDefaultEndpointPort(ServerHost_Port)
                        .WithConnectionValidator(new MqttServerConnectionValidatorDelegate(EventHanlder.EH_ConnectionValidator))
                        .WithSubscriptionInterceptor(c => { c.AcceptSubscription = true; })
                        .WithApplicationMessageInterceptor(c => { c.AcceptPublish = true; });

                    mqttServer = new MqttFactory().CreateMqttServer() as MqttServer;
                    mqttServer.StartedHandler =
                        new MqttServerStartedHandlerDelegate(EventHanlder.EH_StartServer);
                    mqttServer.StoppedHandler =
                        new MqttServerStoppedHandlerDelegate(EventHanlder.EH_StopServer);
                    mqttServer.ClientConnectedHandler =
                        new MqttServerClientConnectedHandlerDelegate(EventHanlder.EH_ClientConnected);
                    mqttServer.ClientDisconnectedHandler =
                        new MqttServerClientDisconnectedHandlerDelegate(EventHanlder.EH_ClientDisConnected);
                    mqttServer.ClientSubscribedTopicHandler =
                        new MqttServerClientSubscribedHandlerDelegate(EventHanlder.EH_ClientSubscribed);
                    mqttServer.ClientUnsubscribedTopicHandler =
                        new MqttServerClientUnsubscribedTopicHandlerDelegate(EventHanlder.EH_ClientUnSubscribed);
                    mqttServer.ApplicationMessageReceivedHandler =
                        new MqttApplicationMessageReceivedHandlerDelegate(EventHanlder.EH_MessageReceived);
                    LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Info, "MqttService", "初始化成功");
                }
            }
            catch (Exception ex)
            {
                LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Exception, "MqttService", $"初始化异常:{ex.Message}");
            }
        }

        public async Task<ReuquestResult> StartMQServer()
        {
            try
            {
                if (mqttServer != null && mqOptionsBuilder != null)
                {
                    await mqttServer.StartAsync(mqOptionsBuilder.Build());
                    mqOptionsBuilder = null;
                    return new ReuquestResult { Successful = true };
                }
                else
                    LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Exception, "MqttService", $"开启成功");
                return new ReuquestResult { Successful = false };
            }
            catch (Exception e)
            {
                LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Exception, "MqttService", $"开启异常:{e.Message}");
                return new ReuquestResult { Successful = false, Code = 500 };
            }
        }


        public async Task<ReuquestResult> StopServer()
        {
            if (mqttServer == null) return new ReuquestResult { Code = 404, Successful = false };
            try
            {
                await mqttServer.StopAsync();
                mqttServer = null;
                LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Exception, "MqttService", $"停止服务成功");
                return new ReuquestResult { Successful = true };
            }
            catch (Exception ex)
            {
                LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Exception, "MqttService", $"停止服务异常:{ex.Message}");
                return new ReuquestResult { Successful = false, Code = 500 };
            }
        }


    }
}
