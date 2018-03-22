/* Kien sua ngay 10/04/2008
 * Muc dich: Ho tro UpdatePanel cua AjaxControlToolkit (truoc do khong su dung duoc) trong Admin
*/

using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Service;

namespace CMS.Web.UI
{
    /// <summary>
    /// The BasePage class is the class that webforms have to inherit to use the templates.
    /// </summary>
    public class GenericBasePage : PortalPage
    {
        // Member variables
        private string _css;
        private string _templateDir;
        private string _templateFilename;
        private string _title;

        #region properties

        /// <summary>
        /// Template property (filename of the User Control). This property can be used to change 
        /// page templates run-time.
        /// </summary>
        public string TemplateFilename
        {
            set { _templateFilename = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected string TemplateDir
        {
            set { _templateDir = value; }
        }

        /// <summary>
        /// The page title as shown in the title bar of the browser.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Path to external stylesheet file, relative from the application root.
        /// </summary>
        public string Css
        {
            get { return _css; }
            set { _css = value; }
        }

        /// <summary>
        /// Property for the template control. This property can be used for finding other controls.
        /// </summary>
        public UserControl TemplateControl
        {
            get
            {
                if (Controls.Count > 0)
                {
                    if (Controls[0] is UserControl)
                    {
                        return (UserControl) Controls[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// The messagebox.
        /// </summary>
        public HtmlGenericControl MessageBox
        {
            get
            {
                if (TemplateControl != null)
                {
                    return TemplateControl.FindControl("MessageBox") as HtmlGenericControl;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// The core repository for persisting Cuyahoga objects.
        /// </summary>
        public CoreRepository CoreRepository
        {
            get { return HttpContext.Current.Items["CoreRepository"] as CoreRepository; }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public GenericBasePage()
        {
            _templateFilename = null;
            _templateDir = null;
            _css = null;
        }

        /// <summary>
        /// Protected constructor that accepts template parameters. Could be handled more elegantly.
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <param name="templateDir"></param>
        /// <param name="css"></param>
        protected GenericBasePage(string templateFileName, string templateDir, string css)
        {
            _templateFilename = templateFileName;
            _templateDir = templateDir;
            _css = css;
        }

        /// <summary>
        /// Try to find the MessageBox control, insert the errortext and set visibility to true.
        /// </summary>
        /// <param name="errorText"></param>
        protected virtual void ShowError(string errorText)
        {
            if (MessageBox != null)
            {
                MessageBox.InnerHtml = "An error occured: " + errorText;
                MessageBox.Attributes["class"] = "errorbox";
                MessageBox.Visible = true;
            }
            else
            {
                // Throw an Exception and hope it will be handled by the global application exception handler.
                throw new Exception(errorText);
            }
        }

        /// <summary>
        /// Try to find the MessageBox control, insert the message and set visibility to true.
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ShowMessage(string message)
        {
            if (MessageBox != null)
            {
                MessageBox.InnerHtml = message;
                MessageBox.Attributes["class"] = "messagebox";
                MessageBox.Visible = true;
            }
            // TODO: change the class attribute to make a difference with the error (nice background image?)
        }

        /// <summary>
        /// Show the message of the exception, and the messages of the inner exceptions.
        /// </summary>
        /// <param name="exception"></param>
        protected virtual void ShowException(Exception exception)
        {
            string exceptionMessage = "<p>" + exception.Message + "</p>";
            Exception innerException = exception.InnerException;
            while (innerException != null)
            {
                exceptionMessage += "<p>" + innerException.Message + "</p>";
                innerException = innerException.InnerException;
            }
            ShowError(exceptionMessage);
        }

        protected override void OnInit(EventArgs e)
        {
            // Init
            PlaceHolder plc = null;
            ControlCollection col = Controls;

            // Set template directory
            if (_templateDir == null && ConfigurationSettings.AppSettings["TemplateDir"] != null)
            {
                _templateDir = ConfigurationSettings.AppSettings["TemplateDir"];
            }

            // Get the template control
            if (_templateFilename == null)
            {
                _templateFilename = ConfigurationSettings.AppSettings["DefaultTemplate"];
            }
            BasePageControl pageControl = (BasePageControl) LoadControl(ResolveUrl(_templateDir + _templateFilename));

            // Add the pagecontrol on top of the control collection of the page
            pageControl.ID = "p";
            col.AddAt(0, pageControl);

            // Get the Content placeholder
            plc = pageControl.Content;
            if (plc != null)
            {
                // Iterate through the controls in the page to find the form control.
                foreach (Control control in col)
                {
                    if (control is HtmlForm)
                    {
                        // We've found the form control. Now move all child controls into the placeholder.
                        HtmlForm formControl = (HtmlForm) control;
                        while (formControl.Controls.Count > 0)
                        {
                            // Updated 10/04/2008: Support UpdatePanel by Kien
                            if (formControl.Controls[0] is UpdatePanel)
                            {
                                UpdatePanel updatepanel = new UpdatePanel();
                                UpdatePanel oldPanel = (UpdatePanel) formControl.Controls[0];
                                while (oldPanel.ContentTemplateContainer.Controls.Count > 0)
                                {
                                    updatepanel.ContentTemplateContainer.Controls.Add(
                                        oldPanel.ContentTemplateContainer.Controls[0]);
                                }
                                plc.Controls.Add(updatepanel);
                                formControl.Controls.RemoveAt(0);
                            }
                            else
                            {
                                plc.Controls.Add(formControl.Controls[0]);
                            }
                        }
                    }
                }
                // throw away all controls in the page, except the page control 
                while (col.Count > 1)
                    col.Remove(col[1]);
            }
            base.OnInit(e);
        }

        /// <summary>
        /// Use the PreRender event to set the page title and stylesheet. These are properties of the page control
        /// which is at position 0 in the controls collection.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            BasePageControl pageControl = (BasePageControl) Controls[0];

            // Set the stylesheet and title properties
            if (_css == null)
            {
                _css = ResolveUrl(ConfigurationSettings.AppSettings["DefaultCss"]);
            }
            if (_title == null)
            {
                _title = ConfigurationSettings.AppSettings["DefaultTitle"];
            }
            pageControl.Title = _title;
            pageControl.Css = _css;
            // Kien them vao de support mot so control Ajax
            base.OnPreRender(e);
        }

        /// <summary>
        /// Resolution of the __doPostBack bug (MS .NET 1.1). Forgot where the source came from. 
        /// Perhaps somewhere from the ASP.NET forum.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringWriter stringWriter = new StringWriter(stringBuilder);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
            base.Render(htmlWriter);
            string html = stringBuilder.ToString();
            int start = html.IndexOf("<form name=\"") + 12;
            int end = html.IndexOf("\"", start);
            string formID = html.Substring(start, end - start);
            string replace = formID.Replace(":", "_");
            html = html.Replace("document." + formID, "document." + replace);
            writer.Write(html);
        }
    }
}