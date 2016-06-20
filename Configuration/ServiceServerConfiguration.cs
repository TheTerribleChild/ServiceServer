using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class ServiceServerConfiguration : Configuration<ServiceServerConfiguration>
    {
        public static string SERVICE_SERVER_LOCATION { get; set; } = @"D:\ServiceServer";
        public static bool DEBUG_MODE { get; set; } = true;

        public override string ConfigurationFileLocation
        {
            get
            {
                return Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ServerService", "ServiceServer.config"); ;
            }
        }

        public ServiceServerConfiguration(){
            Console.WriteLine(JsonConvert.SerializeObject(this));
        }
    }
}
