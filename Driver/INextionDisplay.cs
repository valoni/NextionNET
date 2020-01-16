using System;
namespace JernejK.NextionNET.Driver
{
    /// <summary>
    /// Nextion display interface
    /// </summary>
    public interface INextionDisplay : IDisposable
    {
        byte Backlight { get; set; }
        ControlCollection Controls { get; }
        NextionDisplay.GUIDesign GUI { get; }
        int Height { get; }
        byte PageId { get; set; }
        void RefreshComponent(string id);
        void SetPage(string pageName);
        void SetSleepMode(bool sleepMode);
        object SyncObject { get; }
        event NextionDisplay.TouchEventDelegate TouchEvent;
        TouchMode TouchMode { get; set; }
        event NextionDisplay.TouchEventXYDelegate TouchXYEvent;
        int Width { get; }
    }
}
