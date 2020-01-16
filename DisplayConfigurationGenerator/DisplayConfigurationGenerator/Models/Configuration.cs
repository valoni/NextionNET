using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayConfigurationGenerator.Models
{
    public class Configuration
    {
        public string HMIFile { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string OutputFile { get; set; }
    }
}
