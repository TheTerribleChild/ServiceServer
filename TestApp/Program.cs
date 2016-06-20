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
            Configuration.ServiceServerConfiguration.LoadConfiguration();// sConfig = Configuration.ServiceServerConfiguration.GetInstance();
            Console.WriteLine(Configuration.ServiceServerConfiguration.SERVICE_SERVER_LOCATION + " " + Configuration.ServiceServerConfiguration.DEBUG_MODE);
            Configuration.ServiceServerConfiguration.DEBUG_MODE = false;
            Console.WriteLine(Configuration.ServiceServerConfiguration.SERVICE_SERVER_LOCATION + " " + Configuration.ServiceServerConfiguration.DEBUG_MODE);
            Configuration.ServiceServerConfiguration.SaveConfiguration();

            UserDatabaseService.UserDatabaseServiceConfiguration.LoadConfiguration();// uConfig = UserDatabaseServiceConfiguration.GetInstance();
            Console.WriteLine(UserDatabaseService.UserDatabaseServiceConfiguration.testChange);
            UserDatabaseService.UserDatabaseServiceConfiguration.testChange = true;
            Console.WriteLine(UserDatabaseService.UserDatabaseServiceConfiguration.testChange);
            UserDatabaseService.UserDatabaseServiceConfiguration.SaveConfiguration();
            Console.ReadLine();
        }
    }
}
