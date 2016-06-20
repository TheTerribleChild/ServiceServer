using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace UserDatabaseService
{
    public static class UserQueryEngine
    {
        private static UserDatabaseService service = UserDatabaseService.GetInstance();

        public static User GetUserById(int id)
        {
            if (service.CurrentServiceState == BaseService.ServiceState.Off)
                return null;

            User returnUser = null;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    User userById = udc.Users.FirstOrDefault<User>(u => u.Id == id);
                    if (userById != null)
                    {
                        returnUser = new User() { Id = userById.Id, Email = userById.Email, UserName = userById.UserName, PasswordHash = userById.PasswordHash };
                        Utility.LogUtility.Log(service, LogType.INFO, String.Format("GetUserById{{0}, {1}, {2}}", userById.Id, userById.Email, userById.UserName));
                    }
                    else
                    {
                        Utility.LogUtility.Log(service, LogType.INFO, String.Format("Get User[ID={0}] Failed: Invalid user ID", userById.Id));
                    }
                }
            }
            catch(Exception ex)
            {
                Utility.LogUtility.Log(service, LogType.ERROR, "Get User Failed: " + ex);
            }

            return returnUser;
        }

        public static User GetUserByEmail(string email)
        {
            if (service.CurrentServiceState == BaseService.ServiceState.Off)
                return null;

            User returnUser = null;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    User userByEmail = udc.Users.FirstOrDefault<User>(u => u.Email == email);
                    if (userByEmail != null)
                    {
                        returnUser = new User() { Id = userByEmail.Id, Email = userByEmail.Email, UserName = userByEmail.UserName, PasswordHash = userByEmail.PasswordHash };
                        Utility.LogUtility.Log(service, LogType.INFO, String.Format("GetUserById{{0}, {1}, {2}}", userByEmail.Id, userByEmail.Email, userByEmail.UserName));
                    }
                    else
                    {
                        Utility.LogUtility.Log(service, LogType.INFO, String.Format("Get User[ID={0}] Failed: Invalid user ID", userByEmail.Id));
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.LogUtility.Log(service, LogType.ERROR, "Get User Failed: " + ex);
            }

            return returnUser;
        }

        public static User GetUserByUserName(string userName)
        {
            if (service.CurrentServiceState == BaseService.ServiceState.Off)
                return null;

            User returnUser = null;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    User userByUserName = udc.Users.FirstOrDefault<User>(u => u.UserName == userName);
                    if (userByUserName != null)
                    {
                        returnUser = new User() { Id = userByUserName.Id, Email = userByUserName.Email, UserName = userByUserName.UserName, PasswordHash = userByUserName.PasswordHash };
                        Utility.LogUtility.Log(service, LogType.INFO, String.Format("GetUserById{{0}, {1}, {2}}", userByUserName.Id, userByUserName.Email, userByUserName.UserName));
                    }
                    else
                    {
                        Utility.LogUtility.Log(service, LogType.INFO, String.Format("Get User[ID={0}] Failed: Invalid user ID", userByUserName.Id));
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.LogUtility.Log(service, LogType.ERROR, "Get User Failed: " + ex);
            }

            return returnUser;
        }
    }
}
