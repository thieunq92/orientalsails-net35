using System;
using System.Web.UI.WebControls;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class AgencyLocations : SailsAdminBasePage
    {
        #region -- PRIVATE MEMBERS --

        private AgencyLocation _activeCost;

        /// <summary>
        /// Biến ViewState lưu Service hiện tại
        /// </summary>
        private AgencyLocation ActiveCost
        {
            get
            {
                if (_activeCost != null)
                {
                    return _activeCost;
                }
                if (ViewState["serviceId"] != null && Convert.ToInt32(ViewState["serviceId"]) > 0)
                {
                    return Module.GetObject<AgencyLocation>(Convert.ToInt32(ViewState["serviceId"]));
                }
                _activeCost = new AgencyLocation();
                return _activeCost;
            }
            set
            {
                _activeCost = value;
                ViewState["serviceId"] = value.Id;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Locations";
            if (!IsPostBack)
            {
                var list = Module.GetObject<AgencyLocation>(Expression.IsNull("Parent"), 0, 0);

                rptServices.DataSource = list;
                rptServices.DataBind();
                labelFormTitle.Text = "New location";
                btnDelete.Visible = false;
                btnDelete.Enabled = false;

                ddlSuppliers.DataSource = list;
                ddlSuppliers.DataTextField = "Name";
                ddlSuppliers.DataValueField = "Id";
                ddlSuppliers.DataBind();
                ddlSuppliers.Items.Insert(0, "-- Parent --");

                //ddlServices.DataSource = Module.ExtraOptionGetAll();
                //ddlServices.DataTextField = "Name";
                //ddlServices.DataValueField = "Id";
                //ddlServices.DataBind();
                //ddlServices.Items.Insert(0, "-- Associated service --");
            }
        }

        protected void rptServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            AgencyLocation service = e.Item.DataItem as AgencyLocation;
            if (service != null)
            {
                using (LinkButton lbtEdit = e.Item.FindControl("lbtEdit") as LinkButton)
                {
                    if (lbtEdit != null)
                    {
                        // Gán text và command argument, điều này cũng có thể làm ngay trên aspx
                        lbtEdit.Text = service.Name;
                        lbtEdit.CommandArgument = service.Id.ToString();
                    }
                }

                var rptChild = e.Item.FindControl("rptChild") as Repeater;
                if (rptChild != null)
                {
                    rptChild.DataSource = service.Child;
                    rptChild.DataBind();
                }
            }
        }

        protected void rptServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "edit":

                    #region -- Lấy thông tin dịch vụ

                    ActiveCost = Module.GetObject<AgencyLocation>(Convert.ToInt32(e.CommandArgument));
                    txtServiceName.Text = ActiveCost.Name;
                    if (ActiveCost.Parent != null)
                    {
                        ddlSuppliers.SelectedValue = ActiveCost.Parent.Id.ToString();
                    }
                    else
                    {
                        ddlSuppliers.SelectedIndex = 0;
                    }
                    #endregion

                    btnDelete.Visible = true;
                    btnDelete.Enabled = true;
                    labelFormTitle.Text = ActiveCost.Name;
                    break;
                default:
                    break;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ActiveCost = new AgencyLocation();
            txtServiceName.Text = String.Empty;
            labelFormTitle.Text = "New location";
            ddlSuppliers.SelectedIndex = 0;
            btnDelete.Visible = false;
            btnDelete.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ActiveCost.Name = txtServiceName.Text;
            if (ddlSuppliers.SelectedIndex > 0)
            {
                ActiveCost.Parent = Module.GetObject<AgencyLocation>(Convert.ToInt32(ddlSuppliers.SelectedValue));
            }
            else
            {
                ActiveCost.Parent = null;
            }

            // Kiểm tra trong View State
            Module.SaveOrUpdate(ActiveCost);
            ActiveCost = ActiveCost;
            labelFormTitle.Text = ActiveCost.Name;
            rptServices.DataSource = Module.GetObject<AgencyLocation>(Expression.IsNull("Parent"), 0, 0);
            rptServices.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Module.Delete(ActiveCost);
            btnAdd_Click(sender, e);
            rptServices.DataSource = Module.GetObject<AgencyLocation>(Expression.IsNull("Parent"), 0, 0);
            rptServices.DataBind();
        }
    }
}