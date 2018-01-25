using SmartShelf.Common;
using SuperSocket.ProtoBase;

namespace SmartShelf.Client
{
    public class ClientReceiveFilter : BeginEndMarkReceiveFilter<ClientPackageInfo>
    {
        private static readonly byte[] BeginMark = "02".HexStringToByteArray();
        private static readonly byte[] EndMark = "03".HexStringToByteArray();

        public ClientReceiveFilter() : base(BeginMark, EndMark)
        {
        }

        public override ClientPackageInfo ResolvePackage(IBufferStream bufferStream)
        {

            var bodyBuffer = new byte[bufferStream.Length];
            bufferStream.Read(bodyBuffer, 0, (int)bufferStream.Length);
            return new ClientPackageInfo
            {
                BodySize = bodyBuffer.Length,
                Body = bodyBuffer
            };
        }
    }
}