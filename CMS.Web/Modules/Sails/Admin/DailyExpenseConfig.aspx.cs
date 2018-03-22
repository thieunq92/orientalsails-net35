using System;
using System.Collections;
using System.Globalization;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class DailyExpenseConfig : SailsAdminBasePage
    {
        protected IList _costingList;
        protected DailyCostTable _table;

        protected IList CostingList
        {
            get
            {
                if (_costingList == null)
                {
                    _costingList = new ArrayList();
                    foreach (CostType type in Module.CostTypeGetAutoDailyBase())
                    {
                        if (type.Id != SailsModule.GUIDE_COST && type.Id != SailsModule.HAIPHONG)
                        {
                            _costingList.Add(type);
                        }
                    }
                }
                return _costingList;
            }
        }

        protected DailyCostTable ActiveTable
        {
            get
            {
                if (_table == null)
                {
                    if (Request.QueryString["TableId"] != null)
                    {
                        _table = Module.DailyCostTableGetById(Convert.ToInt32(Request.QueryString["TableId"]));
                    }
                    else
                    {
                        _table = new DailyCostTable();
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
                rptCostingTable.DataSource = Module.DailyCostTableGetAll();
                rptCostingTable.DataBind();

                if (ActiveTable.Id > 0)
                {
                    txtValidFrom.Text = ActiveTable.ValidFrom.ToString("dd/MM/yyyy");

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
        }

        protected void rptCostingTable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is DailyCostTable)
            {
                DailyCostTable table = (DailyCostTable) e.Item.DataItem;
                Literal litValidFrom = e.Item.FindControl("litValidFrom") as Literal;
                if (litValidFrom != null)
                {
                    litValidFrom.Text = table.ValidFrom.ToString("dd/MM/yyyy");
                }

                HyperLink hyperLinkEdit = e.Item.FindControl("hyperLinkEdit") as HyperLink;
                if (hyperLinkEdit != null)
                {
                    hyperLinkEdit.NavigateUrl =
                        string.Format("DailyExpenseConfig.aspx?NodeId={0}&SectionId={1}&TableId={2}",
                                      Node.Id, Section.Id, table.Id);
                }
            }
        }

        protected void rptCostingTable_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    Module.Delete(Module.DailyCostTableGetById(Convert.ToInt32(e.CommandArgument)));
                    rptCostingTable.DataSource = Module.DailyCostTableGetAll();
                    rptCostingTable.DataBind();
                    break;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PageRedirect(string.Format("DailyExpenseConfig.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ActiveTable.ValidFrom = DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Module.SaveOrUpdate(ActiveTable);

            foreach (RepeaterItem item in rptServices.Items)
            {
                HiddenField hiddenType = (HiddenField) item.FindControl("hiddenType");
                HiddenField hiddenId = (HiddenField) item.FindControl("hiddenId");
                DailyCost cost;
                if (!string.IsNullOrEmpty(hiddenId.Value) && Convert.ToInt32(hiddenId.Value) > 0)
                {
                    cost = Module.DailyCostGetById(Convert.ToInt32(hiddenId.Value));
                }
                else
                {
                    cost = new DailyCost();
                }
                TextBox txtAdult = (TextBox) item.FindControl("txtAdult");

                cost.Cost = Convert.ToDouble(txtAdult.Text);

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

            PageRedirect(string.Format("DailyExpenseConfig.aspx?NodeId={0}&SectionId={1}&TableId={2}", Node.Id,
                                       Section.Id, ActiveTable.Id));
        }

        protected void rptServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is CostType)
            {
                CostType type = (CostType) e.Item.DataItem;
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

            if (e.Item.DataItem is DailyCost)
            {
                DailyCost cost = (DailyCost) e.Item.DataItem;
                HiddenField hiddenId = (HiddenField) e.Item.FindControl("hiddenId");
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

                TextBox txtAdult = (TextBox) e.Item.FindControl("txtAdult");

                txtAdult.Text = cost.Cost.ToString("0");
            }
        }
    }
}