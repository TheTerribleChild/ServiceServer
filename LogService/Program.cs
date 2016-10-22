using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using System.IO;
using CommandLine.Text;
using Utility;

namespace LogService
{
    class Program
    {
        class Options
        {
            [Option('p', "port", Required = false, DefaultValue = -1, HelpText = "Specify port number. Leave empty for automatic port pick" )]
            public int Port { get; set; }

            [Option('d', "directory", Required = false, DefaultValue = null, HelpText = "Specify a log directory location containing root")]
            public string ServiceDirectory { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }

        static void Main(string[] args)
        {
            Options options = new Options();
            int webServicePortNumber = -1;
            string logDirectoryLocation = null;

            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                //Check for available port number
                if (options.Port == -1)
                {
                    webServicePortNumber = WebUtility.GetNextAvailablePortNumber();
                    if (!WebUtility.IsPortOpen(webServicePortNumber))
                    {
                        Console.WriteLine("No ports available");
                        return;
                    }
                }
                else
                {
                    if (WebUtility.IsPortOpen(options.Port))
                        webServicePortNumber = options.Port;
                    else
                    {
                        Console.WriteLine("Port " + options.Port + " is not available.");
                        return;
                    }
                }

                //Check for valid path
                try
                {
                    if (Path.IsPathRooted(options.ServiceDirectory))
                    {
                        if(!Directory.Exists(options.ServiceDirectory))
                            Directory.CreateDirectory(options.ServiceDirectory);
                    }
                    logDirectoryLocation = options.ServiceDirectory;
                }
                catch
                {
                    Console.WriteLine("Invalid directory path");
                    return;
                }
            }
            else{
                Console.WriteLine("Invalid Arguement " + options.GetUsage());
                return;
            }

            //Start Log Service Here

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
