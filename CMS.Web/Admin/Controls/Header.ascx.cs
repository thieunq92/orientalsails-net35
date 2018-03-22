using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Admin.UI;
using CMS.Web.Util;

namespace CMS.Web.Admin.Controls
{
    // using System.Data;

    /// <summary>
    ///		Summary description for Header.
    /// </summary>
    public class Header : UserControl
    {
        private AdminBasePage _page;
        protected HyperLink hplSite;

        protected LinkButton lbtLogout;

        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _page = (AdminBasePage) Page;
            }
            catch (InvalidCastException ex)
            {
                throw new Exception("This control requires a Page of the type AdminBasePage.", ex);
            }
            if (_page.ActiveSection != null && _page.ActiveSection.Id>0)
            {
                hplSite.NavigateUrl = UrlHelper.GetFullUrlFromSectionViaSite(_page.ActiveSection);
            }
            else if (_page.ActiveNode != null && _page.ActiveNode.Id > 0)
            {
                hplSite.NavigateUrl = UrlHelper.GetFullUrlFromNodeViaSite(_page.ActiveNode);
            }
            else if (_page.ActiveSite != null)
            {
                hplSite.NavigateUrl = _page.ActiveSite.SiteUrl;
            }
            else
            {
                hplSite.Visible = false;
            }
            lbtLogout.Visible = Page.User.Identity.IsAuthenticated;
        }

        private void lbtLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Context.Response.Redirect(Context.Request.RawUrl);
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
            this.lbtLogout.Click += new System.EventHandler(this.lbtLogout_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}