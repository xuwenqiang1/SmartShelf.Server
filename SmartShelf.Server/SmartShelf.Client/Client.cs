using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;

namespace SmartShelf.Client
{
    public class Client : EasyClient<ClientPackageInfo>
    {
        public Client()
        {

        }

        protected override void HandlePackage(IPackageInfo package)
        {
            base.HandlePackage(package);
        }
    }
}
