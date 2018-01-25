using System;
using SmartShelf.Server;

namespace SmartShelf.Application.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverManager = ServerManager.GetInstance();
            serverManager.Start();
            Console.WriteLine("Press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
            }
            serverManager.Stop();
        }
    }
}
