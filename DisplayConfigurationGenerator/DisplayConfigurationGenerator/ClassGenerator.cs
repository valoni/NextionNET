using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayConfigurationGenerator
{
    public class ClassGenerator
    {
        private static string MakeNameSafe(string name)
        {
            name = name.Replace(" ", "_").Replace("!", "_").Replace("\"", "_").Replace(",", "_").Replace("-", "_");
            return name;
        }

        public string GenerateClass(Models.Configuration configuration, Models.HMI.Display display)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormatLine("namespace {0}", configuration.Namespace);
            sb.AppendFormatLine("{{");
            sb.AppendFormatLine("public partial class {0}", configuration.ClassName);
            sb.AppendFormatLine("{{");

            #region Font
            //Font
            sb.AppendFormatLine("public enum Fonts");
            sb.AppendFormatLine("{{");

            if (display.Fonts != null)
            {
                foreach(var font in display.Fonts)
                {
                    sb.AppendFormatLine("{0},", MakeNameSafe(font.Name));
                }
            }

            sb.AppendFormatLine("}}");
            #endregion

            #region Picture

            sb.AppendFormatLine("public enum Pictures");
            sb.AppendFormatLine("{{");

            if (display.Images != null)
            {
                for(int i = 0;i < display.Images.Count;i++)
                {
                    sb.AppendFormatLine("Picture{0}_{1},", i, display.Images[i].Hash);
                }
            }

            sb.AppendFormatLine("}}");

            #endregion

            #region Page

            StringBuilder sbInitAllPages = new StringBuilder();

            for(int pageId = 0;pageId < display.Pages.Count;pageId++)
            {
                var sbControlList = new StringBuilder();
                var sbInitFunction = new StringBuilder();

                var page = display.Pages[pageId];
                sb.AppendFormatLine("public partial class Page{0}", pageId);
                sb.AppendFormatLine("{{");
                sb.AppendFormatLine("static partial void UserInit(JernejK.NextionNET.Driver.NextionDisplay display);");

                sbInitAllPages.AppendFormatLine("Page{0}.Init(display);", pageId);

                sb.AppendFormatLine("public const byte Id = {0};", pageId);
                sb.AppendFormatLine("public const string Name = \"{0}\";", page.Id);

                for(int cmpId = 0;cmpId < page.Components.Count;cmpId++)
                {
                    var cmp = page.Components[cmpId];
                    var codeId = MakeNameSafe(cmp.Id);
                    switch(cmp.Type)
                    {
                        case "Text":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.TextBox {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineTextBox({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Button":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.Button {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineButton({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Number":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.NumberBox {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineNumberBox({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Progress bar":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.ProgressBar {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineProgressBar({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Picture":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.PictureBox {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefinePictureBox({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Crop Image":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.CropBox {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineCropBox({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Gauges":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.Gauge {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineGauge({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Waveform":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.Waveform {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineWaveform({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Slider":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.Slider {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineSlider({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Timer":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.Hidden.Timer {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineTimer({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Variable":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.Hidden.Variable {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineVariable({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        case "Dual-state button":
                            sbControlList.AppendFormatLine("public static JernejK.NextionNET.Driver.Controls.DualStateButton {0};", codeId);
                            sbInitFunction.AppendFormatLine("{0} = display.Controls.DefineDualState({1}, \"{2}\", {3});", codeId, pageId, cmp.Id, cmpId);
                            break;
                        default:
                            Console.WriteLine("Unknown type: {0}. Skipping...", cmp.Type);
                            break;

                    }
                }

                sb.Append(sbControlList.ToString());

                sb.AppendFormatLine("public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)");
                sb.AppendFormatLine("{{");
                sb.Append(sbInitFunction.ToString());
                sb.AppendFormatLine("UserInit(display);");
                sb.AppendFormatLine("}}");
            
                sb.AppendFormatLine("}}");
            }

            #endregion

            //Init function
            sb.AppendFormatLine("public static void Init(JernejK.NextionNET.Driver.NextionDisplay display)");
            sb.AppendFormatLine("{{");
            sb.Append(sbInitAllPages.ToString());
            sb.AppendFormatLine("}}");

            //Close class
            sb.AppendFormatLine("}}");
            //Close namespace
            sb.AppendFormatLine("}}");

            return sb.ToString();
        }
    }
}
