using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver
{
    public enum SystemEventType : byte
    {
        AutomaticSleepMode = 0x86,
        AutomaticWakeUp = 0x87,
    }
}
