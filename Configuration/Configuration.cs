using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class StaticPropertyContractResolver : DefaultContractResolver
{
    protected override List<MemberInfo> GetSerializableMembers(Type objectType)
    {
        var baseMembers = base.GetSerializableMembers(objectType);
        PropertyInfo[] allProperty = objectType.GetProperties();
        baseMembers.Clear();
        baseMembers.AddRange(allProperty);
        return baseMembers;
    }
}

namespace Configuration
{
    public abstract class Configuration<T> : INotifyPropertyChanged where T : Configuration<T>, new()
    {
        private static T _instance;
        public event PropertyChangedEventHandler PropertyChanged;
        protected static JsonSerializer jsonSerializer { get; } = JsonSerializer.Create(new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, ContractResolver = new StaticPropertyContractResolver() }); 

        public abstract string ConfigurationFileLocation { get; }


        public static T GetInstance()
        {
            if (_instance == null)
            {
                _instance = new T();
                if (_instance.ConfigurationFileLocation != null)
                {
                    
                    T loadInstance = LoadConfiguration(_instance.ConfigurationFileLocation);
                    
                    if (loadInstance != null)
                        _instance = loadInstance;
                }
            }

            return _instance;
        }

        protected Configuration()
        {

        }

        public static void LoadConfiguration()
        {
            GetInstance();
        }

        private static T LoadConfiguration(string configurationFileLocation)
        {
            T configuration = null;
            try
            {
                if (File.Exists(configurationFileLocation))
                {
                    using (StreamReader file = new StreamReader(File.OpenRead(configurationFileLocation)))
                    using (JsonTextReader jsonReader = new JsonTextReader(file))
                    {
                        configuration = jsonSerializer.Deserialize<T>(jsonReader);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return configuration;
        }

        public static bool SaveConfiguration()
        {
            bool succeeded = false;
            try
            {
                if(_instance.ConfigurationFileLocation == null || !Directory.Exists(Path.GetDirectoryName(_instance.ConfigurationFileLocation)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_instance.ConfigurationFileLocation));
                }
                using (StreamWriter file = File.CreateText(_instance.ConfigurationFileLocation))
                using (JsonTextWriter jsonWriter = new JsonTextWriter(file))
                {
                    jsonSerializer.Serialize(jsonWriter, _instance, _instance.GetType());
                }
                succeeded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return succeeded;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
