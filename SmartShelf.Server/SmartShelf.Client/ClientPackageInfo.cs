using SuperSocket.ProtoBase;

namespace SmartShelf.Client
{
    public class ClientPackageInfo : IPackageInfo
    {

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; } = "ClientProtobufPackageInfo";

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