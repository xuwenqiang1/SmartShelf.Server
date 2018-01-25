using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SmartShelf.Server
{
    public class Server : AppServer<Session, SessionRequestInfo>
    {
        public Server()
            : base(new DefaultReceiveFilterFactory<ServerReceiveFilter, SessionRequestInfo>())
        {

        }
    }
}
