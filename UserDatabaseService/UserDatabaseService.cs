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
        public UserDatabaseService() : base()
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
