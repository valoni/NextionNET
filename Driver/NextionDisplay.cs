using System;
using Microsoft.SPOT;
using System.IO.Ports;
using System.Threading;
using System.Text;

namespace JernejK.NextionNET.Driver
{
    public partial class NextionDisplay : JernejK.NextionNET.Driver.INextionDisplay, INextionDisplayPrivate
    {
        #region Events

        public delegate void TouchEventDelegate(object sender, TouchEventArgs args);
        /// <summary>
        /// Component touch event
        /// </summary>
        public event TouchEventDelegate TouchEvent;

        public delegate void TouchEventXYDelegate(object sender, TouchEventXYArgs args);
        /// <summary>
        /// Display touch event (coordinate)
        /// </summary>
        public event TouchEventXYDelegate TouchXYEvent;

        public delegate void SystemEventDelegate(object sender, SystemEventType eventType, SystemEventParameter parameter);
        /// <summary>
        /// Nextion system events
        /// </summary>
        public event SystemEventDelegate SystemEvent;

        #endregion

        #region Private fields

        private SerialPort Serial;

        private CommandBuffer ReceiveBuffer = new CommandBuffer();
        private byte[] SendBuffer = new byte[Consts.SendBufferSize];
        private AutoResetEvent NewResponseEvent = new AutoResetEvent(false);
        private CommandBuffer.Command LastCommand = null;
        private Thread SerialDataReceiveThread;
        private object Sync = new object();

        #endregion

        #region Ctor

        /// <summary>
        /// Create Nextion driver without specifying screen resolution. Some methods might not work.
        /// </summary>
        /// <param name="serial"></param>
        public NextionDisplay(SerialPort serial) : this(serial, 0, 0)
        {
        }

