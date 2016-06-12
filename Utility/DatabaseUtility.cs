using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class DatabaseUtility
    {
        public static bool ConnectDatabase(DbContext context, string dbName, string dbFileName)
        {
            try
            {
                string connectionString = String.Format(@"Data Source=(LocalDB)\mssqllocaldb;AttachDBFileName={1};Initial Catalog={0};Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=5", dbName, dbFileName);
                context.Database.Connection.ConnectionString = connectionString;
                if (!File.Exists(dbFileName))
                {
                    DetatchDatabase(dbName);
                    if (context.Database.Exists())
                    {
                        Console.WriteLine("Already exist, deleting...");
                        context.Database.Delete();
                    }

                    context.Database.Create();
                }
                return true;

            }
            catch (System.Data.SqlClient.SqlException e)
            {
                Console.WriteLine("Failed to create db");
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public static bool DetatchDatabase(string dbName)
        {
            try
            {
                string connectionString = String.Format(@"Data Source=(LocalDB)\mssqllocaldb;Initial Catalog=master;Integrated Security=True");
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = String.Format("exec sp_detach_db '{0}'", dbName);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
