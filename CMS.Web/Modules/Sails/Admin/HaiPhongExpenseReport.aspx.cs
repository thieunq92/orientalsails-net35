using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Web.UI;
using System.Globalization;
using Portal.Modules.OrientalSails.Domain;
using System.Web.UI.HtmlControls;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Web.Util;
using GemBox.Spreadsheet;
using System.IO;

namespace Portal.Modules.OrientalSails.Web.Admin
{

    public partial class HaiPhongExpenseReport : SailsAdminBasePage
    {
        private Cruise _cruise;
        private int baby;
        private int child6to8age;
        private int child9to12age;
        private int adult;
        private double totalTotalCustomer;
        private double totalTotal2dayCustomer;
        private double totalTotal2dayServiceExpense;
        private double totalTotal3dayCustomer;
        private double totalTotal3dayServiceExpense;
        private double totalTotalCruiseExpenseCustomer;
        private double totalTotalCruiseExpense;
        private double totalTotalExpense;
        private double totalTotalExpenseVND;
        private double _2dayServiceExpenseUnitPrice;
        private double _2dayServiceExpenseUnitPriceI;
        private double _3dayServiceExpenseUnitPrice;
        private double _3dayServiceExpenseUnitPriceI;
        private double customerCruisePrice;
        private double customerCruisePriceI;
        private double cruisePrice;
        private double cruisePriceI;
        private string moneyFormat;
        private double exchangeRate;

