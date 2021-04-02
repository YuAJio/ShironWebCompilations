using MQTTnet;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace YrsMQTTNet.Core
{
    public class MQEventHanlder
    {
        /// <summary>
        /// 认证用户名
        /// </summary>
        private string ServerAuth_Username;
        /// <summary>
        /// 认证密码
        /// </summary>
        private string ServerAuth_Password;

        public MQEventHanlder(string serverAuth_Username, string serverAuth_Password)
        {
            ServerAuth_Username = serverAuth_Username;
            ServerAuth_Password = serverAuth_Password;
        }

        /// <summary>
        /// 鉴权身份认证
        /// </summary>
        /// <param name="cxt"></param>
        public void EH_ConnectionValidator(MqttConnectionValidatorContext cxt)
        {
            if (string.IsNullOrEmpty(ServerAuth_Username) || string.IsNullOrEmpty(ServerAuth_Password))
                cxt.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.BadUserNameOrPassword;
            else if (cxt.Username != ServerAuth_Username || cxt.Password != ServerAuth_Password)
                cxt.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.BadUserNameOrPassword;
            cxt.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.Success;
        }


        /// <summary>
        /// 服务器被打开
        /// </summary>
        public void EH_StartServer(EventArgs eventArgs)
        {
            LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Info, "MQTT服务已打开");
        }

        /// <summary>
        /// 服务器被关闭
        /// </summary>
        public void EH_StopServer(EventArgs eventArgs)
        {
            LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Info, "MQTT服务已关闭");
        }

        /// <summary>
        /// 某客户端连接已建立
        /// </summary>
        public void EH_ClientConnected(MqttServerClientConnectedEventArgs eventArgs)
        {
            LMLogs.NLog.Logger.Log(
                LMLogs.LogLevel.Info,
                $"检测到有客户端建立了连接:\n客户端名称:<{eventArgs.ClientId}>");
        }

        /// <summary>
        /// 某客户端连接已断开
        /// </summary>
        public void EH_ClientDisConnected(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Info,
                $"检测到有客户端断开了连接:\n客户端名称:<{eventArgs.ClientId}>\n客户端断开接方式:<{eventArgs.DisconnectType}>");
        }

        /// <summary>
        /// 某客户端订阅了某主题
        /// </summary>
        public void EH_ClientSubscribed(MqttServerClientSubscribedTopicEventArgs eventArgs)
        {
            LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Info,
                $"检测到有客户端订阅了主题:\n" +
                $"客户端名称:<{eventArgs.ClientId}>\n" +
                $"主题名:<{eventArgs.TopicFilter.Topic}>");
        }

        /// <summary>
        /// 某客户端退订了某主题
        /// </summary>
        public void EH_ClientUnSubscribed(MqttServerClientUnsubscribedTopicEventArgs eventArgs)
        {
            LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Info,
              $"检测到有客户端退订了主题:\n" +
              $"客户端名称:<{eventArgs.ClientId}>\n" +
              $"主题名:<{eventArgs.TopicFilter}>");
        }

        /// <summary>
        /// 接收到了某客户端的消息
        /// </summary>
        public void EH_MessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            LMLogs.NLog.Logger.Log(LMLogs.LogLevel.Info,
           $"收到了来自<{eventArgs.ClientId}>客户端的消息:\n" +
           $"客户端名称:<{eventArgs.ClientId}>\n" +
           $"消息主题:<{eventArgs.ApplicationMessage.Topic}>\n" +
           $"消息内容:<{Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload)}>");
        }


    }
}
