using System;
using System.Collections;
using System.Globalization;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class CruiseConfig : SailsAdminBase
    {
        private CruiseExpenseTable _table;

        protected CruiseExpenseTable ActiveTable
        {
            get
            {
                if (_table ==null && Request.QueryString["TableId"] != null)
                {
                    _table = Module.CruiseTableGetById(Convert.ToInt32(Request.QueryString["TableId"]));
                }
                return _table;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCruises.DataSource = Module.CruiseGetAll();
                ddlCruises.DataTextField = "Name";
                ddlCruises.DataValueField = "Id";
                ddlCruises.DataBind();

                rptCruiseTables.DataSource = Module.CruiseTableGetAll();
                rptCruiseTables.DataBind();

                if (ActiveTable!=null)
                {
                    txtValidFrom.Text = ActiveTable.ValidFrom.ToString("dd/MM/yyyy");
                    txtValidTo.Text = ActiveTable.ValidTo.Value.ToString("dd/MM/yyyy");
                    if (ActiveTable.Cruise!=null)
                    {
                        ddlCruises.SelectedValue = ActiveTable.Cruise.Id.ToString();
                    }
                    rptCruiseExpenses.DataSource = ActiveTable.Expenses;
                    rptCruiseExpenses.DataBind();
                }
            }
            string url = string.Format("CruiseConfig.aspx?NodeId={0}&SectionId={1}",Node.Id,Section.Id);
            inputNew.Attributes.Add("onclick",string.Format("window.location = '{0}'", url));
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            IList list = rptCruiseExpensesToIList();
            list.Add(new CruiseExpense());
            rptCruiseExpenses.DataSource = list;
            rptCruiseExpenses.DataBind();
        }

        protected IList rptCruiseExpensesToIList()
        {
            IList result = new ArrayList();
            foreach (RepeaterItem item in rptCruiseExpenses.Items)
            {
                HiddenField hiddenId = (HiddenField)item.FindControl("hiddenId");
                TextBox txtFrom = (TextBox) item.FindControl("txtFrom");
                TextBox txtTo = (TextBox)item.FindControl("txtTo");
                TextBox txtCost = (TextBox)item.FindControl("txtCost");
                CruiseExpense expense = new CruiseExpense();
                if (!string.IsNullOrEmpty(hiddenId.Value))
                {
                    expense.Id = Convert.ToInt32(hiddenId.Value);
                }
                expense.CustomerFrom = Convert.ToInt32(txtFrom.Text);
                expense.CustomerTo = Convert.ToInt32(txtTo.Text);
                expense.Price = Convert.ToDouble(txtCost.Text);
                expense.Currency = 1;
                result.Add(expense);
            }
            return result;
        }

        protected void rptCruiseExpenses_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is CruiseExpense)
            {
                CruiseExpense expense = (CruiseExpense) e.Item.DataItem;
                HiddenField hiddenId = (HiddenField) e.Item.FindControl("hiddenId");
                TextBox txtFrom = (TextBox) e.Item.FindControl("txtFrom");
                TextBox txtTo = (TextBox) e.Item.FindControl("txtTo");
                TextBox txtCost = (TextBox) e.Item.FindControl("txtCost");

                hiddenId.Value = expense.Id.ToString();
                txtFrom.Text = expense.CustomerFrom.ToString();
                txtTo.Text = expense.CustomerTo.ToString();
                txtCost.Text = expense.Price.ToString("#,0.#");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IList list = ValidateData();
            if (list!=null)
            {
                if (ActiveTable==null)
                {
                    _table = new CruiseExpenseTable();
                }

                _table.ValidFrom = DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _table.ValidTo = DateTime.ParseExact(txtValidTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (ddlCruises.SelectedIndex >= 0)
                {
                    _table.Cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
                }

                Module.SaveOrUpdate(_table);

                foreach (CruiseExpense expense in list)
                {
                    expense.Table = _table;
                    Module.SaveOrUpdate(expense);
                }
            }
        }

        protected IList ValidateData()
        {
            ArrayList list = new ArrayList(rptCruiseExpensesToIList());
            list.Sort();

            for (int ii=0; ii < list.Count; ii++)
            {
                CruiseExpense expense = (CruiseExpense) list[ii];
                if (expense.CustomerFrom > expense.CustomerTo)
                {
                    ShowError(string.Format("Customer from must be smaller than customer to ({0} > {1})",expense.CustomerFrom, expense.CustomerTo));
                    return null;
                }

                if (expense.CustomerFrom < 0 || expense.CustomerTo < 0)
                {
                    ShowError("Value cannot be negative");
                    return null;
                }

                if (ii < list.Count - 1)
                {
                    CruiseExpense expenseNext = (CruiseExpense)list[ii + 1];
                    if (expenseNext.CustomerFrom - expense.CustomerTo > 1)
                    {
                        ShowError(string.Format("Lack of data for customer from {0} to {1}", expense.CustomerTo + 1, expenseNext.CustomerFrom -1));
                        return null;
                    }

                    if (expenseNext.CustomerFrom - expense.CustomerTo < 1)
                    {
                        ShowError(string.Format("Duplicate data for customer from {1} to {0}", expense.CustomerTo, expenseNext.CustomerFrom));
                        return null;
                    }
                }
            }

            return list;
        }

        protected void rptCruiseTables_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is CruiseExpenseTable)
            {
                CruiseExpenseTable table = (CruiseExpenseTable) e.Item.DataItem;
                HyperLink hyperLinkEdit = e.Item.FindControl("hyperLinkEdit") as HyperLink;
                if (hyperLinkEdit!=null)
                {
                    hyperLinkEdit.NavigateUrl = string.Format("CruiseConfig.aspx?NodeId={0}&SectionId={1}&TableId={2}",
                                                              Node.Id, Section.Id, table.Id);
                }

                Literal litValidFrom = e.Item.FindControl("litValidFrom") as Literal;
                if (litValidFrom!=null)
                {
                    litValidFrom.Text = table.ValidFrom.ToString("dd/MM/yyyy");
                }

                Literal litValidTo = e.Item.FindControl("litValidTo") as Literal;
                if (litValidTo!=null)
                {
                    litValidTo.Text = table.ValidTo.Value.ToString("dd/MM/yyyy");
                }

                Literal litCruise = e.Item.FindControl("litCruise") as Literal;
                if (litCruise!=null)
                {
                    if (table.Cruise!=null)
                    {
                        litCruise.Text = table.Cruise.Name;
                    }
                }
            }
        }

        protected void rptCruiseTables_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            CruiseExpenseTable table = Module.CruiseTableGetById(Convert.ToInt32(e.CommandArgument));
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    Module.Delete(table);
                    break;
            }
        }
    }
}