        protected Cruise ActiveCruise
        {
            get
            {
                if (_cruise == null && Request.QueryString["cruiseid"] != null)
                {
                    _cruise = Module.CruiseGetById(Convert.ToInt32(Request.QueryString["cruiseid"]));
                }


                if (Request.QueryString["cruiseid"] == null)
                {
                    if (Module.CruiseGetAll().Count > 0)
                    {
                        _cruise = Module.CruiseGetAll()[0] as Cruise;
                    }
                }

                return _cruise;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["from"] != null)
                {
                    DateTime from = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["from"]));
                    txtFrom.Text = from.ToString("dd/MM/yyyy");
                }
                if (Request.QueryString["to"] != null)
                {
                    DateTime to = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["to"]));
                    txtTo.Text = to.ToString("dd/MM/yyyy");
                }

                if (string.IsNullOrEmpty(txtFrom.Text) || string.IsNullOrEmpty(txtTo.Text))
                {
                    DateTime from = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
                    DateTime to = from.AddMonths(1).AddDays(-1);
                    txtFrom.Text = from.ToString("dd/MM/yyyy");
                    txtTo.Text = to.ToString("dd/MM/yyyy");
                }
            }

            rptCruises.DataSource = Module.CruiseGetAll(); ;
            rptCruises.DataBind();

            DateTime fromDate = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var listDates = new List<DateTime>();
            for (var dt = fromDate; dt <= toDate; dt = dt.AddDays(1))
            {
                listDates.Add(dt);
            }
            rptStartDate.DataSource = listDates;
            rptStartDate.DataBind();
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            DateTime from = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            PageRedirect(string.Format("HaiPhongExpenseReport.aspx?NodeId={0}&SectionId={1}&from={2}&to={3}", Node.Id, Section.Id, from.ToOADate(), to.ToOADate()));
        }

        protected void rptCruises_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Cruise cruise = (Cruise)e.Item.DataItem;

            HtmlGenericControl liMenu = e.Item.FindControl("liMenu") as HtmlGenericControl;
            if (liMenu != null)
            {
                if (cruise.Id == ActiveCruise.Id)
                {
                    liMenu.Attributes.Add("class", "selected");
                }
            }

            HyperLink hplCruises = e.Item.FindControl("hplCruises") as HyperLink;
            if (hplCruises != null)
            {
                DateTime from = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime to = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                hplCruises.Text = cruise.Name;
                hplCruises.NavigateUrl =
                    string.Format("HaiPhongExpenseReport.aspx?NodeId={0}&SectionId={1}&from={2}&to={3}&cruiseid={4}", Node.Id, Section.Id, from.ToOADate(), to.ToOADate(), cruise.Id);
            }
        }

        public IList<Booking> GetBookings(DateTime startDate)
        {
            var statusCriterion = Expression.Eq("Status", StatusType.Approved);
            var cruiseCriterion = Expression.Eq("Cruise.Id", ActiveCruise.Id);
            var startDateCriterion = Expression.Eq("StartDate", startDate);
            var criterion = Expression.And(statusCriterion, cruiseCriterion);
            criterion = Expression.And(criterion, startDateCriterion);
            var bookings = Module.GetObject<Booking>(criterion, 0, 0);
            return bookings;
        }

        public IList<Booking> GetBookings2Day(DateTime startDate)
        {
            var bookings = GetBookings(startDate);
            var listBooking2Day = new List<Booking>();
            foreach (var booking in bookings)
            {
                if (booking.Trip.NumberOfDay == 2)
                {
                    listBooking2Day.Add(booking);
                }
            }
            return listBooking2Day;
        }

        public IList<Booking> GetBookings3Day(DateTime startDate)
        {
            var bookings = GetBookings(startDate);
            var listBooking3Day = new List<Booking>();
            foreach (var booking in bookings)
            {
                if (booking.Trip.NumberOfDay == 3)
                {
                    listBooking3Day.Add(booking);
                }
            }
            return listBooking3Day;
        }

        public IList<Booking> GetBookingsNotInspection(IList<Booking> bookings)
        {
            var bookingsNotInspection = new List<Booking>();
            foreach (var booking in bookings)
            {
                if (!booking.Inspection)
                    bookingsNotInspection.Add(booking);
            }
            return bookingsNotInspection;
        }

        public IList<Booking> GetBookingsInspection(IList<Booking> bookings)
        {
            var bookingsInspection = new List<Booking>();
            foreach (var booking in bookings)
            {
                if (booking.Inspection)
                    bookingsInspection.Add(booking);
            }
            return bookingsInspection;
        }

        public void ClearCustomer()
        {
            baby = 0;
            child6to8age = 0;
            child9to12age = 0;
            adult = 0;
        }

        public void ProcessClassifyCustomerType(IList<Booking> bookings)
        {
            ClearCustomer();
            foreach (var booking in bookings)
            {
                var bookingRooms = booking.BookingRooms;
                foreach (BookingRoom bookingRoom in bookingRooms)
                {
                    var customers = bookingRoom.Customers;
                    foreach (Customer customer in customers)
                    {
                        if (customer.Birthday == null)
                            continue;
                        TimeSpan ts = booking.StartDate - customer.Birthday.Value;
                        int age = DateTime.MinValue.AddDays(ts.Days).Year - 1;
                        if (age < 6 || age < 0)
                        {
                            baby = baby + 1;
                        }

                        if (age >= 6 && age <= 8)
                        {
                            child6to8age = child6to8age + 1;
                        }

                        if (age >= 9 && age <= 12)
                        {
                            child9to12age = child9to12age + 1;
                        }

                        if (age >= 13)
                        {
                            adult = adult + 1;
                        }
                    }
                }
            }
        }

        public int GetBaby()
        {
            return baby;
        }

        public int GetChild6to8age()
        {
            return child6to8age;
        }

        public int GetChild9to12age()
        {
            return child9to12age;
        }

        public int GetAdult()
        {
            return adult;
        }

        public double GetTotalCustomer(IList<Booking> bookings)
        {
            ProcessClassifyCustomerType(bookings);
            var baby = GetBaby();
            var child6to8age = GetChild6to8age();
            var child9to12age = GetChild9to12age();
            var adult = GetAdult();
            return ((adult * 1) + (child6to8age * 0.50) + (child9to12age * 0.75) + (baby * 0));
        }

        public void SetTotalTotalCustomer(double totalCustomer)
        {
            this.totalTotalCustomer = this.totalTotalCustomer + totalCustomer;
        }

        public double GetTotalTotalCustomer()
        {
            return totalTotalCustomer;
        }

        public void SetTotalTotal2dayCustomer(double total2DayCustomer)
        {
            this.totalTotal2dayCustomer = this.totalTotal2dayCustomer + total2DayCustomer;
        }

        public double GetTotalTotal2dayCustomer()
        {
            return totalTotal2dayCustomer;
        }

        public void SetTotalTotal2DayServiceExpense(double total2DayServiceExpense)
        {
            this.totalTotal2dayServiceExpense = this.totalTotal2dayServiceExpense + total2DayServiceExpense;
        }

        public double GetTotalTotal2DayServiceExpense()
        {
            return totalTotal2dayServiceExpense;
        }

        public void SetTotalTotal3DayCustomer(double total3DayCustomer)
        {
            this.totalTotal3dayCustomer = this.totalTotal3dayCustomer + total3DayCustomer;
        }

        public double GetTotalTotal3DayCustomer()
        {
            return totalTotal3dayCustomer;
        }
     
        public void SetTotalTotal3DayServiceExpense(double total3DayServiceExpense)
        {
            this.totalTotal3dayServiceExpense = this.totalTotal3dayServiceExpense + total3DayServiceExpense;
        }

        public double GetTotalTotal3DayServiceExpense()
        {
            return totalTotal3dayServiceExpense;
        }

        public void SetTotalTotalCruiseExpenseCustomer(double totalCruiseExpenseCustomer)
        {
            this.totalTotalCruiseExpenseCustomer = this.totalTotalCruiseExpenseCustomer + totalCruiseExpenseCustomer;
        }

        public double GetTotalTotalCruiseExpenseCustomer()
        {
            return totalTotalCruiseExpenseCustomer;
        }
     
        public void SetTotalTotalCruiseExpense(double totalCruiseExpense)
        {
            this.totalTotalCruiseExpense = this.totalTotalCruiseExpense + totalCruiseExpense;
        }

        public double GetTotalTotalCruiseExpense()
        {
            return totalTotalCruiseExpense;
        }
  
        public void SetTotalTotalExpense(double totalExpense)
        {
            this.totalTotalExpense = this.totalTotalExpense + totalExpense;
        }

        public double GetTotalTotalExpense()
        {
            return totalTotalExpense;
        }
 
        public void SetTotalTotalExpenseVND(double totalExpenseVND)
        {
            this.totalTotalExpenseVND = this.totalTotalExpenseVND + totalExpenseVND;
        }

        public double GetTotalTotalExpenseVND()
        {
            return totalTotalExpenseVND;
        }

        public void Set2DayServiceExpenseUnitPrice(double unitPrice)
        {
            _2dayServiceExpenseUnitPrice = unitPrice;
        }

        public double Get2DayServiceExpenseUnitPrice()
        {
            return _2dayServiceExpenseUnitPrice;
        }
  
        public void Set2DayServiceExpenseUnitPriceI(double unitPrice)
        {
            _2dayServiceExpenseUnitPriceI = unitPrice;
        }

        public double Get2DayServiceExpenseUnitPriceI()
        {
            return _2dayServiceExpenseUnitPriceI;
        }
  
        public void Set3DayServiceExpenseUnitPrice(double unitPrice)
        {
            _3dayServiceExpenseUnitPrice = unitPrice;
        }

        public double Get3DayServiceExpenseUnitPrice()
        {
            return _3dayServiceExpenseUnitPrice;
        }

        public void Set3DayServiceExpenseUnitPriceI(double unitPrice)
        {
            _3dayServiceExpenseUnitPriceI = unitPrice;
        }

        public double Get3DayServiceExpenseUnitPriceI()
        {
            return _3dayServiceExpenseUnitPriceI;
        }
  
        public void SetCustomerCruisePrice(double customerCruisePrice)
        {
            this.customerCruisePrice = customerCruisePrice;
        }

        public double GetCustomerCruisePrice()
        {
            return customerCruisePrice;
        }
     
        public void SetCustomerCruisePriceI(double customerCruisePriceI)
        {
            this.customerCruisePriceI = customerCruisePriceI;
        }

        public double GetCustomerCruisePriceI()
        {
            return customerCruisePriceI;
        }

        public void SetCruisePrice(double cruisePrice)
        {
            this.cruisePrice = cruisePrice;
        }

        public double GetCruisePrice()
        {
            return cruisePrice;
        }
   
        public void SetCruisePriceI(double cruisePriceI)
        {
            this.cruisePriceI = cruisePriceI;
        }

        public double GetCruisePriceI()
        {
            return cruisePriceI;
        }
    
        public void SetMoneyFormat(string moneyFormat)
        {
            this.moneyFormat = moneyFormat;
        }

        public string GetMoneyFormat()
        {
            return moneyFormat;
        }
  
        public void SetExchangeRate(double exchangeRate)
        {
            this.exchangeRate = exchangeRate;
        }

        public double GetExchangeRate()
        {
            return exchangeRate;
        }

        protected void rptStartDate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is DateTime)
            {
                var startDate = (DateTime)e.Item.DataItem;

                var ltrStartDate = e.Item.FindControl("ltrStartDate") as Literal;
                var ltrTotalCustomer = e.Item.FindControl("ltrTotalCustomer") as Literal;
                var ltrTotal2dayCustomer = e.Item.FindControl("ltrTotal2dayCustomer") as Literal;
                var ltrTotal2dayServiceExpense = e.Item.FindControl("ltrTotal2dayServiceExpense") as Literal;
                var ltrTotal3dayCustomer = e.Item.FindControl("ltrTotal3dayCustomer") as Literal;
                var ltrTotal3dayServiceExpense = e.Item.FindControl("ltrTotal3dayServiceExpense") as Literal;
                var ltrTotalCruiseExpenseCustomer = e.Item.FindControl("ltrTotalCruiseExpenseCustomer") as Literal;
                var ltrTotalCruiseExpense = e.Item.FindControl("ltrTotalCruiseExpense") as Literal;
                var ltrTotalExpense = e.Item.FindControl("ltrTotalExpense") as Literal;
                var ltr2dayServiceExpenseUnitPrice = e.Item.FindControl("ltr2dayServiceExpenseUnitPrice") as Literal;
                var ltr3dayServiceExpenseUnitPrice = e.Item.FindControl("ltr3dayServiceExpenseUnitPrice") as Literal;
                var tdExchangeRate = e.Item.FindControl("tdExchangeRate") as HtmlTableCell;
                var ltrExchangeRate = e.Item.FindControl("ltrExchangeRate") as Literal;
                var tdTotalCruiseExpenseCustomer = e.Item.FindControl("tdTotalCruiseExpenseCustomer") as HtmlTableCell;
                var tdTotalExpenseVND = e.Item.FindControl("tdTotalExpenseVND") as HtmlTableCell;
                var ltrTotalExpenseVND = e.Item.FindControl("ltrTotalExpenseVND") as Literal;
                var ltrTotalCustomerI = e.Item.FindControl("ltrTotalCustomerI") as Literal;
                var ltrTotal2dayCustomerI = e.Item.FindControl("ltrTotal2dayCustomerI") as Literal;
                var ltrTotal2dayServiceExpenseI = e.Item.FindControl("ltrTotal2dayServiceExpenseI") as Literal;
                var ltrTotal3dayCustomerI = e.Item.FindControl("ltrTotal3dayCustomerI") as Literal;
                var ltrTotal3dayServiceExpenseI = e.Item.FindControl("ltrTotal3dayServiceExpenseI") as Literal;
                var ltrTotalCruiseExpenseCustomerI = e.Item.FindControl("ltrTotalCruiseExpenseCustomerI") as Literal;
                var ltrTotalCruiseExpenseI = e.Item.FindControl("ltrTotalCruiseExpenseI") as Literal;
                var ltrTotalExpenseI = e.Item.FindControl("ltrTotalExpenseI") as Literal;
                var ltr2dayServiceExpenseUnitPriceI = e.Item.FindControl("ltr2dayServiceExpenseUnitPriceI") as Literal;
                var ltr3dayServiceExpenseUnitPriceI = e.Item.FindControl("ltr3dayServiceExpenseUnitPriceI") as Literal;
                var tdExchangeRateI = e.Item.FindControl("tdExchangeRateI") as HtmlTableCell;
                var ltrExchangeRateI = e.Item.FindControl("ltrExchangeRateI") as Literal;
                var tdTotalCruiseExpenseCustomerI = e.Item.FindControl("tdTotalCruiseExpenseCustomerI") as HtmlTableCell;
                var tdTotalExpenseVNDI = e.Item.FindControl("tdTotalExpenseVNDI") as HtmlTableCell;
                var ltrTotalExpenseVNDI = e.Item.FindControl("ltrTotalExpenseVNDI") as Literal;
                var tdStartDate = e.Item.FindControl("tdStartDate") as HtmlTableCell;
                var trInspection = e.Item.FindControl("trInspection") as HtmlTableRow;
                ltrStartDate.Text = startDate.Day.ToString();

                var totalCustomer = GetTotalCustomer(GetBookingsNotInspection(GetBookings(startDate)));
                var totalCustomerI = GetTotalCustomer(GetBookingsInspection(GetBookings(startDate)));
                ltrTotalCustomer.Text = totalCustomer.ToString("#,0.00");
                SetTotalTotalCustomer(totalCustomer);
                ltrTotalCustomerI.Text = totalCustomerI.ToString("#,0.00");
                SetTotalTotalCustomer(totalCustomerI);

                if (ActiveCruise.Name.ToLower() == "oriental sails" || ActiveCruise.Name.ToLower() == "oriental sails 2")
                {
                    Set2DayServiceExpenseUnitPrice(880000);
                    Set2DayServiceExpenseUnitPriceI(720000);
                    Set3DayServiceExpenseUnitPrice(1380000);
                    Set3DayServiceExpenseUnitPriceI(1148000);
                    if (ActiveCruise.Name.ToLower() == "oriental sails")
                    {
                        SetCustomerCruisePrice(210000);
                        SetCruisePrice(9000000);
                        SetCruisePriceI(0);
                    }

                    if (ActiveCruise.Name.ToLower() == "oriental sails 2")
                    {
                        SetCustomerCruisePrice(120000);
                        SetCruisePrice(6000000);
                        SetCruisePriceI(0);
                    }

                    SetMoneyFormat("#,0");
                }

                if (ActiveCruise.Name.ToLower() == "calypso cruiser")
                {
                    Set2DayServiceExpenseUnitPrice(950000);
                    Set2DayServiceExpenseUnitPriceI(762000);
                    Set3DayServiceExpenseUnitPrice(1480000);
                    Set3DayServiceExpenseUnitPriceI(1280000);
                    SetCustomerCruisePrice(210000);
                    SetCruisePrice(6000000);
                    SetCruisePriceI(0);
                    SetCustomerCruisePriceI(0);
                    SetMoneyFormat("#,0");
                }

                if (ActiveCruise.Name.ToLower() == "starlight")
                {
                    Set2DayServiceExpenseUnitPrice(72);
                    Set2DayServiceExpenseUnitPriceI(39.5);
                    Set3DayServiceExpenseUnitPrice(118);
                    Set3DayServiceExpenseUnitPriceI(62);
                    SetCustomerCruisePrice(0);
                    SetCruisePrice(1200);
                    SetCruisePriceI(0);
                    SetCustomerCruisePriceI(0);
                    SetMoneyFormat("#,0.00");

                    thExchangeRate.Visible = true;
                    SetExchangeRate(21000);
                    tdExchangeRate.Visible = true;
                    tdExchangeRateI.Visible = true;
                    tdTotalCruiseExpenseCustomer.Visible = false;
                    tdTotalCruiseExpenseCustomerI.Visible = false;
                    ltrExchangeRate.Text = GetExchangeRate().ToString("#,0");
                    ltrExchangeRateI.Text = GetExchangeRate().ToString("#,0");
                    tdCruisePrice.ColSpan = 1;
                    thUnitPrice.Visible = false;

                    thTotalExpenseVND.Visible = true;
                    tdTotalExpenseVND.Visible = true;
                    tdTotalExpenseVNDI.Visible = true;
                    tdVND.Visible = true;
                }


                var bookings2Day = GetBookings2Day(startDate);
                var bookings2DayInspection = GetBookingsInspection(bookings2Day);
                var bookings2DayNotInspection = GetBookingsNotInspection(bookings2Day);
                var bookings3Day = GetBookings3Day(startDate);
                var bookings3DayInspection = GetBookingsInspection(bookings3Day);
                var bookings3DayNotInspection = GetBookingsNotInspection(bookings3Day);

                if (bookings2DayInspection.Count != 0 || bookings3DayInspection.Count != 0)
                {
                    trInspection.Visible = true;
                    tdStartDate.RowSpan = 2;
                }
                else
                {
                    trInspection.Visible = false;
                }

                var total2dayCustomer = GetTotalCustomer(bookings2DayNotInspection);
                ltrTotal2dayCustomer.Text = total2dayCustomer.ToString("#,0.00");
                SetTotalTotal2dayCustomer(total2dayCustomer);

                var total2DayCustomerI = GetTotalCustomer(bookings2DayInspection);
                ltrTotal2dayCustomerI.Text = total2DayCustomerI.ToString("#,0.00");

                var _2dayServiceExpenseUnitPrice = Get2DayServiceExpenseUnitPrice();
                ltr2dayServiceExpenseUnitPrice.Text = _2dayServiceExpenseUnitPrice.ToString(GetMoneyFormat());

                var _2dayServiceExpenseUnitPriceI = Get2DayServiceExpenseUnitPriceI();
                ltr2dayServiceExpenseUnitPriceI.Text = _2dayServiceExpenseUnitPriceI.ToString(GetMoneyFormat());

                var total2dayServiceExpense = (total2dayCustomer * _2dayServiceExpenseUnitPrice);
                SetTotalTotal2DayServiceExpense(total2dayServiceExpense);
                ltrTotal2dayServiceExpense.Text = total2dayServiceExpense.ToString(GetMoneyFormat());

                var total2dayServiceExpenseI = (total2DayCustomerI * _2dayServiceExpenseUnitPriceI);
                SetTotalTotal2DayServiceExpense(total2dayServiceExpenseI);
                ltrTotal2dayServiceExpenseI.Text = total2dayServiceExpenseI.ToString(GetMoneyFormat());

                var total3dayCustomer = GetTotalCustomer(bookings3DayNotInspection);
                SetTotalTotal3DayCustomer(total3dayCustomer);
                ltrTotal3dayCustomer.Text = total3dayCustomer.ToString("#,0.00");

                var total3dayCustomerI = GetTotalCustomer(bookings3DayInspection);
                SetTotalTotal3DayCustomer(total3dayCustomerI);
                ltrTotal3dayCustomerI.Text = total3dayCustomerI.ToString("#,0.00");

                var _3dayServiceExpenseUnitPrice = Get3DayServiceExpenseUnitPrice();
                ltr3dayServiceExpenseUnitPrice.Text = _3dayServiceExpenseUnitPrice.ToString(GetMoneyFormat());

                var _3dayServiceExpenseUnitPriceI = Get3DayServiceExpenseUnitPriceI();
                ltr3dayServiceExpenseUnitPriceI.Text = _3dayServiceExpenseUnitPriceI.ToString(GetMoneyFormat());

                var total3dayServiceExpense = (total3dayCustomer * _3dayServiceExpenseUnitPrice);
                SetTotalTotal3DayServiceExpense(total3dayServiceExpense);
                ltrTotal3dayServiceExpense.Text = total3dayServiceExpense.ToString(GetMoneyFormat());

                var total3dayServiceExpenseI = (total3dayCustomerI * _3dayServiceExpenseUnitPriceI);
                SetTotalTotal3DayServiceExpense(total3dayServiceExpenseI);
                ltrTotal3dayServiceExpenseI.Text = total3dayServiceExpenseI.ToString(GetMoneyFormat());

                var totalCruiseExpenseCustomer = total2dayCustomer + total3dayCustomer * 2;
                SetTotalTotalCruiseExpenseCustomer(totalCruiseExpenseCustomer);
                ltrTotalCruiseExpenseCustomer.Text = totalCruiseExpenseCustomer.ToString();

                var totalCruiseExpenseCustomerI = total2DayCustomerI + total3dayCustomerI * 2;
                SetTotalTotalCruiseExpenseCustomer(totalCruiseExpenseCustomerI);
                ltrTotalCruiseExpenseCustomerI.Text = totalCruiseExpenseCustomerI.ToString();

                var customerCruisePrice = GetCustomerCruisePrice();
                var cruisePrice = GetCruisePrice();

                if (totalCustomer == 0.0)
                {
                    cruisePrice = 0.0;
                }

                var totalCruiseExpense = totalCruiseExpenseCustomer * customerCruisePrice + cruisePrice;
                SetTotalTotalCruiseExpense(totalCruiseExpense);
                ltrTotalCruiseExpense.Text = totalCruiseExpense.ToString(GetMoneyFormat());

                var cruisePriceI = GetCruisePriceI();
                var customerCruisePriceI = GetCustomerCruisePriceI();
                var totalCruiseExpenseI = totalCruiseExpenseCustomerI * customerCruisePriceI + cruisePriceI;
                SetTotalTotalCruiseExpense(totalCruiseExpenseI);
                ltrTotalCruiseExpenseI.Text = totalCruiseExpenseI.ToString(GetMoneyFormat());

                var totalExpense = (total2dayServiceExpense + total3dayServiceExpense + totalCruiseExpense);
                SetTotalTotalExpense(totalExpense);
                ltrTotalExpense.Text = totalExpense.ToString(GetMoneyFormat());

                var totalExpenseI = total2dayServiceExpenseI + total3dayServiceExpenseI + totalCruiseExpenseI;
                SetTotalTotalExpense(totalExpense);
                ltrTotalExpenseI.Text = totalExpenseI.ToString(GetMoneyFormat());

                var exchangeRate = GetExchangeRate();
                var totalExpenseVND = (total2dayServiceExpense + total3dayServiceExpense + totalCruiseExpense) * exchangeRate;
                SetTotalTotalExpenseVND(totalExpenseVND);
                ltrTotalExpenseVND.Text = totalExpenseVND.ToString(GetMoneyFormat());

                var totalExpenseVNDI = (total2dayServiceExpenseI + total3dayServiceExpenseI + totalCruiseExpenseI) * exchangeRate;
                SetTotalTotalExpenseVND(totalExpenseVNDI);
                ltrTotalExpenseVNDI.Text = totalExpenseVNDI.ToString(GetMoneyFormat());
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                var thFooterExchangeRate = e.Item.FindControl("thFooterExchangeRate") as HtmlTableCell;
                var thFooterTotal3dayServiceExpense = e.Item.FindControl("thFooterTotal3dayServiceExpense") as HtmlTableCell;
                var thFooterTotalExpenseVND = e.Item.FindControl("thFooterTotalExpenseVND") as HtmlTableCell;
                string moneyFormat = "";

                if (ActiveCruise.Name.ToLower() == "oriental sails" || ActiveCruise.Name.ToLower() == "oriental sails 2")
                {
                    moneyFormat = "#,0";
                }

                if (ActiveCruise.Name.ToLower() == "calypso cruiser")
                {
                    moneyFormat = "#,0";
                }

                if (ActiveCruise.Name.ToLower() == "starlight")
                {
                    moneyFormat = "#,0.00";
                    thFooterTotal3dayServiceExpense.Visible = false;
                    thFooterExchangeRate.Visible = true;
                    thFooterTotalExpenseVND.Visible = true;
                }
                string originalMoneyFormat = moneyFormat;

                var ltrTotalTotalCustomer = e.Item.FindControl("ltrTotalTotalCustomer") as Literal;
                ltrTotalTotalCustomer.Text = totalTotalCustomer.ToString("#,0.00");
                var ltrTotalTotal2dayCustomer = e.Item.FindControl("ltrTotalTotal2dayCustomer") as Literal;
                ltrTotalTotal2dayCustomer.Text = totalTotal2dayCustomer.ToString("#,0.00");
                var ltrTotalTotal2dayServiceExpense = e.Item.FindControl("ltrTotalTotal2dayServiceExpense") as Literal;
                if (totalTotal2dayServiceExpense == 0.0)
                {
                    moneyFormat = "";
                }
                ltrTotalTotal2dayServiceExpense.Text = totalTotal2dayServiceExpense.ToString(moneyFormat);
                moneyFormat = originalMoneyFormat;

                var ltrTotalTotal3dayCustomer = e.Item.FindControl("ltrTotalTotal3dayCustomer") as Literal;
                ltrTotalTotal3dayCustomer.Text = totalTotal3dayCustomer.ToString("#,0.00");
                var ltrTotalTotal3dayServiceExpense = e.Item.FindControl("ltrTotalTotal3dayServiceExpense") as Literal;
                if (totalTotal3dayServiceExpense == 0.0)
                {
                    moneyFormat = "";
                }
                ltrTotalTotal3dayServiceExpense.Text = totalTotal3dayServiceExpense.ToString(moneyFormat);
                moneyFormat = originalMoneyFormat;

                var ltrTotalTotalCruiseExpenseCustomer = e.Item.FindControl("ltrTotalTotalCruiseExpenseCustomer") as Literal;
                ltrTotalTotalCruiseExpenseCustomer.Text = totalTotalCruiseExpenseCustomer.ToString("#,0.00");

                var ltrTotalTotalCruiseExpense = e.Item.FindControl("ltrTotalTotalCruiseExpense") as Literal;
                if (totalTotalCruiseExpense == 0.0)
                {
                    moneyFormat = "";
                }
                ltrTotalTotalCruiseExpense.Text = totalTotalCruiseExpense.ToString(moneyFormat);
                moneyFormat = originalMoneyFormat;

                var ltrTotalTotalExpense = e.Item.FindControl("ltrTotalTotalExpense") as Literal;
                if (totalTotalExpense == 0.0)
                {
                    moneyFormat = "";
                }
                ltrTotalTotalExpense.Text = totalTotalExpense.ToString(moneyFormat);

                var ltrTotalTotalExpenseVND = e.Item.FindControl("ltrTotalTotalExpenseVND") as Literal;
                if (totalTotalExpense == 0.0)
                {
                    moneyFormat = "";
                }
                else
                {
                    moneyFormat = "#,0";
                }
                ltrTotalTotalExpenseVND.Text = totalTotalExpenseVND.ToString(moneyFormat);
            }
        }


        protected void btnExportExpense_Click(object sender, EventArgs e)
        {
            ExcelFile excelFile = new ExcelFile();
            ExcelWorksheet workSheet = excelFile.Worksheets.Add("Chi Phí Hải Phong");
            DateTime date = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            workSheet.Rows[0].Cells[5].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Cells.GetSubrangeAbsolute(0, 5, 0, 10).Merged = true;
            workSheet.Rows[0].Cells[5].Value = "BẢNG QUYẾT TOÁN CÔNG NỢ HẢI PHONG - Tàu " + ActiveCruise.Name;
            workSheet.Rows[1].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Cells.GetSubrangeAbsolute(1, 7, 1, 9);
            workSheet.Rows[1].Cells[7].Value = "THÁNG " + date.Month + " NĂM " + date.Year;
            workSheet.Rows[2].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            workSheet.Rows[2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            workSheet.Rows[2].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Rows[3].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Rows[2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[0].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Cells.GetSubrangeAbsolute(2, 0, 3, 0).Merged = true;
            workSheet.Rows[2].Cells[0].Value = "Ngày";

            workSheet.Rows[2].Cells[1].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[1].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Cells.GetSubrangeAbsolute(2, 1, 3, 1).Merged = true;
            workSheet.Rows[2].Cells[1].Value = "SK đi";

            workSheet.Rows[2].Cells[2].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[2].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[2].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Cells.GetSubrangeAbsolute(2, 2, 2, 4).Merged = true;
            workSheet.Rows[2].Cells[2].Value = "SK 2 ngày";

            workSheet.Rows[2].Cells[5].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[5].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[5].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Cells.GetSubrangeAbsolute(2, 5, 2, 7).Merged = true;
            workSheet.Rows[2].Cells[5].Value = "SK 3 ngày";

            workSheet.Rows[2].Cells[8].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[8].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[8].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Cells.GetSubrangeAbsolute(2, 8, 2, 9).Merged = true;
            workSheet.Rows[2].Cells[8].Value = "Tiền tàu";

            workSheet.Rows[2].Cells[10].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[10].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            workSheet.Rows[2].Cells[10].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Cells.GetSubrangeAbsolute(2, 10, 3, 10).Merged = true;
            workSheet.Rows[2].Cells[10].Value = "Tổng cộng";

            workSheet.Rows[3].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
            workSheet.Rows[3].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            workSheet.Rows[3].Cells[2].Value = "SK";
            workSheet.Rows[3].Cells[3].Value = "Đơn giá";
            workSheet.Rows[3].Cells[4].Value = "Thành tiền";
            workSheet.Rows[3].Cells[5].Value = "SK";
            workSheet.Rows[3].Cells[6].Value = "Đơn giá";
            workSheet.Rows[3].Cells[7].Value = "Thành tiền";
            workSheet.Rows[3].Cells[8].Value = "Đơn giá";
            workSheet.Rows[3].Cells[9].Value = "Thành tiền";

            if (ActiveCruise.Name.ToLower() == "starlight")
            {
                workSheet.Rows[2].Cells[8].Value = "Tiền tàu";
                workSheet.Rows[3].Cells[8].Value = "Thành tiền";
                workSheet.Cells.GetSubrangeAbsolute(2, 8, 2, 9).Merged = false;
                workSheet.Cells.GetSubrangeAbsolute(2, 9, 3, 9).Merged = true;
                workSheet.Rows[2].Cells[9].Value = "Tổng cộng";
                workSheet.Cells.GetSubrangeAbsolute(2, 9, 3, 9).Merged = true;
                workSheet.Rows[2].Cells[10].Value = "Tỷ giá";
                workSheet.Rows[2].Cells[11].Value = "Tổng cộng";
                workSheet.Rows[3].Cells[11].Value = "VNĐ";
            }

            var rowDataStart = 3;

            DateTime fromDate = DateTime.ParseExact(txtFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(txtTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var listDates = new List<DateTime>();
            for (var dt = fromDate; dt <= toDate; dt = dt.AddDays(1))
            {
                listDates.Add(dt);
            }

            for (int i = 0; i < listDates.Count; i++)
            {
                var startDate = listDates[i];
                var statusCriterion = Expression.Eq("Status", StatusType.Approved);
                var cruiseCriterion = Expression.Eq("Cruise.Id", ActiveCruise.Id);
                var startDateCriterion = Expression.Eq("StartDate", startDate);

                var criterion = Expression.And(statusCriterion, cruiseCriterion);
                criterion = Expression.And(criterion, startDateCriterion);

                var bookings = Module.GetObject<Booking>(criterion, 0, 0);
                var adult = 0;
                var baby = 0;
                var child6to8age = 0;
                var child9to12age = 0;
                var listBooking2Day = new List<Booking>();
                var listBooking3Day = new List<Booking>();
                foreach (var booking in bookings)
                {
                    var bookingRooms = booking.BookingRooms;
                    if (booking.Trip.NumberOfDay == 2)
                    {
                        listBooking2Day.Add(booking);
                    }
                    if (booking.Trip.NumberOfDay == 3)
                    {
                        listBooking3Day.Add(booking);
                    }
                    foreach (BookingRoom bookingRoom in bookingRooms)
                    {
                        var customers = bookingRoom.Customers;
                        foreach (Customer customer in customers)
                        {

                            if (customer.Birthday == null)
                            {
                                continue;
                            }
                            else
                            {
                                TimeSpan ts = booking.StartDate - customer.Birthday.Value;
                                int age = DateTime.MinValue.AddDays(ts.Days).Year - 1;
                                if (age < 6)
                                {
                                    baby = baby + 1;
                                }

                                if (age >= 6 && age <= 8)
                                {
                                    child6to8age = child6to8age + 1;
                                }

                                if (age >= 9 && age <= 12)
                                {
                                    child9to12age = child9to12age + 1;
                                }

                                if (age >= 13)
                                {
                                    adult = adult + 1;
                                }
                            }
                        }
                    }
                }

                var totalCustomer = ((adult * 1) + (child6to8age * 0.5) + (child9to12age * 0.75) + (baby * 0));

                double _2dayServiceExpenseUnitPrice = 0.0;
                double _3dayServiceExpenseUnitPrice = 0.0;
                double customerCruisePrice = 0.0;
                double cruisePrice = 0.0;
                string moneyFormat = "";
                double exchangeRate = 1.0;

                if (ActiveCruise.Name.ToLower() == "oriental sails" || ActiveCruise.Name.ToLower() == "oriental sails 2")
                {
                    _2dayServiceExpenseUnitPrice = 880000;
                    _3dayServiceExpenseUnitPrice = 1380000;
                    if (ActiveCruise.Name.ToLower() == "oriental sails")
                    {
                        customerCruisePrice = 210000;
                        cruisePrice = 9000000;
                    }

                    if (ActiveCruise.Name.ToLower() == "oriental sails 2")
                    {
                        customerCruisePrice = 120000;
                        cruisePrice = 6000000;
                    }

                    moneyFormat = "#,0";
                }

                if (ActiveCruise.Name.ToLower() == "calypso cruiser")
                {
                    _2dayServiceExpenseUnitPrice = 950000;
                    _3dayServiceExpenseUnitPrice = 1480000;
                    customerCruisePrice = 210000;
                    cruisePrice = 6000000;
                    moneyFormat = "#,0";
                }

                if (ActiveCruise.Name.ToLower() == "starlight")
                {
                    _2dayServiceExpenseUnitPrice = 72;
                    _3dayServiceExpenseUnitPrice = 118;
                    customerCruisePrice = 0;
                    cruisePrice = 1200;
                    moneyFormat = "#,0.00";
                    exchangeRate = 21000;
                }
                string originalMoneyFormat = moneyFormat;

                if (bookings.Count == 0)
                {
                    cruisePrice = 0.0;
                }

                /* Khách 2 ngày */
                bookings = listBooking2Day;
                adult = 0;
                baby = 0;
                child6to8age = 0;
                child9to12age = 0;
                foreach (var booking in bookings)
                {
                    var bookingRooms = booking.BookingRooms;

                    foreach (BookingRoom bookingRoom in bookingRooms)
                    {
                        var customers = bookingRoom.Customers;
                        foreach (Customer customer in customers)
                        {
                            if (customer.Birthday == null)
                            {
                                continue;
                            }
                            else
                            {
                                TimeSpan ts = booking.StartDate - customer.Birthday.Value;
                                int age = DateTime.MinValue.AddDays(ts.Days).Year - 1;
                                if (age < 6)
                                {
                                    baby = baby + 1;
                                }

                                if (age >= 6 && age <= 8)
                                {
                                    child6to8age = child6to8age + 1;
                                }

                                if (age >= 9 && age <= 12)
                                {
                                    child9to12age = child9to12age + 1;
                                }

                                if (age >= 13)
                                {
                                    adult = adult + 1;
                                }
                            }
                        }
                    }
                }
                var total2dayCustomer = ((adult * 1) + (child6to8age * 0.5) + (child9to12age * 0.75) + (baby * 0));

                var total2dayServiceExpense = (total2dayCustomer * _2dayServiceExpenseUnitPrice);

                /* Khách 3 ngày */
                bookings = listBooking3Day;

                adult = 0;
                baby = 0;
                child6to8age = 0;
                child9to12age = 0;
                foreach (var booking in bookings)
                {
                    var bookingRooms = booking.BookingRooms;
                    foreach (BookingRoom bookingRoom in bookingRooms)
                    {
                        var customers = bookingRoom.Customers;
                        foreach (Customer customer in customers)
                        {
                            if (customer.Birthday == null)
                            {
                                continue;
                            }
                            else
                            {
                                TimeSpan ts = booking.StartDate - customer.Birthday.Value;
                                int age = DateTime.MinValue.AddDays(ts.Days).Year - 1;
                                if (age < 6)
                                {
                                    baby = baby + 1;
                                }

                                if (age >= 6 && age <= 8)
                                {
                                    child6to8age = child6to8age + 1;
                                }

                                if (age >= 9 && age <= 12)
                                {
                                    child9to12age = child9to12age + 1;
                                }

                                if (age >= 13)
                                {
                                    adult = adult + 1;
                                }
                            }
                        }
                    }
                }
                var total3dayCustomer = ((adult * 1) + (child6to8age * 0.5) + (child9to12age * 0.75) + (baby * 0));

                var total3dayServiceExpense = (total3dayCustomer * _3dayServiceExpenseUnitPrice);

                var totalCruiseExpenseCustomer = total2dayCustomer + total3dayCustomer * 2;

                var totalCruiseExpense = totalCruiseExpenseCustomer * customerCruisePrice + cruisePrice;

                var totalExpense = (total2dayServiceExpense + total3dayServiceExpense + totalCruiseExpense);

                var totalExpenseVND = (total2dayServiceExpense + total3dayServiceExpense + totalCruiseExpense) * exchangeRate;



                rowDataStart = rowDataStart + 1;
                workSheet.Rows[rowDataStart].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                workSheet.Rows[rowDataStart].Cells[0].Value = startDate.Day;
                workSheet.Rows[rowDataStart].Cells[1].Value = totalCustomer.ToString("#,0.00");
                workSheet.Rows[rowDataStart].Cells[2].Value = total2dayCustomer.ToString("#,0.00");
                workSheet.Rows[rowDataStart].Cells[3].Value = _2dayServiceExpenseUnitPrice.ToString(moneyFormat);
                if (total2dayServiceExpense == 0.0)
                {
                    moneyFormat = "";
                }
                workSheet.Rows[rowDataStart].Cells[4].Value = total2dayServiceExpense.ToString(moneyFormat);
                moneyFormat = originalMoneyFormat;

                workSheet.Rows[rowDataStart].Cells[5].Value = total3dayCustomer.ToString("#,0.00");
                workSheet.Rows[rowDataStart].Cells[6].Value = _3dayServiceExpenseUnitPrice.ToString(moneyFormat);
                if (total3dayServiceExpense == 0.0)
                {
                    moneyFormat = "";
                }
                workSheet.Rows[rowDataStart].Cells[7].Value = total3dayServiceExpense.ToString(moneyFormat);
                moneyFormat = originalMoneyFormat;

                workSheet.Rows[rowDataStart].Cells[8].Value = totalCruiseExpenseCustomer.ToString("#,0.00");

                if (totalCruiseExpense == 0.0)
                {
                    moneyFormat = "";
                }
                workSheet.Rows[rowDataStart].Cells[9].Value = totalCruiseExpense.ToString(moneyFormat);
                moneyFormat = originalMoneyFormat;

                if (totalExpense == 0.0)
                {
                    moneyFormat = "";
                }
                workSheet.Rows[rowDataStart].Cells[10].Value = totalExpense.ToString(moneyFormat);
                moneyFormat = originalMoneyFormat;


                if (ActiveCruise.Name.ToLower() == "starlight")
                {
                    if (totalCruiseExpense == 0.0)
                    {
                        workSheet.Rows[rowDataStart].Cells[8].Value = totalCruiseExpense.ToString(moneyFormat);
                    }
                    moneyFormat = originalMoneyFormat;

                    if (totalExpense == 0.0)
                    {
                        workSheet.Rows[rowDataStart].Cells[9].Value = totalExpense.ToString(moneyFormat);
                    }
                    moneyFormat = originalMoneyFormat;
                    workSheet.Rows[rowDataStart].Cells[10].Value = exchangeRate.ToString(moneyFormat);

                    if (totalExpenseVND == 0.0)
                    {
                        workSheet.Rows[rowDataStart].Cells[11].Value = totalExpenseVND.ToString(moneyFormat);
                    }
                    moneyFormat = originalMoneyFormat;
                }
            }

            var totalMoneyFormat = "#,0";
            var totalOriginalMoneyFormat = "";
            if (ActiveCruise.Name.ToLower() == "starlight")
            {
                totalMoneyFormat = "#,0.00";
            }
            totalOriginalMoneyFormat = totalMoneyFormat;
            workSheet.Rows[rowDataStart + 1].Style.Font.Weight = ExcelFont.BoldWeight;
            workSheet.Rows[rowDataStart + 1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
            workSheet.Rows[rowDataStart + 1].Cells[1].Value = totalTotalCustomer.ToString("#,0.00");
            workSheet.Rows[rowDataStart + 1].Cells[2].Value = totalTotal2dayCustomer.ToString("#,0.00");
            if (totalTotal2dayServiceExpense == 0.0)
            {
                totalMoneyFormat = "";
            }
            workSheet.Rows[rowDataStart + 1].Cells[4].Value = totalTotal2dayServiceExpense.ToString(totalMoneyFormat);
            totalMoneyFormat = totalOriginalMoneyFormat;

            workSheet.Rows[rowDataStart + 1].Cells[5].Value = totalTotal3dayCustomer.ToString("#,0.00");
            if (totalTotal3dayServiceExpense == 0.0)
            {
                totalMoneyFormat = "";
            }
            workSheet.Rows[rowDataStart + 1].Cells[7].Value = totalTotal3dayServiceExpense.ToString(totalMoneyFormat);
            totalMoneyFormat = totalOriginalMoneyFormat;

            workSheet.Rows[rowDataStart + 1].Cells[8].Value = totalTotalCruiseExpenseCustomer.ToString("#,0.00");

            if (totalTotalCruiseExpense == 0.0)
            {
                totalMoneyFormat = "";
            }
            workSheet.Rows[rowDataStart + 1].Cells[9].Value = totalTotalCruiseExpense.ToString(totalMoneyFormat);
            totalMoneyFormat = totalOriginalMoneyFormat;

            if (totalTotal2dayServiceExpense == 0.0)
            {
                totalMoneyFormat = "";
            }
            workSheet.Rows[rowDataStart + 1].Cells[10].Value = totalTotalExpense.ToString(totalMoneyFormat);
            totalMoneyFormat = totalOriginalMoneyFormat;

            if (ActiveCruise.Name.ToLower() == "starlight")
            {
                if (totalTotalCruiseExpense == 0.0)
                {
                    workSheet.Rows[rowDataStart + 1].Cells[8].Value = totalTotalCruiseExpense.ToString(totalMoneyFormat);
                }
                totalMoneyFormat = totalOriginalMoneyFormat;

                if (totalTotalExpense == 0.0)
                {
                    workSheet.Rows[rowDataStart + 1].Cells[9].Value = totalTotalExpense.ToString(totalMoneyFormat);
                }
                totalMoneyFormat = totalOriginalMoneyFormat;
                workSheet.Rows[rowDataStart + 1].Cells[10].Value = "";

                if (totalTotalExpenseVND == 0.0)
                {
                    workSheet.Rows[rowDataStart + 1].Cells[11].Value = totalTotalExpenseVND.ToString(totalMoneyFormat);
                }
                totalMoneyFormat = totalOriginalMoneyFormat;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";

            Response.AppendHeader("content-disposition", "attachment; filename=" + string.Format("CongNoHaiPhong-Thang{0}-{1}.xls", date.Month, date.Year));

            MemoryStream m = new MemoryStream();

            excelFile.SaveXls(m);

            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();

            m.Close();
            Response.End();

        }
    }
}