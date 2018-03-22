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
    public partial class HaiPhongContracts : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var criterion = Expression.Ge("HaiPhongContractId", 1);
            rptHaiPhongContract.DataSource = Module.GetObject<HaiPhongContract>(criterion, 0, 0);
            rptHaiPhongContract.DataBind();
        }

        protected void rptHaiPhongContract_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rptCruise = e.Item.FindControl("rptCruise") as Repeater;

            var criterion = Expression.Eq("Deleted", false);
            rptCruise.DataSource = Module.GetObject<Cruise>(criterion, 0, 0);
            rptCruise.DataBind();
        }

        protected void rptCruise_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cruise = (Cruise)e.Item.DataItem;
            var contract = ((RepeaterItem)e.Item.Parent.NamingContainer).DataItem as HaiPhongContract;
            var ltrExpenseLink = e.Item.FindControl("ltrExpenseLink") as Literal;
            var ltrExenseLinkHtml = "<a href = '"+ "/Modules/Sails/Admin/HaiPhongExpenses.aspx?NodeId=1&SectionId=15&Contract="+ contract.HaiPhongContractId +"&Cruise="+ cruise.Id +"' style='color:blue' > " +
                cruise.Name + "</a>";
            ltrExpenseLink.Text = ltrExenseLinkHtml;
        }
    }
}