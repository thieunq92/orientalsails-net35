using System;
using Portal.Modules.OrientalSails.Web.Controls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class AgencySelectorPage : AgencyList
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            string clientId = Request.QueryString["clientid"];

            string script = string.Format(
    @"function Done(name, id)
{{
    idcontrol = window.opener.document.getElementById('{0}');
	idcontrol.value = id;
    namecontrol = window.opener.document.getElementById('{1}');
    namecontrol.value = name;
    window.close();
}}", clientId, clientId + AgencySelector.PNAMEID);

            Page.ClientScript.RegisterClientScriptBlock(typeof(AgencySelectorPage), "done", script, true);
        }

        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                str += "&Name=" + txtName.Text;
            }

            if (ddlRoles.SelectedIndex > 0)
            {
                str += "&RoleId=" + ddlRoles.SelectedValue;
            }

            str += "&clientid=" + Request.QueryString["clientid"];

            PageRedirect(string.Format("AgencySelectorPage.aspx?NodeId={0}&SectionId={1}{2}", Node.Id, Section.Id, str));
        }
    }
}
