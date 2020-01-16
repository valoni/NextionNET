using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayConfigurationGenerator.Models.HMI
{
    public class Page
    {
        public string Id { get; set; }

        public List<Component> Components { get; set; }
    }
}
