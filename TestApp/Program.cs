using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDatabaseService;
using System.Threading;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserDatabaseService.UserDatabaseService uds = new UserDatabaseService.UserDatabaseService();
            uds.StartService();
            Thread.Sleep(2000);
            uds.StopService();
            Console.ReadLine();
        }
    }
}
