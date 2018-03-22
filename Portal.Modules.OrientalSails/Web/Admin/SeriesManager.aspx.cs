using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Web.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SeriesManager : SailsAdminBasePage
    {
        private SeriesManagerBLL seriesManagerBLL;
        public SeriesManagerBLL SeriesManagerBLL
        {
            get
            {
                if (seriesManagerBLL == null)
                {
                    seriesManagerBLL = new SeriesManagerBLL();
                }
                return seriesManagerBLL;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlLoadData();
                SeriesLoadData();
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (seriesManagerBLL != null)
            {
                seriesManagerBLL.Dispose();
                seriesManagerBLL = null;
            }
        }

        public void ControlLoadData()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["p"]))
            {
                txtPartner.Text = Request.QueryString["p"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["sc"]))
            {
                txtSeriesCode.Text = Request.QueryString["sc"];
            }
        }

        public void SeriesLoadData()
        {
            int count = 0;
            var listSeries = SeriesManagerBLL.SeriesBookingGetByQueryString(Request.QueryString, pagerSeries.PageSize,
                pagerSeries.CurrentPageIndex, out count);
            rptListSeries.DataSource = listSeries;
            pagerSeries.AllowCustomPaging = true;
            pagerSeries.VirtualItemCount = count;
            rptListSeries.DataBind();
        }

        protected void rptListSeries_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var seriesId = Int32.Parse(e.CommandArgument.ToString());
            var series = SeriesManagerBLL.SeriesGetById(seriesId);
            if (e.CommandName == "cancel")
            {
                foreach (var booking in series.ListBooking)
                {
                    if (booking.StartDate <= DateTime.Now.Date)
                    {
                        continue;
                    }
                    booking.Status = Util.StatusType.Cancelled;
                    SeriesManagerBLL.BookingSaveOrUpdate(booking);
                }
            }
        }

        protected void buttonSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) + QueryStringBuildByCriterion());
        }

        public string QueryStringBuildByCriterion()
        {
            NameValueCollection nvcQueryString = new NameValueCollection();
            nvcQueryString.Add("NodeId", "1");
            nvcQueryString.Add("SectionId", "15");
            if (!string.IsNullOrEmpty(Request.QueryString["p"]))
            {
                txtPartner.Text = Request.QueryString["p"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["sc"]))
            {
                txtSeriesCode.Text = Request.QueryString["sc"];
            }
            var criterions = (from key in nvcQueryString.AllKeys
                              from value in nvcQueryString.GetValues(key)
                              select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            return "?" + string.Join("&", criterions);
        }
    }
}