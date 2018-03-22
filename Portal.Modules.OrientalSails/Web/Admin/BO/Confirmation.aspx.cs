using CMS.Core.Domain;
using CMS.Core.Service;
using CMS.Core.Util;
using CMS.Web.Components;
using CMS.Web.Util;
using Newtonsoft.Json;
using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace Portal.Modules.OrientalSails.Web.Admin.BO
{
    public partial class Confirmation : System.Web.UI.Page
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
            DateTime startDate = DateTime.ParseExact(Request.QueryString["sd"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
            lblStartDate.Text = startDate.ToLongDateString();
            int itineraryId = Int32.Parse(Request.QueryString["i"]);
            var itinerary = NSession.QueryOver<SailsTrip>().Where(x => x.Id == itineraryId).SingleOrDefault();
            lblItinerary.Text = itinerary.Name;
            int cruiseId = Int32.Parse(Request.QueryString["c"]);
            var cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == cruiseId).SingleOrDefault();
            lblCruise.Text = cruise.Name;
            lblYourName.Text = Request.QueryString["yn"];
            lblYourEmail.Text = Request.QueryString["ye"];
            lblYourPhone.Text = Request.QueryString["p"];

            var lBookedRoom = GetListBookedRoom();
            var lSingleRoom = GetListSingleRoom();
            var lChildren = GetListChildren();
            var lFullRoom = GetListFullRoom();

            foreach (var bookedRoom in lBookedRoom)
            {
                lblRoom.Text = lblRoom.Text + bookedRoom.Quantity + " " +
                    bookedRoom.RoomClass.Name +
                    bookedRoom.RoomType.Name + " ";

                foreach (var singleRoom in lSingleRoom)
                {
                    if (singleRoom.RoomClass == bookedRoom.RoomClass && singleRoom.RoomType == bookedRoom.RoomType)
                    {
                        lblRoom.Text = lblRoom.Text + "(" + " " + singleRoom.Quantity + " " + "Single Room" + " " + ")";
                    }
                }

                foreach (var children in lChildren)
                {
                    if (children.RoomClass == bookedRoom.RoomClass && children.RoomType == bookedRoom.RoomType)
                    {
                        lblRoom.Text = lblRoom.Text + " " + "+" + " " + children.Quantity + " " + "Children" + "<br/>";
                    }
                }
            }

            var fullRoomPrice = GetRoomPrice(lFullRoom, cruise, itinerary);
            var singleRoomPrice = GetSingleRoomPrice(lSingleRoom, cruise, itinerary);
            var childrenPrice = GetChildrenPrice(lChildren, cruise, itinerary);
            var exchangeRate = GetExchangeRate();
            var totalAmount = fullRoomPrice + singleRoomPrice + childrenPrice;
            var exchangedTotalAmount = totalAmount * exchangeRate;
            lblTotalAmount.Text = totalAmount.ToString("N", CultureInfo.GetCultureInfo("en-US")) + " " + "USD x " + "<a href='https://www.vietcombank.com.vn/exchangerates/' data-toggle='tooltip' title='We use Vietcombank &#39;s Exchange Rate. Click to see more' target='_blank'>" + exchangeRate.ToString("N", CultureInfo.GetCultureInfo("en-US")) + "</a>" + " = " + exchangedTotalAmount.ToString("N", CultureInfo.GetCultureInfo("en-US")) + " VND";
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            var startdate = Request.QueryString["sd"];
            var tripId = Request.QueryString["i"];
            var ptripId = Int32.Parse(tripId);
            var trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == ptripId).SingleOrDefault();
            var sectionId = Request.QueryString["SectionId"];
            var cruiseId = Request.QueryString["c"];
            var pcruiseId = Int32.Parse(cruiseId);
            var cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == pcruiseId).SingleOrDefault();
            var paymentMethod = Request.Params["paymentmethod"];
            var pPaymentMethod = Int32.Parse(paymentMethod);

            var lBookedRoom = GetListBookedRoom();
            var lSingleRoom = GetListSingleRoom();
            var lChildren = GetListChildren();
            var lFullRoom = GetListFullRoom();
            var fullRoomPrice = GetRoomPrice(lFullRoom, cruise, trip);
            var singleRoomPrice = GetSingleRoomPrice(lSingleRoom, cruise, trip);
            var childrenPrice = GetChildrenPrice(lChildren, cruise, trip);
            var exchangeRate = GetExchangeRate();
            var totalAmount = fullRoomPrice + singleRoomPrice + childrenPrice;
            var exchangedTotalAmount = totalAmount * exchangeRate;

            var lRoomInCruise = JsonConvert.DeserializeObject<List<JsonModel.RoomInCruise>>(BookingTour.CheckAvaiable(startdate, tripId, sectionId));

            bool canAddBooking = false;
            foreach (var roomInCruise in lRoomInCruise)
            {
                if (roomInCruise.JCruise.CruiseId.ToString() != cruiseId)
                    continue;
                var lavaiableRoom = roomInCruise.LJAvaiableRoom;
                foreach (var avaiableRoom in lavaiableRoom)
                {
                    foreach (var bookedRoom in lBookedRoom)
                    {
                        if (avaiableRoom.JRoomClass.RoomClassId == bookedRoom.RoomClass.Id &&
                            avaiableRoom.JRoomType.RoomTypeId == bookedRoom.RoomType.Id)
                        {
                            if (bookedRoom.Quantity > avaiableRoom.LJRoom.Count())
                            {
                                canAddBooking = false;
                                continue;
                            }
                            canAddBooking = true;
                        }
                    }
                }
            }

            if (!canAddBooking)
                return;


            var booking = new Booking()
            {
                Trip = trip,
                Cruise = cruise,
                StartDate = DateTime.ParseExact(startdate, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                CreatedDate = DateTime.Now,
                Status = StatusType.Pending,
                ModifiedDate = DateTime.Now,
                IsTotalUsd = false,
                Total = exchangedTotalAmount,
                IsPaid = true,

            };
            booking.EndDate = booking.StartDate.AddDays(booking.Trip.NumberOfDay - 1);
            NSession.Save(booking);

            lBookedRoom.OrderBy(x => x.RoomClass).ThenBy(x => x.RoomType);
            foreach (var bookedRoom in lBookedRoom)
            {
                var childrenCount = 0;

                foreach (var singleRoom in lSingleRoom)
                {
                    if (bookedRoom.RoomClass == singleRoom.RoomClass && bookedRoom.RoomType == singleRoom.RoomType)
                    {
                        foreach (var children in lChildren)
                        {
                            if (singleRoom.RoomClass == children.RoomClass && singleRoom.RoomType == children.RoomType)
                            {
                                int singleRoomCount = 0;
                                while (singleRoomCount < singleRoom.Quantity)
                                {
                                    var bookingRoom = new BookingRoom()
                                    {
                                        Book = booking,
                                        RoomType = bookedRoom.RoomType,
                                        RoomClass = bookedRoom.RoomClass,
                                        IsSingle = true,
                                    };
                                    Customer customer = new Customer()
                                    {
                                        BookingRoom = bookingRoom,
                                        Booking = booking,
                                    };
                                    NSession.Save(customer);

                                    if (childrenCount < children.Quantity)
                                    {
                                        bookingRoom.HasChild = true;
                                        Customer childCustomer = new Customer()
                                        {
                                            IsChild = true,
                                            BookingRoom = bookingRoom,
                                            Booking = booking,
                                        };
                                        NSession.Save(childCustomer);
                                        childrenCount++;
                                    }
                                    NSession.Save(bookingRoom);
                                    singleRoomCount++;
                                }
                            }
                        }
                    }
                }

                foreach (var fullRoom in lFullRoom)
                {
                    if (bookedRoom.RoomClass == fullRoom.RoomClass && bookedRoom.RoomType == fullRoom.RoomType)
                    {
                        foreach (var children in lChildren)
                        {
                            if (fullRoom.RoomClass == children.RoomClass && fullRoom.RoomType == children.RoomType)
                            {
                                int fullRoomCount = 0;
                                while (fullRoomCount < fullRoom.Quantity)
                                {
                                    var bookingRoom = new BookingRoom()
                                    {
                                        Book = booking,
                                        RoomType = bookedRoom.RoomType,
                                        RoomClass = bookedRoom.RoomClass,
                                        IsSingle = false,
                                    };
                                    NSession.Save(bookingRoom);
                                    Customer customer = new Customer()
                                    {
                                        BookingRoom = bookingRoom,
                                        Booking = booking,
                                    };
                                    NSession.Save(customer);
                                    if (childrenCount < children.Quantity)
                                    {
                                        bookingRoom.HasChild = true;
                                        Customer childCustomer = new Customer()
                                        {
                                            IsChild = true,
                                            BookingRoom = bookingRoom,
                                            Booking = booking,
                                        };
                                        NSession.Save(childCustomer);
                                        childrenCount++;
                                    }
                                    NSession.Update(bookingRoom);
                                    fullRoomCount++;
                                }
                            }
                        }
                    }
                }

            }

            string SECURE_SECRET = "A3EFDFABA8653DF2342E8DAC29B51AF0";
            string vpcMerchant = "ONEPAY";
            string vpcAccessCode = "D67342C2";
            string vpcRequestString = "https://mtf.onepay.vn/onecomm-pay/vpc.op";
            string vpcReturnUrl = "http://localhost:1988/Modules/Sails/Admin/Bo/ResultPayment.aspx?NodeId=1&SectionId=15&module=noidia";

            if (pPaymentMethod == 1)
            {
                vpcRequestString = "" + vpcRequestString;
                SECURE_SECRET = "" + SECURE_SECRET;
                vpcMerchant = "" + vpcMerchant;
                vpcAccessCode = "" + vpcAccessCode;
                vpcReturnUrl = "" + vpcReturnUrl;
            }//config module noi dia

            if (pPaymentMethod == 2)
            {
                vpcRequestString = "https://mtf.onepay.vn/vpcpay/vpcpay.op";
                SECURE_SECRET = "6D0870CDE5F24F34F3915FB0045120DB";
                vpcMerchant = "TESTONEPAY";
                vpcAccessCode = "6BEB2546";
                vpcReturnUrl = "http://localhost:1988/Modules/Sails/Admin/Bo/ResultPayment.aspx?NodeId=1&SectionId=15&module=quocte";
            }//config module quoc te
            VPCRequest conn = new VPCRequest(vpcRequestString);
            conn.SetSecureSecret(SECURE_SECRET);
            // Add the Digital Order Fields for the functionality you wish to use
            // Core Transaction Fields
            conn.AddDigitalOrderField("AgainLink", "http://onepay.vn");
            conn.AddDigitalOrderField("Title", "OnePAY PAYMENT GATEWAY");
            conn.AddDigitalOrderField("vpc_Locale", "en");//Chon ngon ngu hien thi tren cong thanh toan (vn/en)
            conn.AddDigitalOrderField("vpc_Version", "2");
            conn.AddDigitalOrderField("vpc_Command", "pay");

            if (pPaymentMethod == 1)
                conn.AddDigitalOrderField("vpc_Currency", "VND");

            conn.AddDigitalOrderField("vpc_Merchant", vpcMerchant);//Thay đổi
            conn.AddDigitalOrderField("vpc_AccessCode", vpcAccessCode);//Thay đổi
            conn.AddDigitalOrderField("vpc_MerchTxnRef", booking.Id.ToString());// Thay đổi
            conn.AddDigitalOrderField("vpc_OrderInfo", "Pay for booking " + booking.Id.ToString());
            conn.AddDigitalOrderField("vpc_Amount", (exchangedTotalAmount * 100).ToString());
            conn.AddDigitalOrderField("vpc_ReturnURL", vpcReturnUrl);

            // Dia chi IP cua khach hang
            conn.AddDigitalOrderField("vpc_TicketNo", GetIPAddress());
            // Chuyen huong trinh duyet sang cong thanh toan
            String url = conn.Create3PartyQueryString();
            Page.Response.Redirect(url);
        }

        public IList<BookedRoom> GetListBookedRoom()
        {
            var qsRooms = Request.QueryString["rooms"];
            int beginning = 0;
            var lBookedRoom = new List<BookedRoom>();
            while (beginning <= qsRooms.Length - 1)
            {
                var regex = new Regex(@"(\[[\w,]+\])+");
                var mgroupedRoom = regex.Match(qsRooms, beginning);
                if (mgroupedRoom.Success)
                {
                    var groupedRoom = mgroupedRoom.Groups[0];
                    var vgroupedRoom = groupedRoom.Value;
                    vgroupedRoom = vgroupedRoom.Replace("[", "");
                    vgroupedRoom = vgroupedRoom.Replace("]", "");
                    var propertiesRoom = vgroupedRoom.Split(new char[] { ',' });
                    var roomclassId = Int32.Parse(propertiesRoom[0]);
                    var roomtypeId = Int32.Parse(propertiesRoom[1]);
                    var quantity = Int32.Parse(propertiesRoom[2]);
                    var bookedRoom = new BookedRoom()
                    {
                        RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == roomclassId).SingleOrDefault(),
                        RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == roomtypeId).SingleOrDefault(),
                        Quantity = quantity,
                    };
                    lBookedRoom.Add(bookedRoom);
                    beginning = groupedRoom.Index + groupedRoom.Length;
                    continue;
                }
                break;
            }
            return lBookedRoom;
        }

        public IList<BookedRoom> GetListFullRoom()
        {
            var lFullRoom = new List<BookedRoom>();
            var lBookedRoom = GetListBookedRoom();
            var lSingleRoom = GetListSingleRoom();
            foreach (var bookedRoom in lBookedRoom)
            {
                foreach (var singleRoom in lSingleRoom)
                {
                    if (singleRoom.RoomClass == bookedRoom.RoomClass && bookedRoom.RoomType == singleRoom.RoomType)
                    {
                        bookedRoom.Quantity = bookedRoom.Quantity - singleRoom.Quantity;
                    }
                }
                lFullRoom.Add(bookedRoom);
            }
            return lFullRoom;
        }

        public IList<BookedRoom> GetListSingleRoom()
        {
            var qsSingleRoom = Request.QueryString["singleroom"];
            int beginning = 0;
            var lSingleRoom = new List<BookedRoom>();
            while (beginning <= qsSingleRoom.Length - 1)
            {
                var regex = new Regex(@"(\[[\w,]+\])+");
                var mgroupedRoom = regex.Match(qsSingleRoom, beginning);
                if (mgroupedRoom.Success)
                {
                    var groupedRoom = mgroupedRoom.Groups[0];
                    var vgroupedRoom = groupedRoom.Value;
                    vgroupedRoom = vgroupedRoom.Replace("[", "");
                    vgroupedRoom = vgroupedRoom.Replace("]", "");
                    var propertiesRoom = vgroupedRoom.Split(new char[] { ',' });
                    var roomclassId = Int32.Parse(propertiesRoom[0]);
                    var roomtypeId = Int32.Parse(propertiesRoom[1]);
                    var quantity = Int32.Parse(propertiesRoom[2]);
                    var test = NSession.QueryOver<RoomClass>().List();
                    var bookedRoom = new BookedRoom();
                    bookedRoom.RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == roomclassId).SingleOrDefault();
                    bookedRoom.RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == roomtypeId).SingleOrDefault();
                    bookedRoom.Quantity = quantity;
                    lSingleRoom.Add(bookedRoom);
                    beginning = groupedRoom.Index + groupedRoom.Length;
                    continue;
                }
                break;
            }
            return lSingleRoom;
        }

        public IList<BookedChildren> GetListChildren()
        {
            var qsChildren = Request.QueryString["children"];
            int beginning = 0;
            var lChildren = new List<BookedChildren>();
            while (beginning <= qsChildren.Length - 1)
            {
                var regex = new Regex(@"(\[[\w,]+\])+");
                var mgroupedRoom = regex.Match(qsChildren, beginning);
                if (mgroupedRoom.Success)
                {
                    var groupedRoom = mgroupedRoom.Groups[0];
                    var vgroupedRoom = groupedRoom.Value;
                    vgroupedRoom = vgroupedRoom.Replace("[", "");
                    vgroupedRoom = vgroupedRoom.Replace("]", "");
                    var propertiesRoom = vgroupedRoom.Split(new char[] { ',' });
                    var roomclassId = Int32.Parse(propertiesRoom[0]);
                    var roomtypeId = Int32.Parse(propertiesRoom[1]);
                    var quantity = Int32.Parse(propertiesRoom[2]);
                    var test = NSession.QueryOver<RoomClass>().List();
                    var bookedChildren = new BookedChildren();
                    bookedChildren.RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == roomclassId).SingleOrDefault();
                    bookedChildren.RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == roomtypeId).SingleOrDefault();
                    bookedChildren.Quantity = quantity;
                    lChildren.Add(bookedChildren);
                    beginning = groupedRoom.Index + groupedRoom.Length;
                    continue;
                }
                break;
            }
            return lChildren;
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public double GetExchangeRate()
        {
            try
            {
                var webRequest = WebRequest.Create(@"http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx");
                var response = webRequest.GetResponse();
                var rs = response.GetResponseStream();
                var sr = new StreamReader(rs);
                var exchangeDocument = sr.ReadToEnd();
                var xd = XDocument.Parse(exchangeDocument);
                var exchangeRate = xd.Descendants().Where(x => (string)x.Attribute("CurrencyCode") == "USD").FirstOrDefault().Attribute("Transfer").Value;
                var pExchangeRate = Double.Parse(exchangeRate);
                return pExchangeRate;
            }
            catch (Exception)
            {
                double pExchangeRate = 21000;
                return pExchangeRate;
            }
        }

        protected double GetRoomPrice(IList<BookedRoom> lBookedRoom, Cruise cruise, SailsTrip trip)
        {
            var lRoomPrice = new List<RoomPrice>();

            var roomPrice1 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 2).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = false,
                Price = 20,
            };//os hl2d  deluxe double

            var roomPrice2 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 3).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = false,
                Price = 40,
            };//os hl2d deluxe twwin

            var roomPrice3 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 3).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 2).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = false,
                Price = 100,
            }; //os2 hl2d deluxe double

            var roomPrice4 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 3).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 3).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = false,
                Price = 70,
            }; //os2 hl2d deluxe twin

            var roomPrice5 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 3).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 7).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = false,
                Price = 120,
            }; //os2 hl2d deluxe triple
            lRoomPrice.Add(roomPrice1);
            lRoomPrice.Add(roomPrice2);
            lRoomPrice.Add(roomPrice3);
            lRoomPrice.Add(roomPrice4);
            lRoomPrice.Add(roomPrice5);

            lRoomPrice = lRoomPrice.Where(x => x.Cruise == cruise && x.Trip == trip).ToList();

            double allRoomPrice = 0.0;
            foreach (var bookedRoom in lBookedRoom)
            {
                foreach (var roomPrice in lRoomPrice)
                {
                    if (bookedRoom.RoomClass == roomPrice.RoomClass && bookedRoom.RoomType == roomPrice.RoomType)
                    {
                        allRoomPrice = allRoomPrice + roomPrice.Price * bookedRoom.Quantity;
                    }
                }
            }

            return allRoomPrice;
        }

        public double GetSingleRoomPrice(IList<BookedRoom> lSingleRoom, Cruise cruise, SailsTrip trip)
        {
            var lRoomPrice = new List<RoomPrice>();
            var roomPrice1 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 2).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = true,
                Price = 10,
            };//os hl2d  deluxe double

            var roomPrice2 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 3).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = true,
                Price = 30,
            };//os hl2d deluxe twwin

            var roomPrice3 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 3).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 2).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = true,
                Price = 200,
            }; //os2 hl2d deluxe double

            var roomPrice4 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 3).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 3).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = true,
                Price = 10,
            }; //os2 hl2d deluxe twin

            var roomPrice5 = new RoomPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 3).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 7).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                SingleRoom = true,
                Price = 320,
            }; //os2 hl2d deluxe triple
            lRoomPrice.Add(roomPrice1);
            lRoomPrice.Add(roomPrice2);
            lRoomPrice.Add(roomPrice3);
            lRoomPrice.Add(roomPrice4);
            lRoomPrice.Add(roomPrice5);

            lRoomPrice = lRoomPrice.Where(x => x.Cruise == cruise && x.Trip == trip).ToList();

            double allRoomPrice = 0.0;
            foreach (var singleRoom in lSingleRoom)
            {
                foreach (var roomPrice in lRoomPrice)
                {
                    if (singleRoom.RoomClass == roomPrice.RoomClass && singleRoom.RoomType == roomPrice.RoomType)
                    {
                        allRoomPrice = allRoomPrice + roomPrice.Price * singleRoom.Quantity;
                    }
                }
            }

            return allRoomPrice;
        }

        public double GetChildrenPrice(IList<BookedChildren> lChildren, Cruise cruise, SailsTrip trip)
        {
            var lChildrenPrice = new List<ChildrenPrice>();
            var childrenPrice1 = new ChildrenPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 2).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                Price = 10,
            };//os hl2d  deluxe double

            var childrenPrice2 = new ChildrenPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 3).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                Price = 30,
            };//os hl2d deluxe twwin

            var childrenPrice3 = new ChildrenPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 3).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 2).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                Price = 200,
            }; //os2 hl2d deluxe double

            var childrenPrice4 = new ChildrenPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 3).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 3).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                Price = 10,
            }; //os2 hl2d deluxe twin

            var childrenPrice5 = new ChildrenPrice()
            {
                Cruise = NSession.QueryOver<Cruise>().Where(x => x.Id == 3).SingleOrDefault(),
                RoomClass = NSession.QueryOver<RoomClass>().Where(x => x.Id == 1).SingleOrDefault(),
                RoomType = NSession.QueryOver<RoomTypex>().Where(x => x.Id == 7).SingleOrDefault(),
                Trip = NSession.QueryOver<SailsTrip>().Where(x => x.Id == 2).SingleOrDefault(),
                Price = 320,
            }; //os2 hl2d deluxe triple
            lChildrenPrice.Add(childrenPrice1);
            lChildrenPrice.Add(childrenPrice2);
            lChildrenPrice.Add(childrenPrice3);
            lChildrenPrice.Add(childrenPrice4);
            lChildrenPrice.Add(childrenPrice5);

            lChildrenPrice = lChildrenPrice.Where(x => x.Cruise == cruise && x.Trip == trip).ToList();

            double allChildrenPrice = 0.0;
            foreach (var children in lChildren)
            {
                foreach (var childrenPrice in lChildrenPrice)
                {
                    if (children.RoomClass == childrenPrice.RoomClass && children.RoomType == childrenPrice.RoomType)
                    {
                        allChildrenPrice = allChildrenPrice + childrenPrice.Price * children.Quantity;
                    }
                }
            }

            return allChildrenPrice;
        }

        public class BookedRoom
        {
            public RoomClass RoomClass { get; set; }

            public RoomTypex RoomType { get; set; }

            public bool SingleRoom { get; set; }

            public int Quantity { get; set; }
        }

        public class BookedChildren
        {
            public RoomClass RoomClass { get; set; }

            public RoomTypex RoomType { get; set; }

            public int Quantity { get; set; }

        }

        public class RoomPrice
        {
            public Cruise Cruise { get; set; }

            public RoomClass RoomClass { get; set; }

            public RoomTypex RoomType { get; set; }

            public SailsTrip Trip { get; set; }

            public bool SingleRoom { get; set; }

            public double Price { get; set; }
        }

        public class ChildrenPrice
        {
            public Cruise Cruise { get; set; }

            public RoomClass RoomClass { get; set; }

            public RoomTypex RoomType { get; set; }

            public SailsTrip Trip { get; set; }

            public double Price { get; set; }
        }
    }
}