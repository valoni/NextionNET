using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Demo.TicTacToe
{
    public partial class DisplayConfiguration
    {
        public enum MyPictures
        {
            Board = Pictures.Picture0_F39E4247,
            O = Pictures.Picture1_C18360FE,
            X = Pictures.Picture2_27A98F84,
            O_Small = Pictures.Picture3_75A22EBE,
            X_Small = Pictures.Picture4_1BA22AAD,
            Empty = Pictures.Picture5_DB414468
        }

        public partial class Page1
        {
            public static Driver.Controls.PictureBox[] PlayGround;
            static partial void UserInit(NextionNET.Driver.NextionDisplay display)
            {
                PlayGround = new[] { PlayGround00, PlayGround01, PlayGround02, PlayGround10, PlayGround11, PlayGround12, PlayGround20, PlayGround21, PlayGround22 };
            }
        }
    }
}
