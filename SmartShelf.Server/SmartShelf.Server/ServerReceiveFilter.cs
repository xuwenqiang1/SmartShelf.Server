using SmartShelf.Common;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;

namespace SmartShelf.Server
{
    public class ServerReceiveFilter : BeginEndMarkReceiveFilter<SessionRequestInfo>
    {
        private static readonly byte[] BeginMark = "02".HexStringToByteArray();
        private static readonly byte[] EndMark = "03".HexStringToByteArray();

        public ServerReceiveFilter() : base(BeginMark, EndMark)
        {
        }

        protected override SessionRequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length)
        {
            return new SessionRequestInfo
            {
                Body = readBuffer.CloneRange(offset, length),
                BodySize = length
            };
        }
    }
}