using System;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.Admin.Controls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using System.Collections;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class Sales : SailsAdminBasePage
    {
        public UserSelector userSelect
        {
            get { return (UserSelector) userSelector ; }
        }

        private IList _roles;

        protected IList Roles
        {
            get {
                if (_roles ==null)
                {
                    _roles = Module.RoleGetAll();
                }
                return _roles;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //rptSales.DataSource = Module.SalesGetAll();
                //rptSales.DataBind();

                BindRoles(ddlSaleRoles);
                BindRoles(ddlSupplierRoles);
                BindRoles(ddlGuideRoles);

                if (Module.ModuleSettings("Sale")!=null)
                {
                    int id = Convert.ToInt32(Module.ModuleSettings("Sale"));
                    if (id > 0)
                    {
                        ddlSaleRoles.SelectedValue = id.ToString();
                    }
                }

                if (Module.ModuleSettings("Suppliers") != null)
                {
                    int id = Convert.ToInt32(Module.ModuleSettings("Suppliers"));
                    if (id > 0)
                    {
                        ddlSupplierRoles.SelectedValue = id.ToString();
                    }
                }

                if (Module.ModuleSettings("Guides") != null)
                {
                    int id = Convert.ToInt32(Module.ModuleSettings("Guides"));
                    if (id > 0)
                    {
                        ddlGuideRoles.SelectedValue = id.ToString();
                    }
                }
            }
        }

        protected void BindRoles(DropDownList ddlRoles)
        {
            ddlRoles.DataSource = Roles;
            ddlRoles.DataValueField = "Id";
            ddlRoles.DataTextField = "Name";
            ddlRoles.DataBind();
            ddlRoles.Items.Insert(0, "-- Unbound role --");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (userSelect.SelectedUserId > 0)
            {
                User user = Module.UserGetById(userSelect.SelectedUserId);
                Sale sale = Module.SaleGetByUser(user);
                if (sale ==null)
                {
                    sale = new Sale();
                    sale.User = user;
                    Module.SaveOrUpdate(sale);
                }
            }
        }

        protected void rptSales_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Sale)
            {
                Sale sale = (Sale) e.Item.DataItem;
                Literal litUsername = e.Item.FindControl("litUsername") as Literal;
                if (litUsername!=null)
                {
                    litUsername.Text = sale.User.UserName;
                }

                Literal litName = e.Item.FindControl("litName") as Literal;
                if (litName!=null)
                {
                    litName.Text = sale.User.FullName;
                }


            }
        }

        protected void rptSales_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    Module.Delete(Module.SaleGetById(Convert.ToInt32(e.CommandArgument)));
                    break;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlSaleRoles.SelectedIndex > 0)
            {
                Module.SaveModuleSetting("Sale", ddlSaleRoles.SelectedValue);
            }

            if (ddlSupplierRoles.SelectedIndex > 0)
            {
                Module.SaveModuleSetting("Suppliers", ddlSupplierRoles.SelectedValue);
            }

            if (ddlGuideRoles.SelectedIndex > 0)
            {
                Module.SaveModuleSetting("Guides", ddlGuideRoles.SelectedValue);
            }
        }
    }
}
