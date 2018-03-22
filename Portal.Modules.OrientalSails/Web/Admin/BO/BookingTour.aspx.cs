using Castle.Facilities.NHibernateIntegration;
using CMS.Core.Domain;
using CMS.Core.Service;
using CMS.Core.Util;
using CMS.Web.Components;
using CMS.Web.Util;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.DataAccess;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Web.Admin.BO.JsonModel;
using domain = Portal.Modules.OrientalSails.Domain;
using json = Portal.Modules.OrientalSails.Web.Admin.BO.JsonModel;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

namespace Portal.Modules.OrientalSails.Web.Admin.BO
{
    public partial class BookingTour : Page
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

        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            var url = "./Confirmation.aspx?NodeId=1&SectionId=15";
            var sbUrl = new StringBuilder(url);
            var startDate = Request.Params["StartDate"];
            sbUrl.Append("&sd=" + startDate).Append("&");
            var itinerary = Request.Params["Itinerary"];
            sbUrl.Append("i=" + itinerary).Append("&");
            var cruise = Request.Params["Cruise"];
            sbUrl.Append("c=" + cruise).Append("&");
            var yourName = Request.Params["YourName"];
            sbUrl.Append("yn=" + yourName).Append("&");
            var yourEmail = Request.Params["YourEmail"];
            sbUrl.Append("ye=" + yourEmail).Append("&");
            var yourPhone = Request.Params["YourPhone"];
            sbUrl.Append("p=" + yourPhone).Append("&");
            var rooms = Request.Params["rooms"];
            sbUrl.Append("rooms=" + rooms).Append("&");
            var singleroom = Request.Params["singleroom"];
            sbUrl.Append("singleroom=" + singleroom).Append("&");
            var children = Request.Params["children"];
            sbUrl.Append("children=" + children).Append("&");
            Page.Response.Redirect(sbUrl.ToString());
        }

        [System.Web.Services.WebMethod]
        public static string CheckAvaiable(string sd, string tid, string sid)
        {
            try
            {
                int sectionId = Int32.Parse(sid);
                Section = (Section)CoreRepository.GetObjectById(typeof(Section), sectionId);

                DateTime pStartDate = DateTime.ParseExact(sd, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                int tripId = Int32.Parse(tid);
                var trip = Module.TripGetById(tripId);
                var cruises = Module.CruiseGetAll();
                var involvedCruises = new List<domain.Cruise>();
                foreach (domain.Cruise cruise in cruises)
                {
                    if (cruise.Trips.Contains(trip))
                    {
                        involvedCruises.Add(cruise);
                    }
                }

                var ljRoomInCruise = new List<json.RoomInCruise>();
                IList<json.GroupedRoom> ljAvaiableGroupedRoom = null;
                IList<json.GroupedRoom> ljPendingGroupedRoom = null;
                foreach (domain.Cruise cruise in involvedCruises)
                {
                    var lRoomInCruise = NSession.QueryOver<domain.Room>().
                        Where(x => x.Cruise == cruise).
                        Fetch(x => x.RoomClass).Eager.
                        Fetch(x => x.RoomType).Eager.
                        List();
                    var lgrInCruise = lRoomInCruise.GroupBy(x => new { x.RoomClass, x.RoomType }).OrderBy(x => x.Key.RoomClass.Id).ThenBy(x => x.Key.RoomType.Id).ToList();

                    var tripDuration = trip.NumberOfDay - 1;

                    for (var i = 0; i < tripDuration; i++)
                    {
                        var firstDay = false;
                        if (i == 0)
                            firstDay = true;
                        var dayToCheck = pStartDate.AddDays(i);
                        var lBookingOnDayToCheck = NSession.QueryOver<BookingRoom>().
                            JoinQueryOver<Booking>(x => x.Book).
                            Where(x => x.StartDate <= dayToCheck && x.EndDate > dayToCheck).
                            Where(x => x.Cruise.Id == cruise.Id && x.Deleted == false).
                                Where(x => x.Status == StatusType.Approved || x.Status == StatusType.Pending).
                                Fetch(x => x.RoomClass).Eager.
                                Fetch(x => x.RoomType).Eager.
                                List();

                        var glBookingOnDayToCheck = lBookingOnDayToCheck.GroupBy(x => new { x.RoomClass, x.RoomType }).ToList();
                        var ljGroupedRoom = new List<json.GroupedRoom>();
                        for (int j = 0; j < lgrInCruise.Count(); j++)
                        {
                            var legrIncruise = lgrInCruise[j].ToList();
                            for (int k = 0; k < glBookingOnDayToCheck.Count(); k++)
                            {
                                if (lgrInCruise[j].Key.RoomClass == glBookingOnDayToCheck[k].Key.RoomClass &&
                                    lgrInCruise[j].Key.RoomType == glBookingOnDayToCheck[k].Key.RoomType)
                                {
                                    legrIncruise.RemoveRange(0, glBookingOnDayToCheck[k].Count());
                                    break;
                                }
                            }

                            var ljRoom = new List<json.Room>();
                            foreach (var egrInCruise in legrIncruise)
                            {
                                var jRoom = new json.Room()
                                {
                                    RoomId = egrInCruise.Id
                                };
                                ljRoom.Add(jRoom);
                            }

                            var jgroupedRoom = new json.GroupedRoom()
                            {
                                JRoomType = new json.RoomType()
                                {
                                    RoomTypeId = lgrInCruise[j].Key.RoomType.Id,
                                    Name = lgrInCruise[j].Key.RoomType.Name
                                },
                                JRoomClass = new json.RoomClass()
                                {
                                    RoomClassId = lgrInCruise[j].Key.RoomClass.Id,
                                    Name = lgrInCruise[j].Key.RoomClass.Name
                                },
                                LJRoom = ljRoom,
                            };

                            ljGroupedRoom.Add(jgroupedRoom);
                        }

                        if (firstDay)
                        {
                            ljAvaiableGroupedRoom = ljGroupedRoom;
                            continue;
                        }
                        for (int k = 0; k < ljGroupedRoom.Count(); k++)
                        {
                            for (int l = 0; l < ljAvaiableGroupedRoom.Count(); l++)
                            {
                                if (ljAvaiableGroupedRoom[l].JRoomClass == ljGroupedRoom[k].JRoomClass &&
                                    ljAvaiableGroupedRoom[l].JRoomType == ljGroupedRoom[k].JRoomType)
                                {
                                    if (ljGroupedRoom[k].LJRoom.Count() < ljAvaiableGroupedRoom[l].LJRoom.Count())
                                    {
                                        ljAvaiableGroupedRoom[l].LJRoom = ljGroupedRoom[k].LJRoom;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    for (var i = 0; i < tripDuration; i++)
                    {
                        var firstDay = false;
                        if (i == 0)
                            firstDay = true;

                        var dayToCheck = pStartDate.AddDays(i);
                        var lBookingOnDayToCheck = NSession.QueryOver<BookingRoom>().
                            JoinQueryOver<Booking>(x => x.Book).
                            Where(x => x.StartDate <= dayToCheck && x.EndDate > dayToCheck).
                            Where(x => x.Cruise.Id == cruise.Id && x.Deleted == false).
                                Where(x => x.Status == StatusType.Pending).
                                Fetch(x => x.RoomClass).Eager.
                                Fetch(x => x.RoomType).Eager.
                                List();
                        var glBookingPendingOnDayToCheck = lBookingOnDayToCheck.GroupBy(x => new { x.RoomClass, x.RoomType }).OrderBy(x => x.Key.RoomClass.Id).ThenBy(x => x.Key.RoomType.Id).ToList();

                        var ljGroupedRoom = new List<json.GroupedRoom>();
                        for (int k = 0; k < glBookingPendingOnDayToCheck.Count(); k++)
                        {
                            var gBookingPendingOnDayToCheck = glBookingPendingOnDayToCheck[k].ToList();
                            var ljRoom = new List<json.Room>();
                            for (int j = 0; j < gBookingPendingOnDayToCheck.Count(); j++)
                            {
                                var jRoom = new json.Room();
                                ljRoom.Add(jRoom);
                            }

                            var jgroupedRoom = new json.GroupedRoom()
                            {
                                JRoomType = new json.RoomType()
                                {
                                    RoomTypeId = glBookingPendingOnDayToCheck[k].Key.RoomType.Id,
                                    Name = glBookingPendingOnDayToCheck[k].Key.RoomType.Name
                                },
                                JRoomClass = new json.RoomClass()
                                {
                                    RoomClassId = glBookingPendingOnDayToCheck[k].Key.RoomClass.Id,
                                    Name = glBookingPendingOnDayToCheck[k].Key.RoomClass.Name
                                },
                                LJRoom = ljRoom,
                            };
                            ljGroupedRoom.Add(jgroupedRoom);
                        }

                        if (firstDay)
                        {
                            ljPendingGroupedRoom = ljGroupedRoom;
                            continue;
                        }
                        for (int k = 0; k < ljGroupedRoom.Count(); k++)
                        {
                            for (int l = 0; l < ljPendingGroupedRoom.Count(); l++)
                            {
                                if (ljPendingGroupedRoom[l].JRoomClass == ljGroupedRoom[k].JRoomClass &&
                                    ljPendingGroupedRoom[l].JRoomType == ljGroupedRoom[k].JRoomType)
                                {
                                    if (ljGroupedRoom[k].LJRoom.Count() > ljPendingGroupedRoom[l].LJRoom.Count())
                                    {
                                        ljPendingGroupedRoom[l].LJRoom = ljGroupedRoom[k].LJRoom;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    var avaiableRoomQuantity = 0;
                    var avaiableRoomDetail = "";
                    foreach (var jAvaiableGroupedRoom in ljAvaiableGroupedRoom)
                    {
                        avaiableRoomQuantity = avaiableRoomQuantity + jAvaiableGroupedRoom.LJRoom.Count();
                        avaiableRoomDetail = avaiableRoomDetail +
                            jAvaiableGroupedRoom.LJRoom.Count() + " " +
                            jAvaiableGroupedRoom.JRoomClass.Name +
                            jAvaiableGroupedRoom.JRoomType.Name + " ";
                    }

                    var pendingRoomQuantity = 0;
                    var pendingRoomDetail = "";
                    foreach (var jPendingGroupedRoom in ljPendingGroupedRoom)
                    {
                        pendingRoomQuantity = pendingRoomQuantity + jPendingGroupedRoom.LJRoom.Count();
                        pendingRoomDetail = pendingRoomDetail +
                            jPendingGroupedRoom.LJRoom.Count() + " " +
                            jPendingGroupedRoom.JRoomClass.Name +
                            jPendingGroupedRoom.JRoomType.Name + " ";
                    }

                    var roomInCruise = new json.RoomInCruise()
                    {
                        JCruise = new json.Cruise() { CruiseId = cruise.Id, Name = cruise.Name },
                        LJAvaiableRoom = ljAvaiableGroupedRoom,
                        LJPendingRoom = ljPendingGroupedRoom,
                        AvaiableRoomQuantity = avaiableRoomQuantity.ToString(),
                        AvaiableRoomDetail = avaiableRoomDetail,
                        PendingRoomQuantity = pendingRoomQuantity.ToString(),
                        PendingRoomDetail = pendingRoomDetail,
                    };
                    ljRoomInCruise.Add(roomInCruise);
                }
                return JsonConvert.SerializeObject(ljRoomInCruise);
            }
            catch (Exception ex)
            {
                return "not working";
            }
        }

        [System.Web.Services.WebMethod]
        public static string HandleRequestPendingRoom(string sid, string sd, string tid, string cid, string yn, string ye, string yp, string yr)
        {
            SmtpClient smtpClient = new SmtpClient("mail.atravelmate.com");
            smtpClient.Credentials = new NetworkCredential("it2@atravelmate.com", "Thieudeptrai02");
            MailAddress fromAddress = new MailAddress("it2@atravelmate.com", "Hệ thống MO Orientalsails");
            MailMessage message = new MailMessage();
            message.From = fromAddress;
            message.To.Add("it2@atravelmate.com");
            message.Subject = "4 double 1 deluxe";
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = "Dat Booking Thanh Cong";
            smtpClient.Send(message);
            return "working";
        }
    }

    namespace JsonModel
    {
        public class RoomClass
        {
            public int RoomClassId { get; set; }

            public string Name { get; set; }
        }

        public class RoomType
        {
            public int RoomTypeId { get; set; }

            public string Name { get; set; }
        }

        public class Cruise
        {
            public int CruiseId { get; set; }

            public string Name { get; set; }
        }

        public class Room
        {
            public int RoomId { get; set; }
        }

        public class GroupedRoom
        {
            public json.RoomClass JRoomClass { get; set; }

            public json.RoomType JRoomType { get; set; }

            public IList<json.Room> LJRoom { get; set; }
        }

        public class RoomInCruise
        {
            public json.Cruise JCruise { get; set; }

            public IList<json.GroupedRoom> LJAvaiableRoom { get; set; }

            public IList<json.GroupedRoom> LJPendingRoom { get; set; }
            public string AvaiableRoomQuantity { get; set; }

            public string AvaiableRoomDetail { get; set; }

            public string PendingRoomQuantity { get; set; }

            public string PendingRoomDetail { get; set; }

        }
    }
}