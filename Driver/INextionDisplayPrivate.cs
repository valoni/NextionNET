using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver
{
    internal interface INextionDisplayPrivate
    {
        CommandBuffer.Command SendCommand(string command, bool waitForResponse);
    }
}
