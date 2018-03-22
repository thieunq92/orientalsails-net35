using CMS.Core.Domain;
using CMS.Core.Service;
using CMS.Core.Util;
using CMS.Web.Components;
using CMS.Web.Util;
using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin.BO
{
    public partial class TransactionResult : System.Web.UI.Page
    {
        private static bool TripBased
        {
            get
            {
                if (string.IsNullOrEmpty(Config.GetConfiguration()["BookingMode"]))
                {
                    return true;
                }
                return Config.GetConfiguration()["BookingMode"] == "TripBased";
            }
        }

        public static CoreRepository CoreRepository
        {
            get { return HttpContext.Current.Items["CoreRepository"] as CoreRepository; }
        }

        private static ModuleLoader ModuleLoader
        {
            get
            {
                return ContainerAccessorUtil.GetContainer().Resolve<ModuleLoader>();
            }
        }

        private static Section Section { get; set; }

        private static SailsModule Module
        {
            get
            {
                return (SailsModule)ModuleLoader.GetModuleFromSection(Section);
            }
        }

        private static ISession NSession
        {
            get
            {
                return Module.CommonDao.OpenSession();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int sectionId = Int32.Parse(Request.QueryString["SectionId"]);
            Section = (Section)CoreRepository.GetObjectById(typeof(Section), sectionId);

            var status = Request.QueryString["status"];
            if (status == "1")
            {
                lblStatus.Text = "Success";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }

            if (status == "2")
            {
                lblStatus.Text = "Fail";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

            var transactionId = Request.QueryString["tid"];
            lblTransactionId.Text = transactionId;

            var orderInformation = Request.QueryString["oi"];
            lblOrderInfomation.Text = orderInformation;

            var totalAmount = Request.QueryString["ta"];
            lblTotalAmount.Text = totalAmount;

            var bookingId = Request.QueryString["bid"];
            lblBookingId.Text = bookingId;
            var pBookingId = Int32.Parse(bookingId);

            var booking = NSession.QueryOver<Booking>().Where(x => x.Id == pBookingId).SingleOrDefault();
            lblDepartureDate.Text = booking.StartDate.ToLongDateString();
            lblItinerary.Text = booking.Trip.Name;
            lblCruise.Text = booking.Cruise.Name;

            var clBookingRoom = new List<BookingRoom>();
            var lBookingRoom = booking.BookingRooms;
            foreach (var bookingRoom in lBookingRoom)
            {
                clBookingRoom.Add((BookingRoom)bookingRoom);
            }

            var glBookingRoom = clBookingRoom.GroupBy(x => new { x.RoomClass, x.RoomType });
            foreach (var gBookingRoom in glBookingRoom)
            {
                lblRoom.Text = lblRoom.Text + gBookingRoom.Count() + " " +  gBookingRoom.Key.RoomClass.Name + gBookingRoom.Key.RoomType.Name + " ";
            }
        }
    }
}