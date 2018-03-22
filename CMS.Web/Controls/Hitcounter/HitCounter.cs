using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

namespace CMS.Web.Controls.Hitcounter
{
    /// <summary>
    /// A very simple to use hit counter 
    /// </summary>
    [DefaultProperty("Value"), ToolboxData("<{0}:HitCounter runat=server></{0}:HitCounter>")]
    public class HitCounter : WebControl
    {
        #region -- PRIVATE MEMBERS --
        private int mCount;
        private TimeSpan mDelay = TimeSpan.Zero;
        private string mFile = "count.txt";
        private int mPadding = 5;
        private static readonly ILog logger = LogManager.GetLogger(typeof (HitCounter));

        #endregion

        #region -- CONSTRUCTORS --

        public HitCounter()
        {
            BackColor = Color.Black;
            BorderColor = Color.White;
            ForeColor = Color.White;
        }

        #endregion

        #region -- BROWSABLE PROPERTIES --

        #region -- Bindable Properties --
        [Bindable(true), Category("Appearance"), DefaultValue("9999")]
        public int Value
        {
            get { return mCount; }
            set { mCount = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("5")]
        public int Padding
        {
            get { return mPadding; }
            set { mPadding = value; }
        }

        [Bindable(true), Category("Behavior"), DefaultValue("0")]
        public TimeSpan WriteDelay
        {
            get { return mDelay; }
            set { mDelay = value; }
        }

        [Bindable(true), Category("Behavior"), DefaultValue("count.txt")]
        public string TextFileName
        {
            get { return mFile; }
            set { mFile = value; }
        }

        #endregion

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Table; }
        }

        #region hidden properties

        [Browsable(false)]
        public override BorderStyle BorderStyle
        {
            get { return 0; }
        }

        [Browsable(false)]
        public override Unit Width
        {
            get { return 0; }
        }

        [Browsable(false)]
        public override Unit Height
        {
            get { return 0; }
        }

        [Browsable(false)]
        public override bool Enabled
        {
            get { return true; }
        }

        #endregion

        #endregion

        #region saving counter (increment, get and set)

        private string FullPath
        {
            get { return Context.Server.MapPath("") + "\\" + TextFileName; }
        }

        /// <summary>
        /// Increase Hit Counter instantly
        /// </summary>
        /// <returns>New Hits</returns>
        private int incrementWithoutDelay()
        {
            int i = getCounterFromFile();
            i++;
            setCounterInFile(i);
            return i;
        }

        /// <summary>
        /// Increase Hit Counter after some times
        /// </summary>
        /// <returns>New Hits</returns>
        private int incrementWithDelay()
        {
            int i = 0;
            if (Page.Application["DSHHitCounter"] == null)
                i = getCounterFromFile();
            else
                i = (int) Page.Application["DSHHitCounter"];

            i++;
            Page.Application.Lock();
            Page.Application["DSHHitCounter"] = i;
            Page.Application.UnLock();

            //if it's time, then save the value to disk
            DateTime nextwrite = DateTime.Now;
            if (Page.Application["DSHHitCounterTime"] != null)
                nextwrite = (DateTime) Page.Application["DSHHitCounterTime"];
            if (nextwrite <= DateTime.Now)
            {
                //set the time for the next update
                DateTime update = DateTime.Now.Add(WriteDelay);
                Page.Application.Lock();
                Page.Application["DSHHitCounterTime"] = update;
                Page.Application.UnLock();
                //save the conter to the file
                setCounterInFile(i);
            }

            //finally, send back the latest count
            return i;
        }

        private int increment()
        {
            if (WriteDelay == TimeSpan.Zero)
                return incrementWithoutDelay();
            else
                return incrementWithDelay();
        }

        private int getCounterFromFile()
        {
            if (!File.Exists(FullPath)) return 0;
            int retval = 1;
            try
            {
                FileStream fs = new FileStream(FullPath, FileMode.Open);
                int len = (int) fs.Length;
                byte[] buffer = new byte[len];
                fs.Read(buffer, 0, len);
                fs.Close();
                StringBuilder sb = new StringBuilder();
                for (int t = 0; t < len; t++) sb.Append((char) buffer[t]);
                retval = Int32.Parse(sb.ToString());
            }
            catch (Exception ex)
            {
                logger.Error("Hit counter Error",ex);
                //throw ex;
            }

            return retval;
        }

        private void setCounterInFile(int t)
        {
            try
            {
                FileStream fs = new FileStream(FullPath, FileMode.Create);
                string v = t.ToString();
                byte[] buffer = new byte[v.Length];
                for (int x = 0; x < v.Length; x++) buffer[x] = (byte) v[x];
                fs.Write(buffer, 0, v.Length);
                fs.Close();
            }
            catch (Exception)
            {
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (Context != null && !Page.IsPostBack) Value = increment();
            base.OnLoad(e);
        }

        /// <summary>
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            Attributes.Add("cellSpacing", "1");
            Attributes.Add("cellPadding", "1");
            Attributes.Add("border", "1");
            Attributes.Add("borderWidth", BorderWidth.ToString());
            Attributes.Add("borderColor", formatColour(BorderColor));

            base.RenderBeginTag(output);

            output.Write("<TR>");
            foreach (char c in mCount.ToString().PadLeft(mPadding, '0'))
                output.Write("<TD align=\"middle\">" + c + "</TD>");
            output.Write("</TR>");

            base.RenderEndTag(output);
        }

        private string formatColour(Color c)
        {
            string retval = "";
            if (c.IsEmpty)
                retval = "white";
            else if (c.IsKnownColor || c.IsNamedColor || c.IsSystemColor)
                retval = c.Name;
            else
                retval = "#" + c.Name.Remove(0, 2);

            return retval;
        }
    }
}