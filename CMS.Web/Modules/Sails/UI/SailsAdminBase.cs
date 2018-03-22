using System;
using System.IO;
using System.Web;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Web.UI
{
    public class SailsAdminBase : SailsAdminBasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            bool load = true;
            // Check Permission
            string permissionname = string.Format("form_{0}", GetCurrentPageName()).ToUpper();
            if (!UserIdentity.HasPermission(AccessLevel.Administrator))
            {
                if (!Module.PermissionCheck(permissionname, UserIdentity))
                {
                    ShowError("You do not have permission to access this form");
                    load = false;
                }
            }
            if (load)
            {
                base.OnLoad(e);
            }
            else
            {
                Page.Master.FindControl("AdminContent").Visible = false;
            }
        }

        public string GetCurrentPageName()
        {
            string sPath = HttpContext.Current.Request.Url.AbsolutePath;
            FileInfo oInfo = new FileInfo(sPath);
            string sRet = oInfo.Name.Substring(0, oInfo.Name.IndexOf('.'));
            return sRet;
        }
    }
}