using System;
using SuperSocket.SocketBase;

namespace SmartShelf.Server
{
    public class Session : AppSession<Session, SessionRequestInfo>
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="e"></param>
        protected override void HandleException(Exception e)
        {
            // Console.Write("DataProtocolSession Exception : {0}", e.Command);
        }
        protected override void HandleUnknownRequest(SessionRequestInfo requestInfo)
        {
            //  base.HandleUnknownRequest(requestInfo);
            // Console.WriteLine("收到未知消息")
        }

        public virtual string GetSessionId()
        {
            return SessionID;
        }
    }
}