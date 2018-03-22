using System;
using System.Collections;
using System.Globalization;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class Costing : SailsAdminBase
    {
        protected CostingTable _table;
        protected IList _costingList;
        
        protected IList CostingList
        {
            get
            {
                if (_costingList ==null)
                {
                    _costingList = new ArrayList();
                    foreach (CostType type in Module.CostTypeGetCustomerBase())
                    {
                        if (type.Id!= SailsModule.GUIDE_COST && type.Id!= SailsModule.HAIPHONG)
                        {
                            _costingList.Add(type);
                        }
                    }
                }
                return _costingList;
            }
        }
        
        protected CostingTable ActiveTable
        {
            get
            {
                if (_table==null)
                {
                    if(Request.QueryString["TableId"]!=null)
                    {
                        _table = Module.CostingTableGetById(Convert.ToInt32(Request.QueryString["TableId"]));
                    }
                    else
                    {
                        _table = new CostingTable();
                    }
                }
                return _table;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Resources.textCostingConfiguration;
            if (!IsPostBack)
            {
                rptCostingTable.DataSource = Module.CostingTableGetAll();
                rptCostingTable.DataBind();

                BindTrips();

                if (ActiveTable.Id > 0)
                {
                    txtValidFrom.Text = ActiveTable.ValidFrom.ToString("dd/MM/yyyy");                    

                    if (ActiveTable.Trip!=null)
                    {
                        ddlTrips.SelectedValue = ActiveTable.Trip.Id.ToString();
                        ddlOptions.SelectedIndex =(int) ActiveTable.Option;

                        if (ActiveTable.Trip.NumberOfOptions > 0)
                        {
                            ddlOptions.Attributes.CssStyle["display"] = "";
                        }
                    }
                    rptServices.DataSource = ActiveTable.GetActiveCost(CostingList);
                    rptServices.DataBind();
                    btnAdd.Visible = true;
                }
                else
                {
                    rptServices.DataSource = CostingList;
                    rptServices.DataBind();
                }
            }

            string vids = string.Empty;
            foreach (SailsTrip trip in Module.TripGetAll(true))
            {
                if (trip.NumberOfOptions == 2)
                {
                    vids += "#" + trip.Id + "#";
                }
            }
            ddlTrips.Attributes.Add("onChange", string.Format("ddltype_changed('{0}','{1}','{2}')", ddlTrips.ClientID, ddlOptions.ClientID, vids));
        }

        protected void BindTrips()
        {
            ddlTrips.DataSource = Module.TripGetAll(true);
            ddlTrips.DataTextField = "Name";
            ddlTrips.DataValueField = "Id";
            ddlTrips.DataBind();
        }

        protected void rptCostingTable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is CostingTable)
            {
                CostingTable table = (CostingTable) e.Item.DataItem;
                Literal litValidFrom = e.Item.FindControl("litValidFrom") as Literal;
                if (litValidFrom != null)
                {
                    litValidFrom.Text = table.ValidFrom.ToString("dd/MM/yyyy");
                }

                HyperLink hyperLinkEdit = e.Item.FindControl("hyperLinkEdit") as HyperLink;
                if (hyperLinkEdit!=null)
                {
                    hyperLinkEdit.NavigateUrl = string.Format("Costing.aspx?NodeId={0}&SectionId={1}&TableId={2}",
                                                              Node.Id, Section.Id, table.Id);
                }

                Literal litTrip = e.Item.FindControl("litTrip") as Literal;
                if (litTrip!=null)
                {
                    if (table.Trip!=null)
                    {
                        litTrip.Text = table.Trip.Name;
                    }
                }

                Literal litOption = e.Item.FindControl("litOption") as Literal;
                if (litOption!=null)
                {
                    if (table.Trip!=null)
                    {
                        if (table.Trip.NumberOfOptions > 0)
                        {
                            litOption.Text = table.Option.ToString();
                        }
                    }
                }
            }
        }

        protected void rptCostingTable_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    Module.Delete(Module.CostingTableGetById(Convert.ToInt32(e.CommandArgument)));
                    rptCostingTable.DataSource = Module.CostingTableGetAll();
                    rptCostingTable.DataBind();
                    break;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PageRedirect(string.Format("Costing.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ActiveTable.ValidFrom = DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ActiveTable.Trip = Module.TripGetById(Convert.ToInt32(ddlTrips.SelectedValue));
            if (ActiveTable.Trip.NumberOfOptions > 1)
            {
                ActiveTable.Option = (TripOption) ddlOptions.SelectedIndex;
            }
            else
            {
                ActiveTable.Option = TripOption.Option1;
            }
            Module.SaveOrUpdate(ActiveTable);

            foreach (RepeaterItem item in rptServices.Items)
            {
                HiddenField hiddenType = (HiddenField) item.FindControl("hiddenType");
                HiddenField hiddenId = (HiddenField)item.FindControl("hiddenId");
                Domain.Costing cost;
                if (!string.IsNullOrEmpty(hiddenId.Value) && Convert.ToInt32(hiddenId.Value) > 0)
                {
                    cost = Module.CostingGetById(Convert.ToInt32(hiddenId.Value));
                }
                else
                {
                    cost = new Domain.Costing();
                }
                TextBox txtAdult = (TextBox) item.FindControl("txtAdult");
                TextBox txtChild = (TextBox)item.FindControl("txtChild");
                TextBox txtBaby = (TextBox)item.FindControl("txtBaby");

                cost.Adult = Convert.ToDouble(txtAdult.Text);
                cost.Child = Convert.ToDouble(txtChild.Text);
                cost.Baby = Convert.ToDouble(txtBaby.Text);

                foreach (CostType type in CostingList)
                {
                    if (type.Id == Convert.ToInt32(hiddenType.Value))
                    {
                        cost.Type = type;
                    }
                }                

                cost.Table = ActiveTable;
                Module.SaveOrUpdate(cost);
            }

            PageRedirect(string.Format("Costing.aspx?NodeId={0}&SectionId={1}&TableId={2}", Node.Id,Section.Id, ActiveTable.Id));
        }

        protected void rptServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is CostType)
            {
                CostType type = (CostType)e.Item.DataItem;
                Literal litName = e.Item.FindControl("litName") as Literal;
                if (litName != null)
                {
                    litName.Text = type.Name;
                }
                HiddenField hiddenType = e.Item.FindControl("hiddenType") as HiddenField;
                if (hiddenType != null)
                {
                    hiddenType.Value = type.Id.ToString();
                }
            }

            if (e.Item.DataItem is Domain.Costing)
            {
                Domain.Costing cost = (Domain.Costing)e.Item.DataItem;
                HiddenField hiddenId = (HiddenField)e.Item.FindControl("hiddenId");
                hiddenId.Value = cost.Id.ToString();
                Literal litName = e.Item.FindControl("litName") as Literal;
                if (litName != null)
                {
                    litName.Text = cost.Type.Name;
                }
                HiddenField hiddenType = e.Item.FindControl("hiddenType") as HiddenField;
                if (hiddenType != null)
                {
                    hiddenType.Value = (cost.Type.Id).ToString();
                }

                TextBox txtAdult = (TextBox)e.Item.FindControl("txtAdult");
                TextBox txtChild = (TextBox)e.Item.FindControl("txtChild");
                TextBox txtBaby = (TextBox)e.Item.FindControl("txtBaby");

                txtAdult.Text = cost.Adult.ToString("0");
                txtChild.Text = cost.Child.ToString("0");
                txtBaby.Text = cost.Baby.ToString("0");
            }
        }
    }
}
