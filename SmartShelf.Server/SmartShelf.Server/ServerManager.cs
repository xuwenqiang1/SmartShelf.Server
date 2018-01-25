using System.Collections.Concurrent;
using System.Threading;
using SmartShelf.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace SmartShelf.Server
{
    public class ServerManager
    {
        private static readonly Server Server = new Server();
        private static ServerManager _serverManager;
        private static readonly object LockObject = new object();
        private static readonly Logger Logger = Logger.GetLoggerInstance();

        private ServerManager()
        {
            ConnectedClients=new ConcurrentDictionary<string, ConnectedClientInformation>();
            Server.NewSessionConnected += Server_NewSessionConnected;
            Server.SessionClosed += Server_SessionClosed;
            Server.NewRequestReceived += Server_NewRequestReceived;
            //TODO:设置以下配置在配置文件里
            Server.Setup(new RootConfig(), new ServerConfig
            {
                Name = "智能货架通信服务",
                Ip = "Any",
                Port = 9000,
                MaxConnectionNumber = 10000,
                Mode = SocketMode.Tcp,
            });
        }

        private static string ServerName => "智能货架通信服务";
        public static ServerManager GetInstance()
        {
            lock (LockObject)
            {
                return _serverManager ?? (_serverManager = new ServerManager());
            }
        }

        public Server GetCurrentServer()
        {
            return Server;
        }
        public ConcurrentDictionary<string, ConnectedClientInformation> ConnectedClients { get; private set; }

        public int GetConnectedClientNumber()
        {
            return ConnectedClients.Count;
        }
        public void Start()
        {
            if (Server.Start())
            {
                Logger.Info(ServerName + "启动成功！");
            }
        }
        public void Stop()
        {
            Server.Stop();
        }

        private void Server_NewRequestReceived(Session session, SessionRequestInfo requestInfo)
        {
            var packageInformation = Analyze(requestInfo.Body);
            HandleReceivePackage(packageInformation);
            //ThreadPool.QueueUserWorkItem(HandleReceivePackage, packageInformation);
        }
        private void Server_SessionClosed(Session session, CloseReason reason)
        {
            ConnectedClientInformation station;
            ConnectedClients.TryRemove(session.SessionID, out station);
            Logger.Info(ServerName + $"检测点断开连接,设备地址:{session.RemoteEndPoint},SessionId:{session.SessionID}，原因：{reason}");
        }


        private void Server_NewSessionConnected(Session session)
        {
            if (ConnectedClients.TryAdd(session.SessionID, new ConnectedClientInformation
            {
                Name = "",
                Session = session
            }))
            {
                Logger.Info(ServerName + $"检测点连接,设备地址:{session.RemoteEndPoint},SessionId:{session.SessionID}");
            }
        }

        private PackageInformation Analyze(byte[] data)
        {
            return new PackageInformation();
        }

        private void HandleReceivePackage(object data)
        {
            var package = (PackageInformation) data;
            switch (package.Command)
            {
                case "Command1":
                    //TODO:Do work
                    break;
            }
        }
    }
}