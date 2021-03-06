using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.ReportEngine;
using Portal.Modules.OrientalSails.Web.Controls;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BookingHistories : SailsAdminBasePage
    {
        #region -- PRIVATE MEMBERS --

        private readonly ILog _logger = LogManager.GetLogger(typeof (BookingHistories));

        private BookingHistory _prev;
        #endregion

        #region -- Page Event --

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var booking = Module.BookingGetById(Convert.ToInt32(Request.QueryString["bookingid"]));
                var histories = Module.BookingGetHistory(booking);

                _prev = null;
                rptAgencies.DataSource = histories;
                rptAgencies.DataBind();

                _prev = null;
                rptDates.DataSource = histories;
                rptDates.DataBind();

                _prev = null;
                rptTotals.DataSource = histories;
                rptTotals.DataBind();

                _prev = null;
                rptStatus.DataSource = histories;
                rptStatus.DataBind();

                _prev = null;
                rptTrips.DataSource = histories;
                rptTrips.DataBind();

                _prev = null;
                rptCabins.DataSource = histories;
                rptCabins.DataBind();

                _prev = null;
                rptCustomers.DataSource = histories;
                rptCustomers.DataBind();

            }
            catch (Exception ex)
            {
                _logger.Error("Error when Page_Load in BookingView", ex);
                ShowError(ex.Message);
            }
        }

        #endregion

        protected void rptHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void rptDates_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory) e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.StartDate == history.StartDate)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }
                
                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.StartDate.ToString("dd/MM/yyyy"));
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.StartDate.ToString("dd/MM/yyyy"));
                }
                _prev = history;
            }
        }

        protected void rptStatus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.Status == history.Status)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.Status.ToString());
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.Status.ToString());
                }
                _prev = history;
            }
        }

        protected void rptTrips_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory) e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.Trip.Id == history.Trip.Id)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.Trip.Name);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.Trip.Name);
                }
                _prev = history;
            }
        }

        protected void rptAgencies_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.Agency.Id == history.Agency.Id)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.Agency.Name);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.Agency.Name);
                }
                _prev = history;
            }
        }

        protected void rptTotals_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.Total == history.Total && _prev.TotalCurrency == history.TotalCurrency)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.Total + history.TotalCurrency);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.Total + _prev.TotalCurrency);
                }
                _prev = history;
            }
        }

        protected void rptCabins_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.CabinNumber == history.CabinNumber)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.CabinNumber);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.CabinNumber);
                }
                _prev = history;
            }
        }

        protected void rptCustomers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is BookingHistory)
            {
                var history = (BookingHistory)e.Item.DataItem;

                if (_prev != null)
                {
                    if (_prev.CustomerNumber == history.CustomerNumber)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                }

                ValueBinder.BindLiteral(e.Item, "litTime", history.Date.ToString("dd-MMM-yyyy HH:mm"));
                ValueBinder.BindLiteral(e.Item, "litUser", history.User.FullName);
                ValueBinder.BindLiteral(e.Item, "litTo", history.CustomerNumber);
                if (_prev != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litFrom", _prev.CustomerNumber);
                }
                _prev = history;
            }
        }       
    }
}