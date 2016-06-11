using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BaseService;

namespace UserDatabaseService
{
    public class UserDatabaseService : BaseService.BaseService
    {

        private static UserDatabaseService Instance { get; set; }

        public static UserDatabaseService GetInstance()
        {
            if (Instance == null)
                Instance = new UserDatabaseService();
            return Instance;
        }

        private UserDatabaseService() : base()
        {
            Console.WriteLine("UserDatabaseService Created");
        }

        protected override void Do()
        {
            Console.WriteLine("UserDatabaseService doing");
            Thread.Sleep(500);
        }
    }
}
