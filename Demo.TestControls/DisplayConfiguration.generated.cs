namespace Demo.TestControls
{
public partial class DisplayConfiguration
{
public enum Fonts
{
Sanserif_16,
Sanserif_16_B,
}
public enum Pictures
{
Picture0_4C5C9ED1,
Picture1_66B4FE23,
Picture2_D735FB57,
Picture3_162CB705,
}
public partial class Page0
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 0;
public const string Name = "page0";
public static JernejK.NextionNET.Driver.Controls.TextBox t0;
public static JernejK.NextionNET.Driver.Controls.Button b0;
public static JernejK.NextionNET.Driver.Controls.ProgressBar j0;
public static JernejK.NextionNET.Driver.Controls.PictureBox p0;
public static JernejK.NextionNET.Driver.Controls.Slider h0;
public static JernejK.NextionNET.Driver.Controls.NumberBox n0;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
t0 = display.Controls.DefineTextBox(0, "t0", 1);
b0 = display.Controls.DefineButton(0, "b0", 2);
j0 = display.Controls.DefineProgressBar(0, "j0", 3);
p0 = display.Controls.DefinePictureBox(0, "p0", 4);
h0 = display.Controls.DefineSlider(0, "h0", 5);
n0 = display.Controls.DefineNumberBox(0, "n0", 6);
UserInit(display);
}
}
public partial class Page1
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 1;
public const string Name = "page1";
public static JernejK.NextionNET.Driver.Controls.Waveform s0;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
s0 = display.Controls.DefineWaveform(1, "s0", 1);
UserInit(display);
}
}
public partial class Page2
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 2;
public const string Name = "page2";
public static JernejK.NextionNET.Driver.Controls.DualStateButton btnDualState;
public static JernejK.NextionNET.Driver.Controls.TextBox t0;
public static JernejK.NextionNET.Driver.Controls.Button btnTest;
public static JernejK.NextionNET.Driver.Controls.TextBox tbCounter;
public static JernejK.NextionNET.Driver.Controls.Button btnContinue;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
btnDualState = display.Controls.DefineDualState(2, "btnDualState", 1);
t0 = display.Controls.DefineTextBox(2, "t0", 2);
btnTest = display.Controls.DefineButton(2, "btnTest", 3);
tbCounter = display.Controls.DefineTextBox(2, "tbCounter", 4);
btnContinue = display.Controls.DefineButton(2, "btnContinue", 5);
UserInit(display);
}
}
public partial class Page3
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 3;
public const string Name = "page3";
public static JernejK.NextionNET.Driver.Controls.Gauge gauge;
public static JernejK.NextionNET.Driver.Controls.Hidden.Variable va0;
public static JernejK.NextionNET.Driver.Controls.Hidden.Timer tm0;
public static JernejK.NextionNET.Driver.Controls.Hidden.Variable tmp;
public static JernejK.NextionNET.Driver.Controls.Button btnSlow;
public static JernejK.NextionNET.Driver.Controls.Button btnFast;
public static JernejK.NextionNET.Driver.Controls.Button btnColor;
public static JernejK.NextionNET.Driver.Controls.Button btnThick;
public static JernejK.NextionNET.Driver.Controls.Button btnStart;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
gauge = display.Controls.DefineGauge(3, "gauge", 1);
va0 = display.Controls.DefineVariable(3, "va0", 2);
tm0 = display.Controls.DefineTimer(3, "tm0", 3);
tmp = display.Controls.DefineVariable(3, "tmp", 4);
btnSlow = display.Controls.DefineButton(3, "btnSlow", 5);
btnFast = display.Controls.DefineButton(3, "btnFast", 6);
btnColor = display.Controls.DefineButton(3, "btnColor", 7);
btnThick = display.Controls.DefineButton(3, "btnThick", 8);
btnStart = display.Controls.DefineButton(3, "btnStart", 9);
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
