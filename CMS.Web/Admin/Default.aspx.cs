using System;
using CMS.Web.Admin.UI;
// using System.Data;

namespace CMS.Web.Admin
{
    /// <summary>
    /// Summary description for _Default.
    /// </summary>
    public class _Default : AdminBasePage
    {
        private void Page_Load(object sender, EventArgs e)
        {
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}