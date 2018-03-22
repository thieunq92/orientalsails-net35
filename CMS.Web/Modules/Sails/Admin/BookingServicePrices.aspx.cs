using System;
using System.Collections;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BookingServicePrices : SailsAdminBasePage
    {
        private Booking _booking;
        private IList _prices;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["bookingid"]!=null)
            {
                _booking = Module.BookingGetById(Convert.ToInt32(Request.QueryString["bookingid"]));
                _prices = Module.ServicePriceGetByBooking(_booking);
            }
            else
            {
                throw new Exception("Bad request");
            }
            if (!IsPostBack)
            {
                rptServices.DataSource = Module.ExtraOptionGetAll();
                rptServices.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptServices.Items)
            {
                HiddenField hiddenId = (HiddenField) item.FindControl("hiddenId");
                HiddenField hiddenPriceId = (HiddenField)item.FindControl("hiddenPriceId");
                TextBox txtPrice = (TextBox)item.FindControl("txtPrice");
                int priceId = Convert.ToInt32(hiddenPriceId.Value);
                int serviceId = Convert.ToInt32(hiddenId.Value);
                BookingServicePrice price;
                if (priceId > 0)
                {
                    price = Module.ServicePriceGetById(priceId);
                }
                else
                {
                    price = new BookingServicePrice();
                }

                ExtraOption service = Module.ExtraOptionGetById(serviceId);
                price.Booking = _booking;
                price.ExtraOption = service;
                price.UnitPrice = Convert.ToDouble(txtPrice.Text);

                Module.SaveOrUpdate(price);
            }

            ClientScript.RegisterStartupScript(typeof(BookingServicePrices), "close", "window.close();", true);
        }

        protected void rptServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {            
            if (e.Item.DataItem is ExtraOption)
            {
                TextBox txtPrice = (TextBox)e.Item.FindControl("txtPrice");
                ExtraOption service = (ExtraOption) e.Item.DataItem;
                txtPrice.Text = service.Price.ToString();
                foreach (BookingServicePrice price in _prices)
                {
                    if (price.ExtraOption == service)
                    {
                        HiddenField hiddenPriceId = (HiddenField) e.Item.FindControl("hiddenPriceId");
                        hiddenPriceId.Value = price.Id.ToString();
                        
                        txtPrice.Text = price.UnitPrice.ToString();
                        break;
                    }
                }
            }
        }
    }
}
