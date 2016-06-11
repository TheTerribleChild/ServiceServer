using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BaseService;
using System.IO;
using Utility;

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

            string dbFileName = Path.Combine(@"D:\ServiceServer\UserDatabaseService", "UserDatabase.mdf");
            string dbName = "UserEntity";
            Utility.DatabaseUtility.ConnectDatabase(new UserDatabaseContainer(), dbName, dbFileName);
            using (var udc = new UserDatabaseContainer())
            {
                udc.Users.Add(new User() { Id = 1, Email = "a@gmail", PasswordHash = 123, UserName = "aaa" });
                udc.SaveChanges();
            }

            using (var udc = new UserDatabaseContainer())
            {
                User[] users = udc.Users.ToArray<User>();
                foreach(User u in users)
                    Console.WriteLine(u.Id + " " + u.Email);
            }
        }

        protected override void Do()
        {
            Console.WriteLine("UserDatabaseService doing");
            Thread.Sleep(500);
        }
    }
}
