using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Domain;
using NHibernate.Criterion;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class HaiPhongExpenses : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int contractId = 0;
            try
            {
                contractId = Int32.Parse(Request.QueryString["Contract"]);
            }
            catch (FormatException ex)
            {
                ShowError("Contract Id không đúng định dạng. Kiểm tra lại tham số contract trên url.");
                return;
            }
            var contract = Module.GetObject<HaiPhongContract>(contractId);
            if (contract == null)
            {
                ShowError("Không tìm thấy contract. Kiểm tra lại tham số contract trên url.");
                return;
            }

            int cruiseId = 0;
            try
            {
                cruiseId = Int32.Parse(Request.QueryString["Cruise"]);
            }
            catch (FormatException ex)
            {
                ShowError(" Cruise Id không đúng định dạng. Kiểm tra lại tham số cruise trên url.");
                return;
            }
            var cruise = Module.GetObject<Cruise>(cruiseId);

            if (cruise == null)
            {
                ShowError(" Không tìm thấy cruise. Kiểm tra lại tham số cruise trên url.");
                return;
            }

            ltrCruiseName.Text = cruise.Name;
            ltrContractName.Text = contract.Name;
            ltrContractApplyFrom.Text = contract.ApplyFrom.ToString("dd/MM/yyyy");

            var criterion = Expression.Eq("Cruise.Id", cruise.Id);
            rptExpenseTypeTitle.DataSource = Module.GetObject<CruiseHaiPhongExpenseType>(criterion, 0, 0);
            rptExpenseTypeTitle.DataBind();

            rptExpenseType.DataSource = Module.GetObject<CruiseHaiPhongExpenseType>(criterion, 0, 0);
            rptExpenseType.DataBind();
        }

        protected void rptExpenseType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var haiPhongExpenseType = e.Item.DataItem as CruiseHaiPhongExpenseType;
            var rptExpenseCustomerType = e.Item.FindControl("rptExpenseCustomerType") as Repeater;
            var rptExpenseCharter = e.Item.FindControl("rptExpenseCharter") as Repeater;
            var criterion = Expression.Eq("HaiPhongExpenseType.HaiPhongExpenseTypeId", haiPhongExpenseType.HaiPhongExpenseType.HaiPhongExpenseTypeId);

            rptExpenseCustomerType.DataSource = Module.GetObject<HaiPhongExpenseTypeHaiPhongExpenseCustomerType>(criterion, 0, 0).OrderBy(o => o.OrderNumber);
            rptExpenseCustomerType.DataBind();

            rptExpenseCharter.DataSource = Module.GetObject<HaiPhongExpenseCharter>(criterion, 0, 0);
            rptExpenseCharter.DataBind();
        }

        protected void rptExpenseCustomerType_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            var rptExpense = e.Item.FindControl("rptExpense") as Repeater;
            var rptReduceExpense = e.Item.FindControl("rptReduceExpense") as Repeater;
            var haiPhongExpenseTypeHaiPhongExpenseCustomerType = e.Item.DataItem as HaiPhongExpenseTypeHaiPhongExpenseCustomerType;

            var expenseCriterion = Expression.Eq("HaiPhongExpenseTypeHaiPhongExpenseCustomerType.HaiPhongExpenseTypeHaiPhongExpenseCustomerTypeId", haiPhongExpenseTypeHaiPhongExpenseCustomerType.HaiPhongExpenseTypeHaiPhongExpenseCustomerTypeId);

            rptExpense.DataSource = Module.GetObject<HaiPhongExpense>(expenseCriterion, 0, 0);
            rptExpense.DataBind();

            var reduceExpenseCriterion = Expression.Eq("HaiPhongExpenseTypeHaiPhongExpenseCustomerType.HaiPhongExpenseTypeHaiPhongExpenseCustomerTypeId", haiPhongExpenseTypeHaiPhongExpenseCustomerType.HaiPhongExpenseTypeHaiPhongExpenseCustomerTypeId);

            rptReduceExpense.DataSource = Module.GetObject<HaiPhongReduceExpense>(reduceExpenseCriterion, 0, 0);
            rptReduceExpense.DataBind();

        }
    }
}