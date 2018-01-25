using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartShelf.Client;

namespace SmartShelf.Application.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientManager = ClientManager.GetInstance();
            clientManager.Start();
            Console.WriteLine("Press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
            }
            clientManager.Stop();
        }
    }
}
