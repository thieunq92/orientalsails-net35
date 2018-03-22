using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CMS.Web.UI
{
    /// <summary>
    /// This is the base class for every template usercontrol.
    /// </summary>
    public abstract class BaseTemplate : UserControl
    {
        #region -- CONTROLS --
        /// <summary>
        /// Template controls that inherit from BaseTemplate must have a Literal control with 
        /// id="MetaTags".
        /// </summary>
        protected Literal MetaTags;

        /// <summary>
        /// Template controls that inherit from BaseTemplate must have a Literal control with id="PageTitle".
        /// </summary>
        protected Literal PageTitle;

        /// <summary>
        /// Template controls that inherit from BaseTemplate must have a Literal control with 
        /// id="Stylesheets".
        /// </summary>
        protected Literal Stylesheets;
        #endregion

        #region -- Properties --
        /// <summary>
        /// The page title as shown in the title bar of the browser.
        /// </summary>
        public string Title
        {
            get { return PageTitle.Text; }
            set { PageTitle.Text = value; }
        }

        /// <summary>
        /// The form of the template.
        /// </summary>
        public Control Form
        {
            get
            {
                foreach (Control ctrl in Controls)
                {
                    if (ctrl is HtmlForm)
                        return ctrl as HtmlForm;
                }
                return null;
            }
        }

        /// <summary>
        /// All content containers.
        /// </summary>
        public Hashtable Containers
        {
            get
            {
                Hashtable tbl = new Hashtable();
                foreach (Control ctrl in Form.Controls)
                {
                    if (ctrl is PlaceHolder)
                    {
                        tbl.Add(ctrl.ID, ctrl);
                    }
                        // Also check for user controls with content placeholders.
                    else if (ctrl is UserControl)
                    {
                        foreach (Control ctrl2 in ctrl.Controls)
                        {
                            if (ctrl2 is PlaceHolder)
                            {
                                tbl.Add(ctrl2.ID, ctrl2);
                            }
                        }
                    }
                }
                return tbl;
            }
        }

        #endregion

        #region -- METHODS --

        /// <summary>
        /// Converts the list of css links to stylesheet tags and inserts these in the appropriate place.
        /// </summary>
        /// <param name="stylesheets"></param>
        public void RenderCssLinks(string[] stylesheets)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string stylesheet in stylesheets)
            {
                sb.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />\n\t", stylesheet);
            }
            Stylesheets.Text = sb.ToString();
        }

        /// <summary>
        /// Converts the dictionary of meta tags to real meta tags and inserts these in the appropriate place.
        /// </summary>
        /// <param name="metaTags"></param>
        public void RenderMetaTags(IDictionary metaTags)
        {
            StringBuilder sb = new StringBuilder();
            if (metaTags != null)
            {
                foreach (DictionaryEntry entry in metaTags)
                {
                    sb.AppendFormat("<meta name=\"{0}\" content=\"{1}\" />\n\t", entry.Key, entry.Value);
                }
                MetaTags.Text = sb.ToString();
            }
        }

        /// <summary>
        /// Insert hyperlinks in the placeholders to enable placeholder selection (for administration only).
        /// </summary>
        public void InsertContainerButtons()
        {
            string placeholderChooseControl = Context.Request.QueryString["Control"];
            if (placeholderChooseControl != null)
            {
                foreach (PlaceHolder plc in Containers.Values)
                {
                    HtmlInputButton btn = new HtmlInputButton();
                    btn.Value = plc.ID;
                    btn.Attributes.Add("onclick",
                                       String.Format("window.opener.setPlaceholderValue('{0}','{1}');self.close()",
                                                     placeholderChooseControl, plc.ID));
                    plc.Controls.Add(btn);
                }
            }
        }

        #endregion
    }
}