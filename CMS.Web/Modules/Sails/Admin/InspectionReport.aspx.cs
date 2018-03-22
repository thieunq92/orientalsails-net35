using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class InspectionReport : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindBackDateFromToControl();
            BindBackDateToToControl();
            BindRptBookingList();

        }

        public DateTime? GetDateFrom()
        {
            try
            {
                return DateTime.ParseExact(Request.Form[txtFrom.UniqueID], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public DateTime? GetDateTo()
        {
            try
            {
                return DateTime.ParseExact(Request.Form[txtTo.UniqueID], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public DateTime? GetDateFromOnQueryString()
        {
            try
            {
                var dateFrom = Request.QueryString["datefrom"];
                if (!String.IsNullOrEmpty(dateFrom))
                    return DateTime.ParseExact(dateFrom, "ddMMyyyy", CultureInfo.InvariantCulture);
                return GetFirstDateOfMonth();
            }
            catch
            {
                return GetFirstDateOfMonth();
            }
        }

        public DateTime? GetDateToOnQueryString()
        {
            try
            {
                var dateTo = Request.QueryString["dateto"];
                if (!String.IsNullOrEmpty(dateTo))
                    return DateTime.ParseExact(dateTo, "ddMMyyyy", CultureInfo.InvariantCulture);
                return GetLastDateOfMonth();
            }
            catch
            {
                return GetLastDateOfMonth();
            }
        }

        public DateTime GetFirstDateOfMonth()
        {
            var today = DateTime.Now;
            var todayMonth = today.Month;
            var todayYear = today.Year;
            const int firstDayOfMonth = 1;
            var firstDateOfMonth = new DateTime(todayYear, todayMonth, firstDayOfMonth);
            return firstDateOfMonth;
        }

        public DateTime GetLastDateOfMonth()
        {
            var today = DateTime.Now;
            var todayMonth = today.Month;
            var todayYear = today.Year;
            var lastDayOfMonth = DateTime.DaysInMonth(todayYear, todayMonth);
            var lastDateOfMonth = new DateTime(todayYear, todayMonth, lastDayOfMonth);
            return lastDateOfMonth;
        }

        public void BindBackDateFromToControl()
        {
            var dateFrom = GetDateFromOnQueryString();
            if (dateFrom != null)
                txtFrom.Text = dateFrom.Value.ToString("dd/MM/yyyy");
            else
            {
                txtFrom.Text = GetFirstDateOfMonth().ToString("dd/MM/yyyy");
            }

        }

        public void BindBackDateToToControl()
        {
            var dateTo = GetDateToOnQueryString();
            if (dateTo != null)
                txtTo.Text = dateTo.Value.ToString("dd/MM/yyyy");
            else
            {
                txtTo.Text = GetLastDateOfMonth().ToString("dd/MM/yyyy");
            }
        }

        public void BindRptBookingList()
        {
            var dateFrom = GetDateFromOnQueryString();
            var dateTo = GetDateToOnQueryString();
            var finalCriterion = Expression.Ge("Id", 0) as ICriterion;
            finalCriterion = Expression.And(finalCriterion, Expression.Eq("Inspection", true));
            if (dateFrom != null)
            {
                var ts = new TimeSpan(0, 0, 0);
                ICriterion dateFromCriterion = Expression.Ge("StartDate", dateFrom.Value.Date + ts);
                finalCriterion = Expression.And(finalCriterion, dateFromCriterion);
            }
            if (dateTo != null)
            {
                var ts = new TimeSpan(23, 59, 59);
                ICriterion dateToCriterion = Expression.Le("StartDate", dateTo.Value.Date + ts);
                finalCriterion = Expression.And(finalCriterion, dateToCriterion);
            }
            pagerBookings.AllowCustomPaging = true;
            rptBookingList.DataSource = Module.GetObject<Booking>(finalCriterion, pagerBookings.PageSize,
              pagerBookings.CurrentPageIndex);
            pagerBookings.VirtualItemCount = Module.CountObjet<Booking>(finalCriterion);
            rptBookingList.DataBind();
        }

        public void Redirect(string control)
        {
            var dateFrom = GetDateFrom();
            var dateTo = GetDateTo();
            var queryString = "";
            var nvc = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nvc.Remove("datefrom");
            nvc.Remove("dateto");
            var rawUrl = Request.Url.AbsolutePath + "?" + nvc.ToString();
            if (dateFrom != null)
                queryString += "&datefrom=" + dateFrom.Value.ToString("ddMMyyyy");

            if (dateTo != null)
                queryString += "&dateto=" + dateTo.Value.ToString("ddMMyyyy");

            Response.Redirect(rawUrl + queryString);
        }

        protected void rptBookingList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Control plhAccounting = e.Item.FindControl("plhAccounting");
            if (plhAccounting != null)
            {
                plhAccounting.Visible = CheckAccountStatus;
            }

            Booking item = e.Item.DataItem as Booking;
            if (item != null)
            {
                #region Trip

                using (HyperLink hyperLink_Trip = e.Item.FindControl("hyperLink_Trip") as HyperLink)
                {
                    if (hyperLink_Trip != null)
                    {
                        if (item.Trip != null)
                        {
                            hyperLink_Trip.Text = item.Trip.Name;
                            hyperLink_Trip.NavigateUrl =
                                string.Format("BookingView.aspx?NodeId={0}&SectionId={1}&bi={2}", Node.Id,
                                              Section.Id,
                                              item.Id);
                        }
                    }
                }

                #endregion

                HyperLink hplCruise = e.Item.FindControl("hplCruise") as HyperLink;
                if (hplCruise != null)
                {
                    if (item.Cruise != null)
                    {
                        hplCruise.Text = item.Cruise.Name;
                    }
                    else
                    {
                        ShowError("Quá trình nâng cấp lên nhiều tàu chưa hoàn thành!");
                    }
                    //hplCruise.NavigateUrl 
                }
                Literal litCode = e.Item.FindControl("litCode") as Literal;
                if (litCode != null)
                {
                    if (item.Id > 0 && UseCustomBookingId)
                    {
                        litCode.Text = string.Format(BookingFormat, item.Id);
                    }
                    else
                    {
                        litCode.Text = string.Format(BookingFormat, item.Id);
                    }
                }

                HyperLink hplCode = e.Item.FindControl("hplCode") as HyperLink;
                if (hplCode != null)
                {
                    if (item.Id > 0 && UseCustomBookingId)
                    {
                        hplCode.Text = string.Format(BookingFormat, item.Id);
                    }
                    else
                    {
                        hplCode.Text = string.Format(BookingFormat, item.Id);
                    }
                    hplCode.NavigateUrl = string.Format("BookingView.aspx?NodeId={0}&SectionId={1}&bi={2}",
                                                        Node.Id, Section.Id, item.Id);
                }

                Literal litPax = e.Item.FindControl("litPax") as Literal;
                if (litPax != null)
                {
                    litPax.Text = item.Pax.ToString();
                }

                if (ShowCustomerName)
                {
                    Label labelName = e.Item.FindControl("labelName") as Label;
                    if (labelName != null)
                    {
                        labelName.Text = item.CustomerName;
                    }
                }
                else
                {
                    e.Item.FindControl("columnCustomerName").Visible = false;
                }

                Label labelConfirmBy = e.Item.FindControl("labelConfirmBy") as Label;
                if (labelConfirmBy != null)
                {
                    if (item.ModifiedBy != null)
                    {
                        labelConfirmBy.Text = item.ModifiedBy.FullName;
                    }
                    else
                    {
                        labelConfirmBy.Text = "";
                    }
                }

                #region Partner

                HyperLink hplAgency = e.Item.FindControl("hplAgency") as HyperLink;
                if (hplAgency != null)
                {
                    if (item.Agency != null)
                    {
                        hplAgency.Text = item.Agency.Name;
                        hplAgency.NavigateUrl = string.Format("AgencyEdit.aspx?NodeId={0}&SectionId={1}&AgencyId={2}",
                                                              Node.Id, Section.Id, item.Agency.Id);
                    }
                    else
                    {
                        hplAgency.Text = SailsModule.NOAGENCY;
                    }
                }

                Literal litAgencyCode = e.Item.FindControl("litAgencyCode") as Literal;
                if (litAgencyCode != null)
                {
                    if (item.Agency != null)
                    {
                        litAgencyCode.Text = item.AgencyCode;
                    }
                }

                #endregion

                #region StartDate

                using (Label label_startDate = e.Item.FindControl("label_startDate") as Label)
                {
                    if (label_startDate != null)
                    {
                        label_startDate.Text = item.StartDate.ToString("dd/MM/yyyy");
                    }
                }
                #endregion

                #region Status

                using (Label label_Status = e.Item.FindControl("label_Status") as Label)
                {
                    if (label_Status != null)
                    {
                        label_Status.Text = Enum.GetName(typeof(StatusType), item.Status);
                    }
                }

                HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("trItem");
                if (item.Status == StatusType.Pending)
                {
                    row.Attributes.Add("style", "background-color: " + SailsModule.WARNING);
                }
                //if (item.Status == StatusType.Rejected)
                //{
                //    row.Attributes.Add("style", "background-color: " + SailsModule.IMPORTANT);
                //}
                if (item.Status == StatusType.Approved)
                {
                    row.Attributes.Add("style", "background-color: " + SailsModule.GOOD);
                }

                if (!item.IsCharter)
                {
                    Locked locked = Module.LockedCheckByDate(item.Cruise, item.StartDate, item.EndDate);
                    // Nếu trong khoảng thời gian tồn tại lock, và lock đó là lock charter, tức là ngày hiện tại bị đè bởi charter

                    if (locked != null && Module.LockedCheckCharter(locked))
                    {
                        // Chắc chắn đã nằm trên ngày charter, giờ xác định xem đã chuyển hay chưa chuyển
                        if (item.IsTransferred)
                        {
                            // Nếu là đã chuyển thì màu xám
                            row.Attributes.Add("style", "background-color: #AAAAAA");
                        }
                        else
                        {
                            // Nếu vẫn chưa chuyển highlight màu hơi xanh
                            row.Attributes.Add("style", "background-color: #9ABAFF");
                        }
                    }
                }
                else
                {
                    // Chỉ chấp nhận là màu charter khi nó đã chấp thuận
                    if (item.Status == StatusType.Approved)
                    {
                        row.Attributes.Add("style", "background-color: #FF00FF");
                    }
                }

                Literal litAccounting = e.Item.FindControl("litAccounting") as Literal;
                if (litAccounting != null)
                {
                    switch (item.AccountingStatus)
                    {
                        case AccountingStatus.New:
                            litAccounting.Text = Resources.textAccountingNew;
                            break;
                        case AccountingStatus.Modified:
                            litAccounting.Text = Resources.textAccountingModified;
                            break;
                        case AccountingStatus.Updated:
                            litAccounting.Text = Resources.textAccountingUpdated;
                            break;
                    }
                }

                Literal litAmend = e.Item.FindControl("litAmend") as Literal;
                if (litAmend != null)
                {
                    litAmend.Visible = item.Amended > 0;
                }
                #endregion

                #region HyperLink View

                using (HyperLink hyperLinkView = e.Item.FindControl("hyperLinkView") as HyperLink)
                {
                    if (hyperLinkView != null)
                    {
                        hyperLinkView.NavigateUrl =
                            string.Format("BookingView.aspx?NodeId={0}&SectionId={1}&bi={2}", Node.Id, Section.Id,
                                          item.Id);
                    }
                }

                #endregion

                Literal litNote = e.Item.FindControl("litNote") as Literal;
                Image imgNote = e.Item.FindControl("imgNote") as Image;
                if (litNote != null && imgNote != null)
                {
                    if (!string.IsNullOrEmpty(item.Note))
                    {
                        litNote.Text = item.Note;
                    }
                    else
                    {
                        imgNote.Visible = false;
                    }
                }
            }
            else
            {
                e.Item.FindControl("columnCustomerName").Visible = ShowCustomerName;
            }
        }
    }
}