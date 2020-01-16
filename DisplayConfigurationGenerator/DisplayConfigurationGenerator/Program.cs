using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayConfigurationGenerator
{
    class Program
    {
        private enum ConfigurationSwitch
        {
            Undefined,
            Namespace,
            Class,
            Output,
        }
        static Models.Configuration GetConfiguraion(string[] args)
        {
            if (args.Length == 0)
                return null;

            var configuration = new Models.Configuration()
            {
                HMIFile = args[0],
                ClassName = "DisplayConfiguration",
                Namespace = "JernejK.NextionNET",
                OutputFile = "DisplayConfiguration.generated.cs"
            };

            ConfigurationSwitch lastSwitch = ConfigurationSwitch.Undefined;
            for (int i = 0;i < args.Length;i++)
            {
                string arg = args[i];
                if (lastSwitch != ConfigurationSwitch.Undefined)
                {
                    switch(lastSwitch)
                    {
                        case ConfigurationSwitch.Namespace:
                            configuration.Namespace = arg;
                            break;
                        case ConfigurationSwitch.Class:
                            configuration.ClassName = arg;
                            break;
                        case ConfigurationSwitch.Output:
                            configuration.OutputFile = arg;
                            break;
                    }

                    lastSwitch = ConfigurationSwitch.Undefined;
                    continue;
                }

                switch(arg.ToUpper())
                {
                    case "/NS":
                        lastSwitch = ConfigurationSwitch.Namespace;
                        break;
                    case "/CN":
                        lastSwitch = ConfigurationSwitch.Class;
                        break;
                    case "/O":
                        lastSwitch = ConfigurationSwitch.Output;
                        break;
                }
            }

            return configuration;
        }

        static int Main(string[] args)
        {
            var configuration = GetConfiguraion(args);
            if (configuration == null)
            {
                Console.Write("Usage DisplayConfigurationGenerator.exe PathToHmiFile OptionalParameters (for example /ns Test.Display");
                Console.Write("OptionalParameters:");
                Console.Write("/ns - Namespace");
                Console.Write("/cn - Class name");
                Console.Write("/o - Output file name");
                return -1;
            }

            if (!File.Exists(configuration.HMIFile))
            {
                Console.WriteLine("HMI file not exsists");
                return -2;
            }

            var hmiParser = new HMIParser(File.ReadAllBytes(configuration.HMIFile));
            var display = hmiParser.Parse();
            if (display == null)
            {
                Console.WriteLine("Invalid HMI file");
                return -3;
            }

            var classGenerator = new ClassGenerator();
            string classCode = classGenerator.GenerateClass(configuration, display);
            File.WriteAllText(configuration.OutputFile, classCode);

            return 0;       
        }
    }
}
