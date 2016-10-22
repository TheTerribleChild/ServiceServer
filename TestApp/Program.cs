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
            UserDatabaseService.UserDatabaseServiceConfiguration.LoadConfiguration();
            Console.ReadKey();
        }
    }
}
