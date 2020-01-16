using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayConfigurationGenerator
{
    public static class StringBuilderExtensions
    {
        public static void AppendFormatLine(this StringBuilder sb, string value, params object[] args)
        {
            sb.AppendFormat(value, args);
            sb.AppendLine();
        }
    }
}
