using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using GemBox.Spreadsheet;
using NHibernate.Criterion;
using Org.BouncyCastle.Utilities.Date;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BookingReportPeriodAll : BookingReportPeriod
    {
        #region -- FIELDS --
        private IList _cruises;

        protected IList AllCruises
        {
            get
            {
                if (_cruises == null)
                {
                    _cruises = Module.CruiseGetAll();
                }
                return _cruises;
            }
        }

        private RoomUtil _util;

        private Cruise _activeCruise;
        protected Cruise ActiveCruise
        {
            get { return _activeCruise; }
        }
        #endregion

        #region -- PAGE EVENTS --
        protected override void Page_Load(object sender, EventArgs e)
        {
            //if (!UserIdentity.HasPermission(AccessLevel.Administrator))
            //{
            //    ShowError("You don't have permission to view this info, please go away");
            //    return;
            //}
            if (AllCruises.Count == 1 && Request.QueryString["cruiseid"] == null)
            {
                Cruise cruise = (Cruise)AllCruises[0];
                PageRedirect(string.Format("BookingReportPeriod.aspx?NodeId={0}&SectionId={1}&cruiseid={2}", Node.Id, Section.Id, cruise.Id));
            }
            _util = new RoomUtil(Module);
            base.Page_Load(sender, e);
        }
        #endregion

        public void FillDataToSheet(ExcelWorksheet sheet, List<Customer> customerList, int day, Booking booking, ref int currentRow)
        {
            for (int i = 0; i < customerList.Count; i++)
            {
                sheet.Cells[currentRow, 0].Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
                sheet.Cells[currentRow, 1].Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
                sheet.Cells[currentRow, 2].Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
                sheet.Cells[currentRow, 3].Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
                sheet.Cells[currentRow, 4].Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
                sheet.Cells[currentRow, 5].Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);
                sheet.Cells[currentRow, 6].Style.Borders.SetBorders(MultipleBorders.Outside, Color.Black, LineStyle.Thin);

                sheet.Cells[currentRow, 0].Value = currentRow;
                var customer = customerList[i];
                if (!String.IsNullOrEmpty(customer.Fullname))
                    sheet.Cells[currentRow, 1].Value = customer.Fullname.ToUpper();
                if (customer.Birthday.HasValue)
                    sheet.Cells[currentRow, 3].Value = customer.Birthday.Value.Year;
                else
                    sheet.Cells[currentRow, 3].Value = "";
                if (customer.IsMale.HasValue)
                {
                    if (customer.IsMale.Value == true)
                        sheet.Cells[currentRow, 2].Value = "M | Male";
                    else
                        sheet.Cells[currentRow, 2].Value = "F | Female";
                }
                if (customer.Nationality != null)
                    if (customer.Nationality.Name.ToLower() != "khong ro")
                        sheet.Cells[currentRow, 4].Value = customer.Nationality.Name;
                    else
                        sheet.Cells[currentRow, 4].Value = customer.Passport;
                sheet.Cells[currentRow, 5].Value = customer.Passport;
                sheet.Cells[currentRow, 6].Value = customer.Booking.Cruise.GetModifiedCruiseName() + " " + customer.Booking.Trip.NumberOfDay + "D";
                currentRow++;
            }
        }

        public void FillDataToSheetVn(ExcelWorksheet sheet, List<Customer> customerList, int day, Booking booking, ref int currentRow)
        {
            for (int i = 0; i < customerList.Count; i++)
            {
                var customer = customerList[i];
                sheet.Cells[currentRow, 0].Value = booking.Cruise.CruiseCode;
                sheet.Cells[currentRow, 1].Value = customer.Fullname;
                bool? isMale = customer.IsMale;
                if (isMale.HasValue)
                {
                    if (isMale.Value)
                        sheet.Cells[currentRow, 2].Value = "M | Nam";
                    else
                        sheet.Cells[currentRow, 2].Value = "F | Nữ";
                }
                DateTime? birthDay = customer.Birthday;
                if (birthDay.HasValue)
                    sheet.Cells[currentRow, 3].Value = birthDay.Value.Year;
                Nationality nationality = customer.Nationality;
                if (nationality != null)
                {
                    sheet.Cells[currentRow, 4].Value = nationality.NaCode;
                }
                else
                {
                    sheet.Cells[currentRow, 4].Value = "290";
                }
                sheet.Cells[currentRow, 5].Value = customer.Passport;
                sheet.Cells[currentRow, 6].Value = customer.NguyenQuan;
                sheet.Cells[currentRow, 7].Value = booking.StartDate.ToString("dd/MM/yyyy");
                sheet.Cells[currentRow, 8].Value = booking.StartDate.AddDays(day).ToString("dd/MM/yyyy");
                sheet.Cells[currentRow, 9].Value = "Oriental Sails";
                currentRow++;
            }
        }

        public void CreateWorkSheet(ExcelFile ex, DateTime dateFrom, DateTime dateTo)
        {
            var daySpan = dateTo.Subtract(dateFrom).Days;
            for (var i = 0; i <= daySpan; i++)
            {
                var day = dateFrom.Add(new TimeSpan(i, 0, 0, 0));
                var worksheet = ex.Worksheets.AddCopy(day.ToString("dd-MMM-yyyy"), ex.Worksheets[0]);

                ICriterion finalCriterion = Expression.Ge("Id", 0);
                finalCriterion = Expression.And(finalCriterion, Expression.Eq("Status", StatusType.Approved));
                finalCriterion = Expression.And(finalCriterion, Expression.Not(Expression.Eq("IsTransferred", true)));
                finalCriterion = Expression.And(finalCriterion, Expression.Eq("Deleted", false));

                var timeSpan = new TimeSpan(0, 23, 59, 59);
                var dateFrom2DayBookingCriterion = Expression.Ge("StartDate", day);
                var dateTo2DayBookingCriterion = Expression.Le("StartDate", day.Date.Add(timeSpan));
                var dateFrom3DayBookingCriterion = Expression.Le("StartDate", day);
                var dateTo3DayBookingCriterion = Expression.Ge("EndDate", day.Date.Add(timeSpan));

                var dateFromTo2DayBookingCriterion = Expression.And(dateFrom2DayBookingCriterion, dateTo2DayBookingCriterion);
                var dateFromTo3DayBookingCriterion = Expression.And(dateFrom3DayBookingCriterion, dateTo3DayBookingCriterion);

                var dateFromToCriterion = Expression.Or(dateFromTo2DayBookingCriterion, dateFromTo3DayBookingCriterion);

                finalCriterion = Expression.And(finalCriterion, dateFromToCriterion);


                var bookingList = Module.GetObject<Booking>(finalCriterion, 0, 0);
                var currentRowSheet = 1;
                if (bookingList.Count > 0)
                {
                    for (int l = 0; l < bookingList.Count; l++)
                    {
                        var customerListVn = new List<Customer>();
                        var customerList = new List<Customer>();
                        var booking = bookingList[l];
                        var bookingRoomslist = Module.GetObject<BookingRoom>(Expression.Eq("Book", booking), 0, 0);
                        for (int k = 0; k < bookingRoomslist.Count; k++)
                        {
                            var bookingRooms = bookingRoomslist[k];
                            var realCustomersList = bookingRooms.RealCustomers;
                            for (int j = 0; j < realCustomersList.Count; j++)
                            {
                                var customer = realCustomersList[j] as Customer;
                                if (customer != null)
                                {
                                    customerList.Add(customer);
                                }
                                else
                                {
                                    throw new Exception("customer = null");
                                }
                            }
                        }
                        FillDataToSheet(worksheet, customerList, 1, booking, ref currentRowSheet);
                    }
                }
            }
            ex.Worksheets[0].Delete();
        }

        public DateTime GetMinDateInMonthAndYear(int month, int year)
        {
            return new DateTime(year, month, 1);
        }

        public DateTime GetMaxDateInMonthAndYear(int month, int year)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }

        protected override void rptBookingList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Repeater rptCruisesRow = e.Item.FindControl("rptCruisesRow") as Repeater;
                if (rptCruisesRow != null)
                {
                    rptCruisesRow.DataSource = AllCruises;
                    rptCruisesRow.DataBind();
                }

                Repeater rptCruiseRoom = e.Item.FindControl("rptCruiseRoom") as Repeater;
                if (rptCruiseRoom != null)
                {
                    rptCruiseRoom.DataSource = AllCruises;
                    rptCruiseRoom.DataBind();
                }
            }

            #region -- Item --
            if (e.Item.DataItem is DateTime)
            {
                Literal litTr = e.Item.FindControl("litTr") as Literal;
                if (litTr != null)
                {
                    if (e.Item.ItemType == ListItemType.Item)
                    {
                        litTr.Text = "<tr>";
                    }
                    else
                    {
                        litTr.Text = @"<tr style=""background-color:#E9E9E9"">";
                    }
                }

                DateTime date = (DateTime)e.Item.DataItem;
                Label labelDate = (Label)e.Item.FindControl("labelDate");
                if (labelDate != null)
                {
                    labelDate.Text = date.ToString("dd/MM/yyyy");
                }

                HyperLink hplDate = (HyperLink)e.Item.FindControl("hplDate");
                if (hplDate != null)
                {
                    hplDate.Text = date.ToString("dd/MM/yyyy");
                    hplDate.NavigateUrl = string.Format("BookingReport.aspx?NodeId={0}&SectionId={1}&Date={2}", Node.Id,
                                                        Section.Id, date.ToOADate());
                }

                #region -- Counting --
                int count;
                // Điều kiện bắt buộc
                ICriterion criterion = Expression.And(Expression.Eq("Deleted", false),
                                                      Expression.Eq("Status", StatusType.Approved));
                // Không bao gồm booking transfer
                criterion = Expression.And(criterion, Expression.Not(Expression.Eq("IsTransferred", true)));
                criterion = Module.AddDateExpression(criterion, date);
                _util.Bookings = Module.GetBookingListInPeriod(false, StatusType.Approved, false, date);

                Repeater rptCruiseRoom = e.Item.FindControl("rptCruiseRoom") as Repeater;
                if (rptCruiseRoom != null)
                {
                    rptCruiseRoom.DataSource = AllCruises;
                    rptCruiseRoom.DataBind();
                }
                #endregion
            }
            #endregion
        }

        protected void rptCruisesRow_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Cruise)
            {
                Cruise cruise = (Cruise)e.Item.DataItem;
                HtmlTableCell thCruise = e.Item.FindControl("thCruise") as HtmlTableCell;
                if (thCruise != null)
                {
                    thCruise.ColSpan = _util.Rooms(cruise).Count * 2;
                    thCruise.InnerText = cruise.Name;
                }

                Repeater rptRooms = e.Item.FindControl("rptRooms") as Repeater;
                if (rptRooms != null)
                {
                    if (_util.Bookings != null)// Nếu có danh sách booking đã được gán ở trong ItemDataBound của dòng ngày
                    {
                        _activeCruise = cruise; // gán tàu hiện tại để room item databound ở dưới có thể đếm số phòng
                    }
                    rptRooms.DataSource = _util.Rooms(cruise);
                    rptRooms.DataBind();
                }
            }
        }

        protected override void rptRooms_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Room)
            {
                Room room = (Room)e.Item.DataItem;
                string key = string.Format("{0}#{1}", room.RoomClass.Id, room.RoomType.Id);
                Literal litRoomName = e.Item.FindControl("litRoomName") as Literal;
                if (litRoomName != null)
                {
                    litRoomName.Text = room.RoomClass.Name + " " + room.RoomType.Name;
                }

                if (_util.Bookings != null)
                {
                    currentRoomMap = _util.GetRoomCount(_activeCruise);
                    Literal litTotal = e.Item.FindControl("litTotal") as Literal;
                    if (litTotal != null)
                    {
                        if (currentRoomMap != null && currentRoomMap.ContainsKey(key))
                        {
                            if (room.RoomType.IsShared)
                            {
                                litTotal.Text = (currentRoomMap[key] / room.RoomType.Capacity).ToString();
                            }
                            else
                            {
                                litTotal.Text = (currentRoomMap[key]).ToString();
                            }
                        }
                    }

                    Literal litAvail = e.Item.FindControl("litAvail") as Literal;
                    if (litAvail != null)
                    {
                        int avail = _util.RoomCountMap[ActiveCruise.Id][key];
                        // Nếu có người đặt phòng thì số avail sẽ phải nhỏ hơn
                        if (currentRoomMap != null && currentRoomMap.ContainsKey(key))
                        {
                            if (room.RoomType.IsShared)
                            {
                                avail = (_util.RoomCountMap[ActiveCruise.Id][key] - currentRoomMap[key] / room.RoomType.Capacity);
                            }
                            else
                            {
                                avail = (_util.RoomCountMap[ActiveCruise.Id][key] - currentRoomMap[key]);
                            }
                        }
                        if (avail < 0)
                        {
                            ShowError(Resources.errorBookingPeriod);
                            HtmlTableCell tdAvail = e.Item.FindControl("tdAvail") as HtmlTableCell;
                            if (tdAvail != null)
                            {
                                tdAvail.Attributes.CssStyle.Add("background-color", SailsModule.IMPORTANT);
                            }
                        }
                        else if (avail == 0)
                        {
                            HtmlTableCell tdAvail = e.Item.FindControl("tdAvail") as HtmlTableCell;
                            if (tdAvail != null)
                            {
                                tdAvail.Attributes.CssStyle.Add("background-color", SailsModule.WARNING);
                            }
                        }
                        litAvail.Text = avail.ToString();
                    }
                }
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            DateTime? dateFrom;
            DateTime? dateTo;

            try
            {
                dateFrom = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                dateFrom = GetMinDateInMonthAndYear(DateTime.Today.Month, DateTime.Today.Year);
            }

            try
            {
                dateTo = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                dateTo = GetMaxDateInMonthAndYear(DateTime.Today.Month, DateTime.Today.Year);
            }



            var excelFile = new ExcelFile();
            excelFile.LoadXls(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/ClientDetailsPeriod.xls"));
            CreateWorkSheet(excelFile, dateFrom.Value, dateTo.Value);


            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";

            Response.AppendHeader("content-disposition", "attachment; filename=" + string.Format("ClientsDetail.xls"));

            var m = new MemoryStream();

            excelFile.SaveXls(m);

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();

            m.Close();
            Response.End();
        }

    }
}
