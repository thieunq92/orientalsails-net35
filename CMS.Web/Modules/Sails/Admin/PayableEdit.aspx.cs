using System;
using System.Web.UI;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class PayableEdit : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int paymentId = Convert.ToInt32(Request.QueryString["Payable"]);
                SailExpensePayment payment = Module.PaymentGetById(paymentId);

                #region -- get data --
                litDate.Text = payment.Expense.Date.ToString("dd/MM/yyyy");

                //lblTransferCost.Text = payment.Expense.Transfer.ToString("#,0.#");
                //lblTransferOld.Text = payment.Transfer.ToString("#,0.#");
                //txtTransferNew.Text = payment.Transfer.ToString("0.#");

                //lblTicketCost.Text = payment.Expense.Ticket.ToString("#,0.#");
                //lblTicketOld.Text = payment.Ticket.ToString("#,0.#");
                //txtTicketNew.Text = payment.Ticket.ToString("0.#");

                //lblGuideCost.Text = payment.Expense.Guide.ToString("#,0.#");
                //lblGuideOld.Text = payment.Guide.ToString("#,0.#");
                //txtGuideNew.Text = payment.Guide.ToString("0.#");

                //lblMealCost.Text = payment.Expense.Meal.ToString("#,0.#");
                //lblMealOld.Text = payment.Meal.ToString("#,0.#");
                //txtMealNew.Text = payment.Meal.ToString("0.#");

                //lblServiceCost.Text = payment.Expense.Service.ToString("#,0.#");
                //lblServiceOld.Text = payment.Service.ToString("#,0.#");
                //txtServiceNew.Text = payment.Service.ToString("0.#");

                //lblKayaingCost.Text = payment.Expense.Kayaing.ToString("#,0.#");
                //lblKayaingOld.Text = payment.Kayaing.ToString("#,0.#");
                //txtKayaingNew.Text = payment.Kayaing.ToString("0.#");

                //lblCruiseCost.Text = payment.Expense.Cruise.ToString("#,0.#");
                //lblCruiseOld.Text = payment.Cruise.ToString("#,0.#");
                //txtCruiseNew.Text = payment.Cruise.ToString("0.#");
                #endregion

                #region -- set event --
                btnTransferPaid.Attributes.Add("onclick", PaidScript(lblTransferCost, txtTransferNew));
                btnTicketPaid.Attributes.Add("onclick",PaidScript(lblTicketCost, txtTicketNew));
                btnGuidePaid.Attributes.Add("onclick",PaidScript(lblGuideCost, txtGuideNew));
                btnMealPaid.Attributes.Add("onclick",PaidScript(lblMealCost, txtMealNew));
                btnServicePaid.Attributes.Add("onclick",PaidScript(lblServiceCost,txtServiceNew));
                btnKayaingPaid.Attributes.Add("onclick", PaidScript(lblKayaingCost, txtKayaingNew));
                btnCruisePaid.Attributes.Add("onclick",PaidScript(lblCruiseCost, txtCruiseNew));
                btnAllPaid.Attributes.Add("onclick",
                                          btnTransferPaid.Attributes["onclick"] + btnTicketPaid.Attributes["onclick"] +
                                          btnGuidePaid.Attributes["onclick"] + btnMealPaid.Attributes["onclick"] +
                                          btnServicePaid.Attributes["onclick"] + btnKayaingPaid.Attributes["onclick"] +
                                          btnCruisePaid.Attributes["onclick"]);
                btnReturn.Attributes.Add("onclick",string.Format("window.location='PayableList.aspx?NodeId={0}&SectionId={1}'", Node.Id, Section.Id));
                #endregion
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int paymentId = Convert.ToInt32(Request.QueryString["Payable"]);
            SailExpensePayment payment = Module.PaymentGetById(paymentId);
            payment.Transfer = Convert.ToDouble(txtTransferNew.Text);
            payment.Ticket = Convert.ToDouble(txtTicketNew.Text);
            payment.Guide = Convert.ToDouble(txtGuideNew.Text);
            payment.Meal = Convert.ToDouble(txtMealNew.Text);
            payment.Service = Convert.ToDouble(txtServiceNew.Text);
            payment.Kayaing = Convert.ToDouble(txtKayaingNew.Text);
            payment.Cruise = Convert.ToDouble(txtCruiseNew.Text);
            Module.SaveOrUpdate(payment);
            PageRedirect(Request.RawUrl);
        }

        protected static string PaidScript(Control costControl, Control newControl)
        {
            string script = string.Format(@"Paid('{0}','{1}');", costControl.ClientID, newControl.ClientID);
            return script;
        }
    }
}