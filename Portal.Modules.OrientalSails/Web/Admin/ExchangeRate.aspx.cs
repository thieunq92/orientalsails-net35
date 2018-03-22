using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class ExchangeRate : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Resources.titleExchangeRate;
            rptExchangedRate.DataSource = Module.ExchangedGetAll();
            rptExchangedRate.DataBind();
        }

        protected void rptExchangedRate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is USDRate)
            {
                USDRate rate = (USDRate) e.Item.DataItem;
                Literal litValidFrom = (Literal) e.Item.FindControl("litValidFrom");
                if (litValidFrom != null)
                {
                    litValidFrom.Text = rate.ValidFrom.ToString("dd/MM/yyyy");
                }

                Literal litExchangeRate = (Literal) e.Item.FindControl("litExchangeRate");
                if (litExchangeRate!=null)
                {
                    litExchangeRate.Text = rate.Rate.ToString("0");
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            USDRate rate = new USDRate();
            rate.ValidFrom = DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            rate.Rate = Convert.ToDouble(txtExchangeRate.Text);
            Module.SaveOrUpdate(rate);
            PageRedirect(Request.RawUrl);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button) sender;
            USDRate rate = Module.ExchangedGetById(Convert.ToInt32(btnDelete.CommandArgument));
            Module.Delete(rate);
            PageRedirect(Request.RawUrl);
        }
    }
}