        /// <summary>
        /// Default Nextion constructor.
        /// </summary>
        /// <param name="serial">Serial port where Nextion display is connected</param>
        /// <param name="width">Screen width</param>
        /// <param name="height">Screen height</param>
        public NextionDisplay(SerialPort serial, int width, int height)
        {
#if DEBUG
            Debug.Print("Nextion debug output enabled");
#endif
            Controls = new ControlCollection(this);
            GUI = new GUIDesign(this);

            Height = height;
            Width = width;

            Serial = serial;

            Serial.Open();

            //Respond to every first command is illegal instruction, so we send dummy reqest first
            SendCommand(Serial, SendBuffer, "");

            Thread.Sleep(100);

            //Clean buffer (illegal instuction response from previous command)
            Serial.DiscardInBuffer();
            Serial.DiscardOutBuffer();

            SerialDataReceiveThread = new Thread(new ThreadStart(Serial_DataReceived));
            SerialDataReceiveThread.Start();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Screen height
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Screen width
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Control collection
        /// </summary>
        public ControlCollection Controls { get; private set; }

        /// <summary>
        /// Direct access to GUI
        /// </summary>
        public GUIDesign GUI { get; private set; }

        /// <summary>
        /// Access to internal sync object. Use it if you have multithread environment and you need to run complete instruction batch at once.
        /// </summary>
        public object SyncObject
        {
            get { return Sync; }
        }

        /// <summary>
        /// Current page id
        /// </summary>
        public byte PageId
        {
            get
            {
                var response = SendCommand("sendme", true);
                EnsureResponseCommandId(response, 0x66);
                return response.GetByte();
            }
            set
            {
                SendCommand("page " + value, false);
            }
        }

        /// <summary>
        /// Ger or set the way screen process touch events.
        /// </summary>
        public TouchMode TouchMode
        {
            get { return (TouchMode)GetVariableInt("sendxy"); }
            set { SetVariable("sendxy", (int)value); }
        }

        /// <summary>
        /// Get or set display backlight % (0-100)
        /// </summary>
        public byte Backlight
        {
            get { return (byte)GetVariableInt("dim"); }
            set
            {
                if (value > 100)
                    throw new ArgumentException();

                SetVariable("dim", value);
            }
        }

        /// <summary>
        /// If no serial data, it automatically activates sleep time (unit: second, minimum 3, maximum 65,535, power-on default 0). You can't read variable if display is sleeping!
        /// </summary>
        public int SleepAfterNoSerialDataSeconds
        {
            get { return GetVariableInt("ussp"); }
            set 
            {
                if (value > 0 && value < 3)
                    throw new ArgumentException("SleepAfterNoSerialDataSeconds");

                SetVariable("ussp", value); }
        }

        /// <summary>
        /// If no touch operation, it automatically enters into sleep time (unit: second, minimum 3, maximum 65,535, power-on default 0). You can't read variable if display is sleeping!
        /// </summary>
        public int SleepAfterNoTouchSeconds
        {
            get { return GetVariableInt("thsp"); }
            set
            {
                if (value > 0 && value < 3)
                    throw new ArgumentException("SleepAfterNoTouchSeconds");

                SetVariable("thsp", value);
            }
        }

        /// <summary>
        /// Touch in sleep mode will auto-awake switch
        /// </summary>
        public bool TouchInSleepWillWakeUp
        {
            get { return GetVariableInt("thup") == 1; }
            set { SetVariable("thup", value ? 1 : 0); }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Set current page by name
        /// </summary>
        /// <param name="pageName"></param>
        public void SetPage(string pageName)
        {
            SendCommand("page " + pageName, false);
        }

        /// <summary>
        /// Refresh selected component
        /// </summary>
        /// <param name="id"></param>
        public void RefreshComponent(string id)
        {
            SendCommand("ref " + id, false);
        }

        /// <summary>
        /// Set sleep mode
        /// </summary>
        /// <param name="sleepMode"></param>
        public void SetSleepMode(bool sleepMode)
        {
            SetVariable("sleep", sleepMode ? 1 : 0);
        }

        /// <summary>
        /// Read integer value of attribute.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public int GetAttributeValueInt(string id, string attribute)
        {
            var response = SendCommand("get " + id + "." + attribute, true);
            EnsureResponseCommandId(response, 0x71);
            return response.GetInt();
        }

        /// <summary>
        /// Read string value of attribute.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string GetAttributeValueString(string id, string attribute)
        {
            var response = SendCommand("get " + id + "." + attribute, true);
            EnsureResponseCommandId(response, 0x70);
            return response.GetString();
        }

        /// <summary>
        /// Set attribute value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public void SetAttributeValue(string id, string attribute, string value)
        {
            SendCommand(id + "." + attribute + "=\"" + value + "\"", false);
        }

        /// <summary>
        /// Set attribute value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public void SetAttributeValue(string id, string attribute, int value)
        {
            SendCommand(id + "." + attribute + "=" + value, false);
        }

        /// <summary>
        /// Enable or disable Nextion command processor
        /// </summary>
        /// <param name="enabled"></param>
        public void SetCommandProcessorState(bool enabled)
        {
            SendCommand(enabled ? "com_star" : "com_stop", false);
        }

        /// <summary>
        /// Refresh all components of the current page
        /// </summary>
        public void RefreshAll()
        {
            SendCommand("ref 0", false);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get value of integer value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetVariableInt(string id)
        {
            var response = SendCommand("get " + id, true);
            EnsureResponseCommandId(response, 0x71);

            return response.GetInt();
        }

        private void SetVariable(string id, int value)
        {
            SendCommand(id + "=" + value, false);
        }

        private string ProcessCommonErrors(byte commandId, bool throwException)
        {
            string error = null;
            switch (commandId)
            {
                case 0x00:
                    error = "Invalid instruction";
                    break;
                case 0x03:
                    error = "Page ID invalid";
                    break;
                case 0x04:
                    error = "Picture ID invalid";
                    break;
                case 0x05:
                    error = "Font ID invalid";
                    break;
                case 0x1A:
                    error = "Variable name invalid";
                    break;
                case 0x1B:
                    error = "Variable operation invalid";
                    break;
            }

            if (throwException && error != null)
                throw new Exception(error);

            return error;
        }

        private void EnsureResponseCommandId(CommandBuffer.Command command, byte expectedId)
        {
            EnsureResponseCommandId(command.CommandId, expectedId);
        }

        private void EnsureResponseCommandId(byte commandId, byte expectedId)
        {
            if (commandId != expectedId)
                throw new Exception("Invalid response code: " + expectedId);
        }

        private CommandBuffer.Command SendCommand(string command, bool waitForResponse)
        {
#if DEBUG
            Debug.Print("Thread" + Thread.CurrentThread.ManagedThreadId + " sending command: " + command);
#endif
            lock (Sync)
            {
                if (waitForResponse)
                {
                    NewResponseEvent.Reset();
                }

                SendCommand(Serial, SendBuffer, command);

#if DEBUG
                Debug.Print("Thread" + Thread.CurrentThread.ManagedThreadId + " sent");
#endif

                if (!waitForResponse)
                    return null;

#if DEBUG
                Debug.Print("Thread" + Thread.CurrentThread.ManagedThreadId + " waiting to response");
#endif
                if (!NewResponseEvent.WaitOne(Consts.WaitForDisplayResponseMS, false))
                    throw new Exception("Display is not responding");

#if DEBUG
                Debug.Print("Thread" + Thread.CurrentThread.ManagedThreadId + " response received");
#endif

                var response = LastCommand;
                if (response == null)
                    throw new Exception("No response received. Check debug output for errors");

                ProcessCommonErrors(response.CommandId, true);
                return response;
            }
        }

        private void Serial_DataReceived()
        {
            while (true)
            {
                if (ReceiveBuffer.AddByte((byte)Serial.ReadByte()))
                {
                    var newCommand = ReceiveBuffer.LastCommand;

                    string error = ProcessCommonErrors(newCommand.CommandId, false);
                    if (error != null)
                    {
                        LastCommand = null;
                        NewResponseEvent.Set();
                        Debug.Print("Nextion error: " + error);
                        continue;
                    }

                    switch (newCommand.CommandId)
                    {
                        case 0x65:
#if DEBUG
                            Debug.Print("New touch event - component");
#endif
                            ProcessTouchComponent(newCommand);
                            break;
                        case 0x67:
#if DEBUG
                            Debug.Print("New touch event - XY");
#endif
                            ProcessTouchXY(newCommand);
                            break;
                        case 0x68:
#if DEBUG
                            Debug.Print("New touch event - Sleep mode");
#endif
                            ProcessTouchXY(newCommand);
                            break;
                        case (byte)SystemEventType.AutomaticSleepMode:
                        case (byte)SystemEventType.AutomaticWakeUp:
#if DEBUG
                            Debug.Print("System event");
#endif
                            ProcessSystemEvent(newCommand);
                            break;
                        default:
                            LastCommand = newCommand;
#if DEBUG
                            Debug.Print("New command received: " + newCommand.CommandId.ToString("X2"));
#endif
                            NewResponseEvent.Set();
                            break;
                    }
                }
            }
        }

        private void ProcessTouchComponent(CommandBuffer.Command newCommand)
        {
            var data = newCommand.GetBuffer();
            if (data == null || data.Length != 3)
                return;

            if (TouchEvent != null)
            {
                TouchEvent(this, new TouchEventArgs(Controls)
                {
                    PageId = data[0],
                    ControlIndex = data[1],
                    Press = data[2] == 0x1,
                });
            }
        }

        private void ProcessTouchXY(CommandBuffer.Command newCommand)
        {
            var data = newCommand.GetBuffer();
            if (data == null || data.Length != 5)
                return;

            if (TouchXYEvent != null)
            {
                TouchXYEvent(this, new TouchEventXYArgs()
                {
                    X = data[0] << 8 | data[1],
                    Y = data[2] << 8 | data[3],
                    Press = data[4] == 0x1,
                    SleepMode = newCommand.CommandId == 0x68,
                });
            }
        }

        private void ProcessSystemEvent(CommandBuffer.Command newCommand)
        {
            if (SystemEvent != null)
            {
                SystemEvent(this, (SystemEventType)newCommand.CommandId, new SystemEventParameter());
            }
        }

        #endregion

        #region Static
        private static void SendCommand(SerialPort port, byte[] buffer, string command)
        {
            int position = 0;
            bool notFinished = true;
            while (notFinished)
            {
                int count;
                //Reserve last three bytes for command end string
                if (command.Length - position > buffer.Length - Consts.EndOfLineCharCount)
                {
                    count = Encoding.UTF8.GetBytes(command, position, buffer.Length - Consts.EndOfLineCharCount, buffer, 0);
                    position += count;
                }
                else
                {
                    count = Encoding.UTF8.GetBytes(command, position, (command.Length - position), buffer, 0);
                    FillEndOfLine(buffer, ref count);

                    notFinished = false;
                }

                port.Write(buffer, 0, count);
            }

        }

        /// <summary>
        /// Change temporary baud rate variable and return new SerialPort with new baud rate
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="defaultBaudRate"></param>
        /// <param name="newBaudRate"></param>
        /// <returns></returns>
        public static SerialPort TemporaryChangeBaudRate(string portName, int defaultBaudRate, int newBaudRate)
        {
            var serial = new SerialPort(portName, defaultBaudRate);
            serial.Open();

            byte[] buffer = new byte[10];
            //Reset command
            SendCommand(serial, buffer, "");
            SendCommand(serial, buffer, "baud=" + newBaudRate);

            serial.Flush();
            serial.Close();
            serial.Dispose();

            return new SerialPort(portName, newBaudRate);
        }

        private static void FillEndOfLine(byte[] buffer, ref int index)
        {
            buffer[index] = Consts.EndOfLineChar;
            buffer[index + 1] = Consts.EndOfLineChar;
            buffer[index + 2] = Consts.EndOfLineChar;
            index += 3;
        }

        #endregion

        #region INextionDisplayPrivate implementation

        CommandBuffer.Command INextionDisplayPrivate.SendCommand(string command, bool waitForResponse)
        {
            return this.SendCommand(command, waitForResponse);
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            try
            {
                if (SerialDataReceiveThread != null)
                {
                    SerialDataReceiveThread.Abort();
                    SerialDataReceiveThread.Join();
                    SerialDataReceiveThread = null;
                }
            }
            catch { }

            try
            {
                Serial.Close();
                Serial.DiscardInBuffer();
                Serial.DiscardOutBuffer();
                Serial.Dispose();
            }
            catch { }
        }

        #endregion
    }
}
