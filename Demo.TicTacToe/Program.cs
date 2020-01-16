using System;
using Microsoft.SPOT;
using System.Threading;

namespace JernejK.NextionNET.Demo.TicTacToe
{
    public class Program
    {
        private static Driver.NextionDisplay Display;

        private static byte ScoreX, ScoreO;
        private static GameState State;

        private static AutoResetEvent StartGame = new AutoResetEvent(false);
        private static DateTime NextTouchAllowed = DateTime.MinValue;

        private static Position[] PlayGroundPositions;
        private static bool SkipGameTouch = false;

        private static byte MoveNumber;

        public static void Main()
        {
            Display = new Driver.NextionDisplay(Driver.NextionDisplay.TemporaryChangeBaudRate("COM2", 9600, 115200), 240, 320);
            DisplayConfiguration.Init(Display);
            Display.TouchEvent += Display_TouchEvent;
            Display.TouchXYEvent += Display_TouchXYEvent;
            Display.SetSleepMode(false);
            Display.Backlight = 100;

            Display.GUI.DefaultBackgroundColor = (int)Driver.Color.Black;
            Display.GUI.DefaultFontColor = (int)Driver.Color.Red;
            Display.GUI.DefaultFontId = (byte)DisplayConfiguration.Fonts.Sanserif_40_B;
            Display.GUI.DefaultTextHeight = 45;

            RenderScorePage();

            while (true)
            {
                if (State == GameState.Score)
                {
                    StartGame.WaitOne();
                    RenderGamePage();
                }
            }
        }

        static void Display_TouchXYEvent(object sender, Driver.TouchEventXYArgs args)
        {
            //Debouncing
            if (NextTouchAllowed > DateTime.Now)
                return;
            NextTouchAllowed = DateTime.Now.AddSeconds(1);

            Debug.Print("New XY touch");

            if (State == GameState.Score)
            {
                Debug.Print("Start new game");
                StartGame.Set();
            }
        }

        static void Display_TouchEvent(object sender, Driver.TouchEventArgs args)
        {
            //Process command on another thread
            var thread = new Thread(new ThreadStart(() =>
                {
                    if (State == GameState.Game)
                    {
                        //When player already won
                        if (SkipGameTouch)
                            return;

                        //To be on safe side
                        var ctrl = args.ResolveControl();
                        if (ctrl == null)
                            return;

                        //To be on safe side
                        int playGroundIndex = Array.IndexOf(DisplayConfiguration.Page1.PlayGround, ctrl);
                        if (playGroundIndex < 0)
                            return;

                        //Is position occupied?
                        if (PlayGroundPositions[playGroundIndex] != Position.Empty)
                            return;

                        var position = NextMoveO ? Position.O : Position.X;

                        PlayGroundPositions[playGroundIndex] = position;

                        //Render change
                        DisplayConfiguration.Page1.PlayGround[playGroundIndex].PictureId = NextMoveO ? (byte)DisplayConfiguration.MyPictures.O : (byte)DisplayConfiguration.MyPictures.X;

                        MoveNumber++;
                        if (CheckForWinningPosition(position))
                        {
                            SkipGameTouch = true;
                            if (NextMoveO)
                                ScoreO++;
                            else
                                ScoreX++;

                            Display.GUI.WriteText("You won!!!", 0, 0, horizontalAligment: Driver.HorizontalAlignment.Center);

                            Thread.Sleep(3000);
                            RenderScorePage();
                            return;
                        }
                        else
                        {
                            if (MoveNumber == 9)
                            {
                                SkipGameTouch = true;
                                Display.GUI.WriteText("It's a tie!", 0, 0, horizontalAligment: Driver.HorizontalAlignment.Center);
                                Thread.Sleep(3000);
                                RenderScorePage();
                                return;
                            }

                            NextMoveO = !NextMoveO;
                            RenderGamePageNextMove();
                        }
                    }
                }));
            thread.Start();
        }

        private static bool CheckForWinningPosition(Position position)
        {
            //Check rows
            for (int i = 0; i < 9; i = i + 3)
            {
                if (PlayGroundPositions[i] == position && PlayGroundPositions[i + 1] == position && PlayGroundPositions[i + 2] == position)
                    return true;
            }

            //Check columns
            for (int i = 0; i < 3; i++)
            {
                if (PlayGroundPositions[i] == position && PlayGroundPositions[i + 3] == position && PlayGroundPositions[i + 6] == position)
                    return true;
            }

            //Check diagonals
            if (PlayGroundPositions[0] == position && PlayGroundPositions[4] == position && PlayGroundPositions[8] == position)
                return true;
            if (PlayGroundPositions[2] == position && PlayGroundPositions[4] == position && PlayGroundPositions[6] == position)
                return true;

            return false;
        }

        public static void RenderScorePage()
        {
            Display.PageId = DisplayConfiguration.Page0.Id;
            Display.TouchMode = Driver.TouchMode.Coordinates;

            DisplayConfiguration.Page0.Score_X.Text = ScoreX.ToString();
            DisplayConfiguration.Page0.Score_O.Text = ScoreO.ToString();
            State = GameState.Score;
            NextTouchAllowed = DateTime.Now.AddSeconds(3);
        }

        private static bool NextTimeOFirst = false;

        private static bool NextMoveO = false;
        private static void RenderGamePage()
        {
            Display.PageId = DisplayConfiguration.Page1.Id;
            Display.TouchMode = Driver.TouchMode.Component;

            NextMoveO = NextTimeOFirst;
            NextTimeOFirst = !NextTimeOFirst;

            RenderGamePageNextMove();

            State = GameState.Game;
            PlayGroundPositions = new Position[9];
            SkipGameTouch = false;
            MoveNumber = 0;
        }

        private static void RenderGamePageNextMove()
        {
            DisplayConfiguration.Page1.NextMove.PictureId = NextMoveO ? (int)DisplayConfiguration.MyPictures.O_Small : (int)DisplayConfiguration.MyPictures.X_Small;
        }

    }
}
