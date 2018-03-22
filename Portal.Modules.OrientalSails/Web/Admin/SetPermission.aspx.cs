using System;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.ServerControls;
using Portal.Modules.OrientalSails.Web.UI;
using CMS.Core.Util;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SetPermission : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserIdentity.HasPermission(AccessLevel.Administrator))
            {
                ShowError("You must be administrator to use this function");
                return;
            }
            if (!IsPostBack)
            {
                string[] listToRemoveArray =   {"Anonymous user", "Authenticated user", "Editor", "Agent $71", "Agent $68", "Agent $69", "Agent $70", "Agent $72", "Agent $73", "Agent $74", "Agent $75"};

                var listToRemoves = new List<string>();
                listToRemoves.AddRange(listToRemoveArray);

                IList roles = (IList)Module.RoleGetAll();

                var listRoleToRemove = new List<Role>();
                foreach(Role role in roles){
                    if(listToRemoves.Contains(role.Name)){
                        listRoleToRemove.Add(role);
                    }
                }

                foreach (Role role in listRoleToRemove)
                {
                    roles.Remove(role);
                }

                rptRoles.DataSource = roles;
                rptRoles.DataBind();
                rptUsersActive.DataSource = Module.GetUsersActive();
                rptUsersActive.DataBind();
                rptUsersInActive.DataSource = Module.GetUsersInActive();
                rptUsersInActive.DataBind();     
            }
        }

        protected void pagerRoles_PageChanged(object sender, CMS.ServerControls.PageChangedEventArgs e)
        {
            string[] listToRemoveArray = { "Anonymous user", "Authenticated user", "Editor", "Agent $71", "Agent $68", "Agent $69", "Agent $70", "Agent $72", "Agent $73", "Agent $74", "Agent $75" };

            var listToRemoves = new List<string>();
            listToRemoves.AddRange(listToRemoveArray);

            IList roles = (IList)Module.RoleGetAll();

            var listRoleToRemove = new List<Role>();
            foreach (Role role in roles)
            {
                if (listToRemoves.Contains(role.Name))
                {
                    listRoleToRemove.Add(role);
                }
            }

            foreach (Role role in listRoleToRemove)
            {
                roles.Remove(role);
            }
            this.pagerRoles.CurrentPageIndex = e.CurrentPage;
            rptRoles.DataSource = roles;
            rptRoles.DataBind();
        }

        protected void pagerUserActive_PageChanged(object sender, CMS.ServerControls.PageChangedEventArgs e)
        {
            this.pagerUserActive.CurrentPageIndex = e.CurrentPage;
            rptUsersActive.DataSource = Module.GetUsersActive();
            rptUsersActive.DataBind();
        }

        protected void pagerUserInActive_PageChanged(object sender, CMS.ServerControls.PageChangedEventArgs e)
        {
            this.pagerUserInActive.CurrentPageIndex = e.CurrentPage;
            rptUsersInActive.DataSource = Module.GetUsersInActive();
            rptUsersInActive.DataBind();
        }

        protected void rptRoles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Role)
            {
                Role role = (Role)e.Item.DataItem;
                if (role.HasPermission(AccessLevel.Administrator))
                {
                    e.Item.Visible = false;
                    return;
                }
                Literal litName = e.Item.FindControl("litName") as Literal;
                if (litName != null)
                {
                    litName.Text = role.Name;
                }
                HyperLink hplSetPermission = e.Item.FindControl("hplSetPermission") as HyperLink;
                if (hplSetPermission != null)
                {
                    hplSetPermission.NavigateUrl = string.Format("Permissions.aspx?NodeId={0}&SectionId={1}&roleid={2}",
                                                          Node.Id, Section.Id, role.Id);
                }
            }
        }

        protected void rptUsersActive_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            User user = e.Item.DataItem as User;

            if (user != null)
            {
                Label lblLastLogin = e.Item.FindControl("lblLastLogin") as Label;
                if (user.LastLogin != null)
                {
                    lblLastLogin.Text = TimeZoneUtil.AdjustDateToUserTimeZone(user.LastLogin.Value, this.Page.User.Identity).ToString();
                }

                HyperLink hplEdit = (HyperLink)e.Item.FindControl("hplEdit");

                // HACK: as long as ~/ doesn't work properly in mono we have to use a relative path from the Controls
                // directory due to the template construction.
                hplEdit.NavigateUrl = String.Format("~/Admin/UserEdit.aspx?UserId={0}", user.Id);

                HyperLink hplSetPermission = e.Item.FindControl("hplSetPermission") as HyperLink;
                if (hplSetPermission != null)
                {
                    hplSetPermission.NavigateUrl = string.Format("Permissions.aspx?NodeId={0}&SectionId={1}&userid={2}",
                                                          Node.Id, Section.Id, user.Id);
                }

            }
        }

        protected void rptUsersInActive_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            User user = e.Item.DataItem as User;

            if (user != null)
            {
                Label lblLastLogin = e.Item.FindControl("lblLastLogin") as Label;
                if (user.LastLogin != null)
                {
                    lblLastLogin.Text = TimeZoneUtil.AdjustDateToUserTimeZone(user.LastLogin.Value, this.Page.User.Identity).ToString();
                }

                HyperLink hplEdit = (HyperLink)e.Item.FindControl("hplEdit");

                // HACK: as long as ~/ doesn't work properly in mono we have to use a relative path from the Controls
                // directory due to the template construction.
                hplEdit.NavigateUrl = String.Format("~/Admin/UserEdit.aspx?UserId={0}", user.Id);

                HyperLink hplSetPermission = e.Item.FindControl("hplSetPermission") as HyperLink;
                if (hplSetPermission != null)
                {
                    hplSetPermission.NavigateUrl = string.Format("Permissions.aspx?NodeId={0}&SectionId={1}&userid={2}",
                                                          Node.Id, Section.Id, user.Id);
                }

            }
        }

        protected void btnFind_Click(object sender, System.EventArgs e)
        {
            BindUsersActive();
            BindUsersInActive();
        }

        public void BindUsersActive()
        {
            GetUserActive();
            this.rptUsersActive.DataBind();
        }

        public void BindUsersInActive()
        {
            GetUserInActive();
            this.rptUsersInActive.DataBind();
        }

        public void GetUserActive()
        {

            DateTime? from = null;
            DateTime? to = null;

            ICriterion criterion = Expression.Eq("IsActive", true);
            if (!String.IsNullOrEmpty(txtUsername.Text))
            {
                criterion = Expression.And(criterion, Expression.InsensitiveLike("UserName", txtUsername.Text, MatchMode.Anywhere));
            }

            if (!String.IsNullOrEmpty(txtFrom.Text))
            {
                from = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(txtTo.Text))
            {
                to = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(txtUsername.Text))
            {
                criterion = Expression.And(criterion, Expression.InsensitiveLike("UserName", txtUsername.Text, MatchMode.Anywhere));
            }

            if (from.HasValue)
            {
                criterion = Expression.And(criterion, Expression.Ge("LastLogin", from.Value));
            }

            if (to.HasValue)
            {
                criterion = Expression.And(criterion, Expression.Le("LastLogin", to));
            }

            this.rptUsersActive.DataSource = Module.GetObject<User>(criterion, 0, 0);
        }

        public void GetUserInActive()
        {
            DateTime? from = null;
            DateTime? to = null;

            ICriterion criterion = Expression.Eq("IsActive", false);
            if (!String.IsNullOrEmpty(txtUsername.Text))
            {
                criterion = Expression.And(criterion, Expression.InsensitiveLike("UserName", txtUsername.Text, MatchMode.Anywhere));
            }

            if (!String.IsNullOrEmpty(txtFrom.Text))
            {
                from = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(txtTo.Text))
            {
                to = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(txtUsername.Text))
            {
                criterion = Expression.And(criterion, Expression.InsensitiveLike("UserName", txtUsername.Text, MatchMode.Anywhere));
            }

            if (from.HasValue)
            {
                criterion = Expression.And(criterion, Expression.Ge("LastLogin", from.Value));
            }

            if (to.HasValue)
            {
                criterion = Expression.And(criterion, Expression.Le("LastLogin", to));
            }

            this.rptUsersInActive.DataSource = Module.GetObject<User>(criterion, 0, 0);
        }
    }
}
