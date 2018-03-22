using CMS.Core.Domain;
using CMS.Core.Service;
using CMS.Web.Components;
using CMS.Web.Util;
using Newtonsoft.Json;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AddSeriesBookingsWebMethod : System.Web.Services.WebService
    {
        private AddSeriesBookingsBLL addSeriesBookingsBLL;
        private UserUtil userUtil;

        public UserUtil UserUtil
        {
            get
            {
                if (userUtil == null)
                    userUtil = new UserUtil();
                return userUtil;
            }
        }

        public AddSeriesBookingsBLL AddSeriesBookingsBLL
        {
            get
            {
                if (addSeriesBookingsBLL == null)
                    addSeriesBookingsBLL = new AddSeriesBookingsBLL();
                return addSeriesBookingsBLL;
            }
        }
        public void ResourceDispose()
        {
            if (addSeriesBookingsBLL != null)
            {
                addSeriesBookingsBLL.Dispose();
                addSeriesBookingsBLL = null;
            }

            if (userUtil != null)
            {
                userUtil.Dispose();
                userUtil = null;
            }
        }

        [WebMethod]
        public string AgencyContactGetAllByAgency(string ai)
        {
            var agencyId = -1;
            try
            {
                agencyId = Convert.ToInt32(ai);
            }
            catch { }

            var listAgencyContactDTO = new List<AgencyContactDTO>();
            var listAgencyContact = AddSeriesBookingsBLL.AgencyContactGetAllByAgency(agencyId);
            foreach (var agencyContact in listAgencyContact)
            {
                var agencyContactDTO = new AgencyContactDTO()
                {
                    Id = agencyContact.Id,
                    Name = agencyContact.Name,
                };
                listAgencyContactDTO.Add(agencyContactDTO);
            }
            ResourceDispose();
            return JsonConvert.SerializeObject(listAgencyContactDTO);
        }

        [WebMethod]

        public string CreateSeries(string ai, string bi, string sc, string cd, string nod)
        {
            var agencyId = -1;
            try
            {
                agencyId = Convert.ToInt32(ai);
            }
            catch { }

            var agency = AddSeriesBookingsBLL.AgencyGetById(agencyId);

            var bookerId = -1;
            try
            {
                bookerId = Convert.ToInt32(bi);
            }
            catch { }

            var booker = AddSeriesBookingsBLL.AgencyContactGetById(bookerId);

            var seriesCode = sc;
            DateTime? cutoffDate = null;
            try
            {
                cutoffDate = DateTime.ParseExact(cd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }


            var noOfDays = -1;
            try
            {
                noOfDays = Convert.ToInt32(nod);
            }
            catch
            {
                noOfDays = 0;
            }

            var series = new Series()
            {
                SeriesCode = seriesCode,
                CutoffDate = cutoffDate,
                NoOfDays = noOfDays,
            };

            if (booker != null)
            {
                series.Booker = booker;
            }
            else
                if (agency != null)
                {
                    series.Agency = agency;
                }
            AddSeriesBookingsBLL.SeriesSaveOrUpdate(series);
            var seriesDTO = new SeriesDTO()
            {
                Id = series.Id,
                SeriesCode = series.SeriesCode,
                CutoffDate = series.CutoffDate.HasValue ? series.CutoffDate.Value.ToString("dd/MM/yyyy") : "",
                NoOfDays = series.NoOfDays
            };



            if (series.Booker != null)
            {
                seriesDTO.Booker = new SeriesDTO.BookerDTO()
                {
                    Id = series.Booker.Id,
                    Name = series.Booker.Name,
                };

                series.Agency = series.Booker.Agency;
            }

            if (series.Agency != null)
            {
                seriesDTO.Agency = new SeriesDTO.AgencyDTO()
                {
                    Id = series.Agency.Id,
                    Name = series.Agency.Name
                };
            }

            ResourceDispose();
            return JsonConvert.SerializeObject(seriesDTO);
        }

        [WebMethod]
        public string TripGetAll()
        {
            var listTrip = AddSeriesBookingsBLL.TripGetAll();
            var listTripDTO = new List<TripDTO>();
            foreach (var trip in listTrip)
            {
                var tripDTO = new TripDTO()
                {
                    Id = trip.Id,
                    Name = trip.Name
                };
                listTripDTO.Add(tripDTO);
            }
            ResourceDispose();
            return JsonConvert.SerializeObject(listTripDTO);
        }

        [WebMethod]
        public string CheckRoom(string sd, string ti, string SectionId, string NodeId)
        {
            CoreRepository CoreRepository = HttpContext.Current.Items["CoreRepository"] as CoreRepository;
            int nodeId = Int32.Parse(NodeId);
            Node node = (Node)CoreRepository.GetObjectById(typeof(Node), nodeId);
            int sectionId = Int32.Parse(SectionId);
            Section section = (Section)CoreRepository.GetObjectById(typeof(Section), sectionId);
            SailsModule module = (SailsModule)ContainerAccessorUtil.GetContainer().Resolve<ModuleLoader>().GetModuleFromSection(section);

            DateTime? startDate = null;
            try
            {
                startDate = DateTime.ParseExact(sd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var tripId = -1;
            try
            {
                tripId = Convert.ToInt32(ti);
            }
            catch { }

            var trip = AddSeriesBookingsBLL.TripGetById(tripId);
            var listRoomClass = AddSeriesBookingsBLL.RoomClassGetAll();
            var listRoomType = AddSeriesBookingsBLL.RoomTypeGetAll();
            var listCruise = AddSeriesBookingsBLL.CruiseGetAllByTrip(trip);

            var listCheckRoomResultDTO = new List<CheckRoomResultDTO>();
            foreach (var cruise in listCruise)
            {
                var checkRoomResultDTO = new CheckRoomResultDTO();
                checkRoomResultDTO.Cruise = new CheckRoomResultDTO.CruiseDTO()
                {
                    Id = cruise.Id,
                    Name = cruise.Name
                };

                int total = 0;
                string detail = "";
                foreach (var roomClass in listRoomClass)
                {
                    foreach (var roomType in listRoomType)
                    {

                        if (trip == null)
                            break;

                        if (!startDate.HasValue)
                            break;

                        int avail = module.RoomCount(roomClass, roomType, cruise, startDate.Value, trip.NumberOfDay, trip.HalfDay);

                        if (avail > 0)
                        {
                            total += avail;
                            detail += string.Format("{0} {2} {1} ", avail, roomType.Name, roomClass.Name);
                        }

                    }
                }
                checkRoomResultDTO.NoOfRoomAvaiable = total;
                checkRoomResultDTO.DetailRooms = detail;
                listCheckRoomResultDTO.Add(checkRoomResultDTO);
            }
            ResourceDispose();
            return JsonConvert.SerializeObject(listCheckRoomResultDTO);
        }


        [WebMethod]
        public string GetAvaiableRoom(string ci, string sd, string ti, string SectionId, string NodeId)
        {
            CoreRepository CoreRepository = HttpContext.Current.Items["CoreRepository"] as CoreRepository;
            int nodeId = Int32.Parse(NodeId);
            Node node = (Node)CoreRepository.GetObjectById(typeof(Node), nodeId);
            int sectionId = Int32.Parse(SectionId);
            Section section = (Section)CoreRepository.GetObjectById(typeof(Section), sectionId);
            SailsModule module = (SailsModule)ContainerAccessorUtil.GetContainer().Resolve<ModuleLoader>().GetModuleFromSection(section);

            DateTime? startDate = null;
            try
            {
                startDate = DateTime.ParseExact(sd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var tripId = -1;
            try
            {
                tripId = Convert.ToInt32(ti);
            }
            catch { }
            var trip = AddSeriesBookingsBLL.TripGetById(tripId);

            var cruiseId = -1;
            try
            {
                cruiseId = Convert.ToInt32(ci);
            }
            catch { }
            var cruise = AddSeriesBookingsBLL.CruiseGetById(cruiseId);

            var listRoomClass = AddSeriesBookingsBLL.RoomClassGetAll();
            var listRoomType = AddSeriesBookingsBLL.RoomTypeGetAll();

            var listAvaiableRoomDTO = new List<AvaiableRoomDTO>();
            foreach (var roomClass in listRoomClass)
            {
                foreach (var roomType in listRoomType)
                {

                    if (trip == null)
                        break;

                    if (!startDate.HasValue)
                        break;

                    var roomCount = module.RoomCount(roomClass, roomType, cruise, startDate.Value, trip.NumberOfDay, true,
                          trip.HalfDay);


                    var avaiableRoomDTO = new AvaiableRoomDTO();
                    if (roomCount > -1)
                    {
                        avaiableRoomDTO.KindOfRoom = roomClass.Name + " " + roomType.Name;
                        avaiableRoomDTO.RoomClass = new AvaiableRoomDTO.RoomClassDTO()
                        {
                            Id = roomClass.Id,
                            Name = roomClass.Name
                        };

                        avaiableRoomDTO.RoomType = new AvaiableRoomDTO.RoomTypeDTO()
                        {
                            Id = roomType.Id,
                            Name = roomType.Name,
                        };

                        var NoOfAdult = roomCount;
                        avaiableRoomDTO.NoOfAdult = NoOfAdult;

                        var NoOfChild = roomCount;
                        avaiableRoomDTO.NoOfChild = NoOfChild;

                        var NoOfBaby = roomCount;
                        avaiableRoomDTO.NoOfBaby = NoOfBaby;

                        listAvaiableRoomDTO.Add(avaiableRoomDTO);
                    }
                }
            }
            ResourceDispose();
            return JsonConvert.SerializeObject(listAvaiableRoomDTO);
        }

        [WebMethod]

        public string SaveBooking(IList<AvaiableRoomDTO> listAvaiableRoomDTO, string si, string sd, string ci, string ti)
        {

            var seriesId = -1;
            try
            {
                seriesId = Convert.ToInt32(si);
            }
            catch { }

            var series = AddSeriesBookingsBLL.SeriesGetById(seriesId);

            DateTime? startDate = null;
            try
            {
                startDate = DateTime.ParseExact(sd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var tripId = -1;
            try
            {
                tripId = Convert.ToInt32(ti);
            }
            catch { }
            var trip = AddSeriesBookingsBLL.TripGetById(tripId);

            var cruiseId = -1;
            try
            {
                cruiseId = Convert.ToInt32(ci);
            }
            catch { }
            var cruise = AddSeriesBookingsBLL.CruiseGetById(cruiseId);

            var booking = new Booking()
            {
                Agency = series.Booker != null ? series.Booker.Agency : series.Agency,
                Booker = series.Booker,
                StartDate = startDate.Value,
                Trip = trip,
                Cruise = cruise,
                CreatedBy = UserUtil.UserGetCurrent(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Sale = UserUtil.UserGetCurrent(),
                Status = Util.StatusType.Pending,
                Total = 0,
                Series = series,
                AgencyCode = series.SeriesCode,
            };
            booking.EndDate = booking.StartDate.AddDays(booking.Trip.NumberOfDay - 1);

            AddSeriesBookingsBLL.BookingSaveOrUpdate(booking);

            foreach (var avaiableRoomDTO in listAvaiableRoomDTO)
            {
                var selectedRoom = 0;
                try
                {
                    selectedRoom = Convert.ToInt32(avaiableRoomDTO.selectedRoom);
                }
                catch { }

                var selectedChild = 0;
                try
                {
                    selectedChild = Convert.ToInt32(avaiableRoomDTO.selectedChild);
                }
                catch { }

                var selectedBaby = 0;
                try
                {
                    selectedBaby = Convert.ToInt32(avaiableRoomDTO.selectedBaby);
                }
                catch { }

                var noOfBaby = selectedBaby;
                var noOfChild = selectedChild;
                for (int i = 0; i < selectedRoom; i++)
                {

                    var bookingRoom = new BookingRoom()
                    {
                        Book = booking,
                        Booked = 2,
                        HasChild = noOfBaby > 0 ? true : false,
                        HasBaby = noOfChild > 0 ? true : false,
                        RoomClass = AddSeriesBookingsBLL.RoomClassGetById(avaiableRoomDTO.RoomClass.Id),
                        RoomType = AddSeriesBookingsBLL.RoomTypeGetById(avaiableRoomDTO.RoomType.Id),
                    };
                    AddSeriesBookingsBLL.BookingRoomSaveOrUpdate(bookingRoom);
                    booking.BookingRooms.Add(bookingRoom);
                    noOfChild--;
                    noOfBaby--;

                    for (int j = 0; j < bookingRoom.Booked; j++)
                    {
                        var customer = new Customer()
                        {
                            BookingRoom = bookingRoom,
                            Type = Util.CustomerType.Adult,
                        };
                        AddSeriesBookingsBLL.CustomerSaveOrUpdate(customer);
                        bookingRoom.Customers.Add(customer);
                    }

                    if (bookingRoom.HasChild)
                    {
                        var customer = new Customer()
                        {
                            BookingRoom = bookingRoom,
                            Type = Util.CustomerType.Children,
                        };
                        AddSeriesBookingsBLL.CustomerSaveOrUpdate(customer);
                        bookingRoom.Customers.Add(customer);
                    }

                    if (bookingRoom.HasBaby)
                    {
                        var customer = new Customer()
                        {
                            BookingRoom = bookingRoom,
                            Type = Util.CustomerType.Baby,
                        };
                        AddSeriesBookingsBLL.CustomerSaveOrUpdate(customer);
                        bookingRoom.Customers.Add(customer);
                    }
                }
            }

            booking = AddSeriesBookingsBLL.BookingGetById(booking.Id);

            var bookingDTO = new BookingDTO()
            {
                BookingId = "OS" + booking.Id.ToString(),
                StartDate = booking.StartDate.ToString("dd/MM/yyyy"),
                TripName = booking.Trip.Name,
                CruiseName = booking.Cruise.Name,
                NoOfRoom = booking.BookingRooms.Count,
                NoOfPax = booking.Pax,
                Pax = string.Format("Adults : {0}</br> Childs : {1}<br/> Baby : {2}", booking.Adult, booking.Child, booking.Baby),
                Room = booking.RoomName,         
            };
            ResourceDispose();
            return JsonConvert.SerializeObject(bookingDTO);
        }
    }
}
