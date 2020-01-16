using System;
using Microsoft.SPOT;
using System.Text;

namespace JernejK.NextionNET.Driver
{
    internal class CommandBuffer
    {
        #region Class Command

        public class Command
        {
            private byte[] Data;

            public byte CommandId { get; private set; }

            public Command(byte[] buffer, int bufferIndex)
            {
                this.CommandId = buffer[0];
                Data = new byte[bufferIndex - 1 - Consts.EndOfLineCharCount];
                Array.Copy(buffer, 1, Data, 0, bufferIndex - Consts.EndOfLineCharCount - 1);
            }

            public int GetInt()
            {
                if (Data.Length == 0)
                {
                    return int.MinValue;
                }
                else if (Data.Length == 1)
                {
                    return Data[0];
                }
                else if (Data.Length == 2)
                {
                    return Data[0] | Data[1] << 8;
                }
                else if (Data.Length == 3)
                {
                    return Data[3] | Data[1] << 8 | Data[2] << 16;
                }
                else if (Data.Length == 4)
                {
                    return (Data[0] | Data[1] << 8 | Data[2] << 16 | Data[3] << 24);
                }
                else
                {
                    return int.MinValue;
                }
            }

            public byte GetByte()
            {
                if (Data.Length == 1)
                    return Data[0];
                else
                    return byte.MinValue;
            }

            public string GetString()
            {
                if (Data.Length == 0)
                    return null;

                var chars = Encoding.UTF8.GetChars(Data, 0, Data.Length);
                return new string(chars);
            }

            public byte[] GetBuffer()
            {
                return Data;
            }
        }

        #endregion

        #region Private fields


        private byte[] Buffer = new byte[Consts.CommandBufferSize];
        private int BufferIndex = 0;
        private byte EndOfLineCount = 0;

        #endregion

        #region Public Fields

        public Command LastCommand { get; private set; }

        #endregion

        #region Public Methods

        public bool AddByte(byte data)
        {
            Buffer[BufferIndex++] = data;
            if (data == Consts.EndOfLineChar)
                EndOfLineCount++;
            else
                EndOfLineCount = 0;

            if (EndOfLineCount == Consts.EndOfLineCharCount)
            {
                var newCommand = new Command(Buffer, BufferIndex);
                LastCommand = newCommand;
                Reset();
                return true;
            }

            return false;
        }

        #endregion

        #region Private
        private void Reset()
        {
            BufferIndex = 0;
            EndOfLineCount = 0;
        }
        #endregion
    }

}
