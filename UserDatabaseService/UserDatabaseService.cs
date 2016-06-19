using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BaseService;
using System.IO;
using Utility;
using System.ComponentModel;

namespace UserDatabaseService
{
    public class UserDatabaseService : BaseService.BaseService<UserDatabaseService>, ILogable
    {
        public const string DATABASE_NAME = "UserDatabase";

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

        public UserDatabaseService() : base()
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

        public override bool StopServiceAsync()
        {
            Utility.LogUtility.Log(this, LogType.INFO, "User Database Service stopped");
            return true;
        }

        protected override void Do()
        {
            Thread.Sleep(500);
        }

        //==================================================================

        public bool AddUser(string email, string userName, int passwordHash)
        {
            if (CurrentServiceState == ServiceState.Off)
                return false;

            bool isValid = false;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    if(!udc.Users.Any(u => u.Email == email) && !udc.Users.Any(u => u.UserName == userName))
                    {
                        User newUser = new User() { Email = email, UserName = userName, PasswordHash = passwordHash };
                        udc.Users.Add(newUser);
                        udc.SaveChanges();
                        isValid = true;
                        Utility.LogUtility.Log(this, LogType.INFO, String.Format("Added User{{0}, {1}, {2}}", newUser.Id, newUser.Email, newUser.UserName));
                    }
                }
            }
            catch(Exception ex)
            {
                Utility.LogUtility.Log(this, LogType.ERROR, "Add User Failed: " + ex.StackTrace);
            }

            return isValid;
        }

        public bool UpdateUser(int id, string email, string userName, int passwordHash)
        {
            if (CurrentServiceState == ServiceState.Off)
                return false;

            bool isValid = false;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    User userById = udc.Users.FirstOrDefault<User>(u => u.Id == id);
                    User userByEmail = udc.Users.FirstOrDefault<User>(u => u.Email == email);
                    User userByUserName = udc.Users.FirstOrDefault<User>(u => u.UserName == userName);

                    if (userById != null)
                    {
                        if(userByEmail != null && userById.Id != userByEmail.Id)
                        {
                            Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User[ID={0}] Failed: Email[email={1}] already exists", userById.Id, email));
                        }
                        else if(userByUserName != null && userById.Id != userByUserName.Id)
                        {
                            Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User[ID={0}] Failed: User Name[UserName={1}] already exists", userById.Id, userName));
                        }
                        else
                        {
                            userById.Email = email;
                            userById.UserName = userName;
                            userById.PasswordHash = passwordHash;
                            udc.SaveChanges();
                            isValid = true;
                            Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User{{0}, {1}, {2}}", userById.Id, userById.Email, userById.UserName));
                        }
                    }
                    else
                    {
                        Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User[ID={0}] Failed: Invalid user ID", userById.Id));
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Utility.LogUtility.Log(this, LogType.ERROR, "Update User Failed: " + ex.StackTrace);
            }

            return isValid;
        }

        public bool UpdateUserEmail(int id, string email)
        {
            if (CurrentServiceState == ServiceState.Off)
                return false;

            bool isValid = false;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    User userById = udc.Users.FirstOrDefault<User>(u => u.Id == id);
                    User userByEmail = udc.Users.FirstOrDefault<User>(u => u.Email == email);

                    if (userById != null)
                    {
                        if (userByEmail != null && userById.Id != userByEmail.Id)
                        {
                            Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User Email[ID={0}] Failed: Email[email={1}] already exists", userById.Id, email));
                        }
                        else
                        {
                            userById.Email = email;
                            udc.SaveChanges();
                            isValid = true;
                            Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User Email{{0}, {1}, {2}}", userById.Id, userById.Email, userById.UserName));
                        }
                    }
                    else
                    {
                        Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User[ID={0}] Failed: Invalid user ID", userById.Id));
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.LogUtility.Log(this, LogType.ERROR, "Update User Failed: " + ex.StackTrace);
            }

            return isValid;
        }

        public bool UpdateUserName(int id, string userName)
        {
            if (CurrentServiceState == ServiceState.Off)
                return false;

            bool isValid = false;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    User userById = udc.Users.FirstOrDefault<User>(u => u.Id == id);
                    User userByUserName = udc.Users.FirstOrDefault<User>(u => u.UserName == userName);

                    if (userById != null )
                    {
                        if (userByUserName != null && userById.Id != userByUserName.Id)
                        {
                            Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User[ID={0}] Failed: User Name[UserName={1}] already exists", userById.Id, userName));
                        }
                        else
                        {
                            userById.UserName = userName;
                            udc.SaveChanges();
                            isValid = true;
                            Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User Name{{0}, {1}, {2}}", userById.Id, userById.Email, userById.UserName));
                        }
                    }
                    else
                    {
                        Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User[ID={0}] Failed: Invalid user ID", userById.Id));
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.LogUtility.Log(this, LogType.ERROR, "Update User Failed: " + ex.StackTrace);
            }

            return isValid;
        }

        public bool UpdateUserPassword(int id, int passwordHash)
        {
            if (CurrentServiceState == ServiceState.Off)
                return false;

            bool isValid = false;

            try
            {
                using (UserDatabaseContainer udc = new UserDatabaseContainer())
                {
                    User userById = udc.Users.FirstOrDefault<User>(u => u.Id == id);
                    if (userById != null)
                    {
                        userById.PasswordHash = passwordHash;
                        udc.SaveChanges();
                        isValid = true;
                        Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User Password{{0}, {1}, {2}}", userById.Id, userById.Email, userById.UserName));
                    }
                    else
                    {
                        Utility.LogUtility.Log(this, LogType.INFO, String.Format("Updated User[ID={0}] Failed: Invalid user ID", userById.Id));
                    }
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
                    User user = udc.Users.FirstOrDefault<User>(u => u.Id == id);
                    if (user != null)
                    {
                        udc.Users.Remove(user);
                        udc.SaveChanges();
                        isValid = true;
                        Utility.LogUtility.Log(this, LogType.INFO, String.Format("Deleted User{{0}, {1}, {2}}", user.Id, user.Email, user.UserName));
                    }
                    else
                    {
                        Utility.LogUtility.Log(this, LogType.INFO, String.Format("Delete User[ID={0}] Failed: Invalid user ID", id));
                    }
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
