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
            //TODO 添加日志
        }

        /// <summary>
        /// 服务器被关闭
        /// </summary>
        public void EH_StopServer(EventArgs eventArgs)
        {
            //TODO 添加日志

        }

        /// <summary>
        /// 某客户端连接已建立
        /// </summary>
        public void EH_ClientConnected(MqttServerClientConnectedEventArgs eventArgs)
        {
            //TODO 添加日志


        }

        /// <summary>
        /// 某客户端连接已断开
        /// </summary>
        public void EH_ClientDisConnected(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            //TODO 添加日志

        }

        /// <summary>
        /// 某客户端订阅了某主题
        /// </summary>
        public void EH_ClientSubscribed(MqttServerClientSubscribedTopicEventArgs eventArgs)
        {
            //TODO 添加日志

        }

        /// <summary>
        /// 某客户端退订了某主题
        /// </summary>
        public void EH_ClientUnSubscribed(MqttServerClientUnsubscribedTopicEventArgs eventArgs)
        {
            //TODO 添加日志

        }

        /// <summary>
        /// 接收到了某客户端的消息
        /// </summary>
        public void EH_MessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            //TODO 添加日志

        }


    }
}
