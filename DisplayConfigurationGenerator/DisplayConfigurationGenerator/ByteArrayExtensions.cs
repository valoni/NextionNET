using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayConfigurationGenerator
{
    public static class ByteArrayExtensions
    {
        public static byte GetByte(this byte[] data, int index)
        {
            return data[index];
        }

        public static ushort GetUShort(this byte[] data, int index)
        {
            return (ushort)((data[index + 1] << 8) + data[index]);
        }

        public static uint GetUInt(this byte[] data, int index)
        {
            return (uint)((data[index + 3] << 24) + (data[index + 2] << 16) + (data[index + 1] << 8) + (data[index]));
        }

        public static string GetString(this byte[] data, int index, int maxSize)
        {
            int length;
            for (length = 0; length < maxSize; length++)
            {
                if (data[index + length] == 0)
                    break;
            }

            if (length == 0)
                return string.Empty;

            return Encoding.Default.GetString(data, index, length);
        }

        public static byte[] GetBytes(this byte[] data, int index, int lenght)
        {
            byte[] result = new byte[lenght];
            Array.Copy(data, index, result, 0, lenght);
            return result;
        }

        public static bool StartsWith(this byte[] data, string text)
        {
            byte[] textBytes = Encoding.Default.GetBytes(text);
            if (data.Length < textBytes.Length)
                return false;

            for(int i = 0;i < textBytes.Length;i++)
            {
                if (data[i] != textBytes[i])
                    return false;
            }

            return true;
        }

        public static string ToString(this byte[] data, int index, int length, bool encode = false)
        {
            if (encode)
            {
                return Encoding.Default.GetString(data, index, length);
            }

            var sb = new StringBuilder();
            for (int i = index; i < index + length; i++)
            {
                sb.AppendFormat("{0:X2}", data[i]);
            }
            return sb.ToString();
        }
    }
}
