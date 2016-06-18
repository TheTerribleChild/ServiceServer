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
        public const string DATABASE_NAME = "UserDatabase";

        private static UserDatabaseService Instance { get; set; }

        public override string ServiceName
        {
            get
            {
                return "UserDatabaseService";
            }
        }

        public static UserDatabaseService GetInstance()
        {
            if (Instance == null)
                Instance = new UserDatabaseService();
            return Instance;
        }

        private UserDatabaseService() : base()
        {
            
        }

        public override bool SetServiceLocation(string serviceLocation)
        {
            if (!base.SetServiceLocation(serviceLocation))
                return false;
            string databaseLocation = Path.Combine(serviceLocation, DATABASE_NAME + ".mdf");
            return Utility.DatabaseUtility.ConnectDatabase(new UserDatabaseContainer(), DATABASE_NAME, databaseLocation);
        }

        public override bool StartServiceAsync()
        {
            using (var udc = new UserDatabaseContainer())
            {
                udc.Users.Add(new User() { Email = "a@gmail", PasswordHash = 123, UserName = "aaa" });
                udc.SaveChanges();
            }

            using (var udc = new UserDatabaseContainer())
            {
                User[] users = udc.Users.ToArray<User>();
                foreach (User u in users)
                    Console.WriteLine(u.Id + " " + u.Email);
            }
            return true;
        }

        protected override void Do()
        {
            Thread.Sleep(500);
        }

        //==================================================================

        public bool AddUser(string userEmail, string userName, int passwordHash)
        {
            if (CurrentServiceState == ServiceState.Off)
                return false;

            bool isValid = false;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    isValid = true;
                }
            }
            catch(Exception ex)
            {
                
            }

            return isValid;
        }

        public bool UpdateUser(int id)
        {
            if (CurrentServiceState == ServiceState.Off)
                return false;

            bool isValid = false;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {

            }

            return isValid;
        }

        public bool DeleteUser(int id)
        {
            if (CurrentServiceState == ServiceState.Off)
                return false;

            bool isValid = false;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {

            }

            return isValid;
        }


    }
}
