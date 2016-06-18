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
    public class UserDatabaseService : BaseService.BaseService, ILogable
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

        public string LogName
        {
            get
            {
                return "UserDatabaseService";
            }
        }

        public string LogIntro()
        {
            return "User Database Service";
        }

        public static UserDatabaseService GetInstance()
        {
            if (Instance == null)
                Instance = new UserDatabaseService();
            return Instance;
        }

        private UserDatabaseService() : base()
        {
            Utility.LogUtility.Log(this, LogType.INFO, "User Database Service created");
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
            Utility.LogUtility.Log(this, LogType.INFO, "User Database Service started");
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
                Utility.LogUtility.Log(this, LogType.ERROR, "Add User Failed: " + ex.StackTrace);
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
                Utility.LogUtility.Log(this, LogType.ERROR, "Update User Failed: " + ex.StackTrace);
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
                Utility.LogUtility.Log(this, LogType.ERROR, "Delete User Failed: " + ex.StackTrace);
            }

            return isValid;
        }
    }
}
