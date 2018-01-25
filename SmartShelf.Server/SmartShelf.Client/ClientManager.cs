using System;
using System.Net;
using System.Threading;
using SmartShelf.Common;

namespace SmartShelf.Client
{
    public class ClientManager
    {
        private static ClientManager _clientManager;
        private static readonly object LockObject = new object();
        private static readonly Logger Logger = Logger.GetLoggerInstance();
        private static readonly Client Client = new Client();

        private ClientManager()
        {
            Client.Initialize(new ClientReceiveFilter());

            Client.NewPackageReceived += Client_NewPackageReceived;
            Client.Connected += Client_Connected;
            Client.Closed += Client_Closed;
        }
        private static string ClientName => "智能货架通信模拟客户端";
        public static ClientManager GetInstance()
        {
            lock (LockObject)
            {
                return _clientManager ?? (_clientManager = new ClientManager());
            }
        }

        public Client GetCurrentClient()
        {
            return Client;
        }

        public void Start()
        {
            ConnectServer();
            Logger.Info(ClientName + "连接服务端成功！");
        }

        public void Stop()
        {
            Client.Close().GetAwaiter();
        }

        private void Client_Closed(object sender, EventArgs e)
        {
            Logger.Info(ClientName + "连接已被主动断开");
            ConnectServer();
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            Logger.Info(ClientName + "已连接服务端");
        }

        private void Client_NewPackageReceived(object sender, SuperSocket.ClientEngine.PackageEventArgs<ClientPackageInfo> packageInfo)
        {

        }

        private void ConnectServer()
        {
            var result = false;

            while (!result)
            {
                Logger.Info(ClientName + "连接服务端");
                //TODO:设置dnsEndPoint配置在配置文件里
                var dnsEndPoint = new DnsEndPoint("127.0.0.1", 9000);
                result = Client.ConnectAsync(dnsEndPoint).GetAwaiter().GetResult();
                if (!result)
                {
                    Logger.Info(ClientName + "连接服务端失败，10秒后重新连接！");
                    Thread.Sleep(10000);
                }
            }
        }
    }
}