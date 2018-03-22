using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Web.Admin.UI;
using CMS.Web.UI;

namespace CMS.Web.Admin.Controls
{
    // using System.Data;

    /// <summary>
    ///		Summary description for AdminTemplate.
    /// </summary>
    public class AdminTemplate : BasePageControl
    {
        protected HtmlGenericControl MessageBox;
        protected Literal PageTitleLabel;

        private void Page_Load(object sender, EventArgs e)
        {
        }


        private void AdminTemplate_PreRender(object sender, EventArgs e)
        {
            if (Page is AdminBasePage)
                PageTitleLabel.Text = PageTitle.Text;
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
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new EventHandler(AdminTemplate_PreRender);
        }

        #endregion
    }
}