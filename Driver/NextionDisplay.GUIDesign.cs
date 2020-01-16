using System;
using Microsoft.SPOT;

namespace JernejK.NextionNET.Driver
{
    public partial class NextionDisplay
    {
        /// <summary>
        /// Direct access to GUI
        /// </summary>
        public class GUIDesign
        {
            private NextionDisplay Display;

            #region Ctor

            internal GUIDesign(NextionDisplay display)
            {
                Display = display;

                DefaultFontId = 0;
                DefaultFontColor = (int)Color.White;
                DefaultBackgroundColor = (int)Color.Black;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Default font
            /// </summary>
            public byte DefaultFontId { get; set; }
            /// <summary>
            /// Default font color
            /// </summary>
            public int DefaultFontColor { get; set; }
            /// <summary>
            /// Default background color
            /// </summary>
            public int DefaultBackgroundColor { get; set; }
            /// <summary>
            /// Default height
            /// </summary>
            public int DefaultTextHeight { get; set; }

            #endregion

            /// <summary>
            /// Clear screen and set background color
            /// </summary>
            /// <param name="color"></param>
            public void Clear(Color color)
            {
                Clear((int)color);
            }

            /// <summary>
            /// Clear screen and set background color
            /// </summary>
            /// <param name="color"></param>
            public void Clear(int color)
            {
                Display.SendCommand("cls " + color, false);
            }

            /// <summary>
            /// Draw circle
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="radius"></param>
            /// <param name="color"></param>
            public void DrawCircle(int x, int y, int radius, int color)
            {
                Display.SendCommand("cir " + x + "," + y + "," + radius + "," + color, false);
            }

            /// <summary>
            /// Draw line between two points
            /// </summary>
            /// <param name="x1"></param>
            /// <param name="y1"></param>
            /// <param name="x2"></param>
            /// <param name="y2"></param>
            /// <param name="color"></param>
            public void DrawLine(int x1, int y1, int x2, int y2, int color)
            {
                Display.SendCommand("line " + x1 + "," + y1 + "," + x2 + "," + y2 + "," + color, false);
            }

            /// <summary>
            /// Draw rectangle between two points
            /// </summary>
            /// <param name="x1"></param>
            /// <param name="y1"></param>
            /// <param name="x2"></param>
            /// <param name="y2"></param>
            /// <param name="color"></param>
            public void DrawRectangle(int x1, int y1, int x2, int y2, int color)
            {
                Display.SendCommand("draw " + x1 + "," + y1 + "," + x2 + "," + y2 + "," + color, false);
            }

            /// <summary>
            /// Draw rectangle with provided width and height
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <param name="color"></param>
            public void DrawRectangleWidthHeight(int x, int y, int width, int height, int color)
            {
                DrawRectangle(x, y, x + width, y + height, color);
            }

            /// <summary>
            /// Draw filled rectangle between two points
            /// </summary>
            /// <param name="x1"></param>
            /// <param name="y1"></param>
            /// <param name="x2"></param>
            /// <param name="y2"></param>
            /// <param name="color"></param>
            public void DrawRectangleFilled(int x1, int y1, int x2, int y2, int color)
            {
                int x,y,width, height;
                NextUtils.FromX1X2ToXAndLenght(x1, x2, out x, out width);
                NextUtils.FromX1X2ToXAndLenght(y1, y2, out y, out height);

                DrawRectangleFilledWidthHeight(x, y, width, height, color);
            }

            /// <summary>
            /// Draw filled rectangle with provided width and height
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <param name="color"></param>
            public void DrawRectangleFilledWidthHeight(int x, int y, int width, int height, int color)
            {
                Display.SendCommand("fill " + x + "," + y + "," + width + "," + height + "," + color, false);
            }

            /// <summary>
            /// Draw text
            /// </summary>
            /// <param name="text">NextUtils.EscapeString for escaping</param>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <param name="fontId"></param>
            /// <param name="fontColor"></param>
            /// <param name="backColor"></param>
            /// <param name="horizontalAligment"></param>
            /// <param name="verticalAligment"></param>
            public void WriteText(string text, int x, int y, int width, int height, byte fontId, int fontColor, int backColor, HorizontalAlignment horizontalAligment, VerticalAlignment verticalAligment)
            {
                Display.SendCommand("xstr " + x
                    + "," + y
                    + "," + width
                    + "," + height
                    + "," + fontId
                    + "," + fontColor
                    + "," + (backColor != -1 ? backColor.ToString() : "NULL")
                    + "," + (byte)horizontalAligment
                    + "," + (byte)verticalAligment
                    + "," + (byte)BackgroundFill.SolidColor
                    + ",\"" + text + "\"", false);
            }

            /// <summary>
            /// Write text with default font and color
            /// </summary>
            /// <param name="text"></param>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="height"></param>
            /// <param name="width"></param>
            /// <param name="horizontalAligment"></param>
            public void WriteText(string text, int x, int y, int height = int.MinValue, int width = int.MinValue, HorizontalAlignment horizontalAligment = HorizontalAlignment.Left)
            {
                if (width <= 0)
                {
                    if (Display.Width <= 0)
                        throw new Exception("Display width and height is not set. Please set it in drivers constructos");

                    width = Display.Width - x;
                }

                //If height is not specified, try with default height
                if (height <= 0)
                    height = DefaultTextHeight;

                //If default height is not specified, take whole screen
                if (height <= 0)
                {
                    if (Display.Height <= 0)
                        throw new Exception("Display width and height is not set. Please set it in drivers constructos");
                    height = Display.Height - y;
                }

                WriteText(text, x, y, width, height, DefaultFontId, DefaultFontColor, DefaultBackgroundColor, horizontalAligment, VerticalAlignment.Top);
            }
        }
    }
}
