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
            UserDatabaseService.UserDatabaseService uds = UserDatabaseService.UserDatabaseService.GetInstance();


            Console.WriteLine("UserDatabaseService Created");
            //uds.SetDatabaseLocation(@"D:\ServiceServer\UserDatabaseService");
            

            uds.StartServiceAsync();
            Thread.Sleep(2000);
            uds.StopServiceAsync();
            Console.ReadLine();

        }
    }
}
