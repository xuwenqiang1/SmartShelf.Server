namespace SmartShelf.Server
{
    public class PackageInformation
    {
        public string Address { get; set; }
        public int Length { get; set; }
        public string Command { get; set; }
        public string Data { get; set; }
        public string CheckSum { get; set; }
    }
}