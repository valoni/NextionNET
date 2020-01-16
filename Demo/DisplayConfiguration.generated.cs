namespace JernejK.NextionNET.Demo
{
public partial class DisplayConfiguration
{
public enum Fonts
{
Sanserif_16,
Sanserif_16_B,
Sanserif_40,
}
public enum Pictures
{
}
public partial class Page0
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 0;
public const string Name = "page0";
public static JernejK.NextionNET.Driver.Controls.TextBox t0;
public static JernejK.NextionNET.Driver.Controls.TextBox t1;
public static JernejK.NextionNET.Driver.Controls.ProgressBar ProgressBar;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
t0 = display.Controls.DefineTextBox(0, "t0", 1);
t1 = display.Controls.DefineTextBox(0, "t1", 2);
ProgressBar = display.Controls.DefineProgressBar(0, "ProgressBar", 3);
UserInit(display);
}
}
public partial class Page1
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 1;
public const string Name = "ready";
public static JernejK.NextionNET.Driver.Controls.TextBox t0;
public static JernejK.NextionNET.Driver.Controls.TextBox t1;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
t0 = display.Controls.DefineTextBox(1, "t0", 1);
t1 = display.Controls.DefineTextBox(1, "t1", 2);
UserInit(display);
}
}
public partial class Page2
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 2;
public const string Name = "page1";
public static JernejK.NextionNET.Driver.Controls.TextBox MainTextbox;
public static JernejK.NextionNET.Driver.Controls.Button btnTime;
public static JernejK.NextionNET.Driver.Controls.Button btnDate;
public static JernejK.NextionNET.Driver.Controls.Button btnSleep;
public static JernejK.NextionNET.Driver.Controls.Button btnDraw5;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
MainTextbox = display.Controls.DefineTextBox(2, "MainTextbox", 1);
btnTime = display.Controls.DefineButton(2, "btnTime", 2);
btnDate = display.Controls.DefineButton(2, "btnDate", 3);
btnSleep = display.Controls.DefineButton(2, "btnSleep", 4);
btnDraw5 = display.Controls.DefineButton(2, "btnDraw5", 5);
UserInit(display);
}
}
public partial class Page3
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 3;
public const string Name = "page2";
public static JernejK.NextionNET.Driver.Controls.TextBox t0;
public static JernejK.NextionNET.Driver.Controls.TextBox txtX;
public static JernejK.NextionNET.Driver.Controls.TextBox txtY;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
t0 = display.Controls.DefineTextBox(3, "t0", 1);
txtX = display.Controls.DefineTextBox(3, "txtX", 2);
txtY = display.Controls.DefineTextBox(3, "txtY", 3);
UserInit(display);
}
}
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
Page0.Init(display);
Page1.Init(display);
Page2.Init(display);
Page3.Init(display);
}
}
}
