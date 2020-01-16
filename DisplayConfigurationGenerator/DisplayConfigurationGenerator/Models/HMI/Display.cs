using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayConfigurationGenerator.Models.HMI
{
    public class Display
    {
        public bool OrientationVertical { get; set; }
        public byte VersionMajor { get; set; }
        public byte VersionMinor { get; set; }
        public ushort ResolutionHorizontal { get; set; }
        public ushort ResolutionVertical { get; set; }

        public List<Page> Pages { get; set; }

        public List<Font> Fonts { get; set; }

        public List<Image> Images { get; set; }
    }
}
