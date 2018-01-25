using SuperSocket.SocketBase.Protocol;

namespace SmartShelf.Server
{
    /// <summary>
    /// 请求消息
    /// </summary>
    public class SessionRequestInfo : IRequestInfo
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; } = "SmartShelfServer";
        /// <summary>
        /// 包体长度
        /// </summary>
        public int BodySize { get; set; }
        /// <summary>
        ///  包数据
        /// </summary>
        public byte[] Body { get; set; }

       
    }
}