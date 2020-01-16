using System;
using Microsoft.SPOT;
using System.Text;

namespace JernejK.NextionNET.Driver
{
    public class NextUtils
    {
        internal static void FromX1X2ToXAndLenght(int x1, int x2, out int x, out int lenght)
        {
            if (x1 <= x2)
            {
                x = x1;
                lenght = x2 - x1;
            }
            else
            {
                x = x2;
                lenght = x2 - x1;
            }
        }

        /// <summary>
        /// Add escape characters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EscapeString(string value)
        {
            if (value == null)
                return null;

            StringBuilder sb = new StringBuilder(value);
            sb.Replace("\"", "\\\"");

            return sb.ToString();
        }
    }
}
