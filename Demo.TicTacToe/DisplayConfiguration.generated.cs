namespace JernejK.NextionNET.Demo.TicTacToe
{
public partial class DisplayConfiguration
{
public enum Fonts
{
Sanserif_16,
Sanserif_40,
Sanserif_40_B,
SanSerif_24,
}
public enum Pictures
{
Picture0_F39E4247,
Picture1_C18360FE,
Picture2_27A98F84,
Picture3_75A22EBE,
Picture4_1BA22AAD,
Picture5_DB414468,
}
public partial class Page0
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 0;
public const string Name = "Score";
public static JernejK.NextionNET.Driver.Controls.TextBox t0;
public static JernejK.NextionNET.Driver.Controls.TextBox t1;
public static JernejK.NextionNET.Driver.Controls.PictureBox p1;
public static JernejK.NextionNET.Driver.Controls.TextBox Score_X;
public static JernejK.NextionNET.Driver.Controls.TextBox t3;
public static JernejK.NextionNET.Driver.Controls.PictureBox p0;
public static JernejK.NextionNET.Driver.Controls.TextBox Score_O;
public static JernejK.NextionNET.Driver.Controls.TextBox t5;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
t0 = display.Controls.DefineTextBox(0, "t0", 1);
t1 = display.Controls.DefineTextBox(0, "t1", 2);
p1 = display.Controls.DefinePictureBox(0, "p1", 3);
Score_X = display.Controls.DefineTextBox(0, "Score_X", 4);
t3 = display.Controls.DefineTextBox(0, "t3", 5);
p0 = display.Controls.DefinePictureBox(0, "p0", 6);
Score_O = display.Controls.DefineTextBox(0, "Score_O", 7);
t5 = display.Controls.DefineTextBox(0, "t5", 8);
UserInit(display);
}
}
public partial class Page1
{
static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);
public const byte Id = 1;
public const string Name = "Game";
public static JernejK.NextionNET.Driver.Controls.PictureBox p0;
public static JernejK.NextionNET.Driver.Controls.TextBox t0;
public static JernejK.NextionNET.Driver.Controls.PictureBox NextMove;
public static JernejK.NextionNET.Driver.Controls.PictureBox PlayGround00;
public static JernejK.NextionNET.Driver.Controls.PictureBox PlayGround01;
public static JernejK.NextionNET.Driver.Controls.PictureBox PlayGround02;
public static JernejK.NextionNET.Driver.Controls.PictureBox PlayGround10;
public static JernejK.NextionNET.Driver.Controls.PictureBox PlayGround11;
public static JernejK.NextionNET.Driver.Controls.PictureBox PlayGround12;
public static JernejK.NextionNET.Driver.Controls.PictureBox PlayGround20;
public static JernejK.NextionNET.Driver.Controls.PictureBox PlayGround21;
public static JernejK.NextionNET.Driver.Controls.PictureBox PlayGround22;
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
p0 = display.Controls.DefinePictureBox(1, "p0", 1);
t0 = display.Controls.DefineTextBox(1, "t0", 2);
NextMove = display.Controls.DefinePictureBox(1, "NextMove", 3);
PlayGround00 = display.Controls.DefinePictureBox(1, "PlayGround00", 4);
PlayGround01 = display.Controls.DefinePictureBox(1, "PlayGround01", 5);
PlayGround02 = display.Controls.DefinePictureBox(1, "PlayGround02", 6);
PlayGround10 = display.Controls.DefinePictureBox(1, "PlayGround10", 7);
PlayGround11 = display.Controls.DefinePictureBox(1, "PlayGround11", 8);
PlayGround12 = display.Controls.DefinePictureBox(1, "PlayGround12", 9);
PlayGround20 = display.Controls.DefinePictureBox(1, "PlayGround20", 10);
PlayGround21 = display.Controls.DefinePictureBox(1, "PlayGround21", 11);
PlayGround22 = display.Controls.DefinePictureBox(1, "PlayGround22", 12);
UserInit(display);
}
}
public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)
{
Page0.Init(display);
Page1.Init(display);
}
}
}
