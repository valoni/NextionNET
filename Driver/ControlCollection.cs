using System;
using Microsoft.SPOT;
using System.Collections;

namespace JernejK.NextionNET.Driver
{
    /// <summary>
    /// Control collection holder
    /// </summary>
    public class ControlCollection
    {
        /// <summary>
        /// Access to Display object
        /// </summary>
        protected NextionDisplay Display;
        internal ControlCollection(NextionDisplay display)
        {
            Display = display;
        }

        private ArrayList ControlsColl = new ArrayList();

        /// <summary>
        /// Define new progress bar
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.ProgressBar DefineProgressBar(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.ProgressBar(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new textbox
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.TextBox DefineTextBox(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.TextBox(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new numberbox
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.NumberBox DefineNumberBox(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.NumberBox(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new button
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.Button DefineButton(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.Button(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new picture box
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.PictureBox DefinePictureBox(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.PictureBox(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new picture box
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.CropBox DefineCropBox(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.CropBox(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new slider
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.Slider DefineSlider(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.Slider(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new dual state button
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.DualStateButton DefineDualState(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.DualStateButton(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new timer
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.Hidden.Timer DefineTimer(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.Hidden.Timer(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new variable
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.Hidden.Variable DefineVariable(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.Hidden.Variable(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        public Controls.Gauge DefineGauge(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.Gauge(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Define new Wafeform control
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Controls.Waveform DefineWaveform(byte pageId, string id, byte index)
        {
            var ctrl = new Controls.Waveform(Display, pageId, id, index);
            ControlsColl.Add(ctrl);
            return ctrl;
        }

        /// <summary>
        /// Get control by index
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Controls.Bases.ControlBase GetControlOrDefault(byte pageId, byte index)
        {
            foreach (Controls.Bases.ControlBase ctrl in ControlsColl)
            {
                if (ctrl.PageId == pageId && ctrl.Index == index)
                    return ctrl;
            }

            return null;
        }
    }
}
