using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver
{
    internal static class Consts
    {
        public const int CommandBufferSize = 500;
        public const int SendBufferSize = 50;
        public const byte EndOfLineChar = 0xFF;
        public const byte EndOfLineCharCount = 3;

#if DEBUG
        public const int WaitForDisplayResponseMS = int.MaxValue;
#else
        public const int WaitForDisplayResponseMS = 2000;
#endif
    }
}
