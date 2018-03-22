using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Words;
using CMS.Web.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Controls
{
    public partial class CruisePriceTableCharter : UserControl
    {
        private SailsModule _module;
        public SailsModule Module
        {
            get
            {
                if (_module == null)
                {
                    if (Page is SailsAdminBasePage)
                    {
                        _module = ((SailsAdminBasePage)Page).Module;
                    }
                }
                return _module;
            }
        }
        public SailsPriceTable SailsPriceTable;
        public SailsPriceTable SailsPriceTableTemplate;
        public Cruise ActiveCruise;
        public IList<Charter> CharterList;

        public SailsTrip Trip;
        public TripOption Option;

        private bool _hasRoom = false;
        private bool _isValid;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTrip.Text = Trip.TripCode;
            litOption.Text = Option.ToString();
            rptRoomRange.DataSource = CharterList;
            rptRoomRange.DataBind();
        }

        public void Save()
        {
            foreach (RepeaterItem priceItem in rptRoomRange.Items)
            {
                var textBoxPrice = priceItem.FindControl("textBoxPrice") as TextBox;
                var textBoxPriceVND = priceItem.FindControl("textBoxPriceVND") as TextBox;
                var hidCharterId = priceItem.FindControl("hidCharterId") as HiddenField;

                if (textBoxPrice != null && textBoxPrice.Enabled &&
                    textBoxPrice.Text != string.Empty && textBoxPriceVND != null && textBoxPriceVND.Enabled &&
                    textBoxPriceVND.Text != string.Empty)
                {
                    double price;
                    double priceVND;
               
                    if (!double.TryParse(textBoxPrice.Text, out price))
                    {
                        _isValid = false;
                        break;
                    }

                    if (!double.TryParse(textBoxPriceVND.Text, out priceVND))
                    {
                        _isValid = false;
                        break;
                    }

                    if (SailsPriceTableTemplate == null)
                    {
                        if (hidCharterId != null)
                        {
                            var hidCharterIdIntType = Convert.ToInt32(hidCharterId.Value);
                            var charter = Module.GetObject<Charter>(hidCharterIdIntType);
                            charter.PriceUSD = Convert.ToDouble(Request.Params[textBoxPrice.UniqueID]);
                            charter.PriceVND = Convert.ToDouble(Request.Params[textBoxPriceVND.UniqueID]);
                            charter.SailsPriceTable = SailsPriceTable;
                            Module.SaveOrUpdate(charter);
                        }
                        else
                        {
                            throw new Exception("hidCharterId = null");
                        }
                    }
                    else
                    {
                        var charter = new Charter();
                        charter.PriceUSD = Convert.ToDouble(Request.Params[textBoxPrice.UniqueID]);
                        charter.PriceVND = Convert.ToDouble(Request.Params[textBoxPriceVND.UniqueID]);
                        charter.Trip = Trip;
                        charter.TripOption = Option;
                        if (hidCharterId != null)
                        {
                            var hidCharterIdIntType = Convert.ToInt32(hidCharterId.Value);
                            var charterTemplate = Module.GetObject<Charter>(hidCharterIdIntType);
                            charter.RoomFrom = charterTemplate.RoomFrom;
                            charter.RoomTo = charterTemplate.RoomTo;
                        }
                        charter.Cruise = ActiveCruise;
                        charter.SailsPriceTable = SailsPriceTable;
                        Module.SaveOrUpdate(charter);
                    }
                }
            }
        }

        protected void rptRoomRange_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}