using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class AddSeriesBookingsBLL
    {
        public AgencyRepository AgencyRepository { get; set; }
        public AgencyContactRepository AgencyContactRepository { get; set; }
        public SeriesRepository SeriesRepository { get; set; }
        public TripRepository TripRepository { get; set; }
        public RoomClassRepository RoomClassRepository { get; set; }
        public RoomTypeRepository RoomTypeRepository { get; set; }
        public CruiseRepository CruiseRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }
        public BookingRoomRepository BookingRoomRepository { get; set; }
        public CustomerRepository CustomerRepository { get; set; }

        public AddSeriesBookingsBLL()
        {
            AgencyRepository = new AgencyRepository();
            AgencyContactRepository = new AgencyContactRepository();
            SeriesRepository = new SeriesRepository();
            TripRepository = new TripRepository();
            RoomClassRepository = new RoomClassRepository();
            RoomTypeRepository = new RoomTypeRepository();
            CruiseRepository = new CruiseRepository();
            BookingRepository = new BookingRepository();
            BookingRoomRepository = new BookingRoomRepository();
            CustomerRepository = new CustomerRepository();
        }

        public void Dispose()
        {
            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }

            if (AgencyContactRepository != null)
            {
                AgencyContactRepository.Dispose();
                AgencyContactRepository = null;
            }

            if (SeriesRepository != null)
            {
                SeriesRepository.Dispose();
                SeriesRepository = null;
            }

            if (TripRepository != null)
            {
                TripRepository.Dispose();
                TripRepository = null;
            }

            if (RoomClassRepository != null)
            {
                RoomClassRepository.Dispose();
                RoomClassRepository = null;
            }

            if (RoomTypeRepository != null)
            {
                RoomTypeRepository.Dispose();
                RoomTypeRepository = null;
            }

            if (CruiseRepository != null)
            {
                CruiseRepository.Dispose();
                CruiseRepository = null;
            }

            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }

            if (BookingRoomRepository != null)
            {
                BookingRoomRepository.Dispose();
                BookingRoomRepository = null;
            }

            if (CustomerRepository != null)
            {
                CustomerRepository.Dispose();
                CustomerRepository = null;
            }
        }

        public IList<AgencyContact> AgencyContactGetAllByAgency(int agencyId)
        {
            return AgencyContactRepository.AgencyContactGetAllByAgency(agencyId);
        }

        public Agency AgencyGetById(int agencyId)
        {
            return AgencyRepository.AgencyGetById(agencyId);
        }

        public AgencyContact AgencyContactGetById(int bookerId)
        {
            return AgencyContactRepository.AgencyContactGetById(bookerId);
        }

        public void SeriesSaveOrUpdate(Series series)
        {
            SeriesRepository.SaveOrUpdate(series);
        }

        public IList<SailsTrip> TripGetAll()
        {
            return TripRepository.TripGetAll();
        }

        public IList<RoomClass> RoomClassGetAll()
        {
            return RoomClassRepository.RoomClassGetAll();
        }

        public IList<RoomTypex> RoomTypeGetAll()
        {
            return RoomTypeRepository.RoomTypeGetAll();
        }

        public SailsTrip TripGetById(int tripId)
        {
            return TripRepository.TripGetById(tripId);
        }

        public IList<Cruise> CruiseGetAllByTrip(SailsTrip trip)
        {
            return CruiseRepository.CruiseGetAllByTrip(trip);
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return CruiseRepository.CruiseGetById(cruiseId);
        }

        public Series SeriesGetById(int seriesId)
        {
            return SeriesRepository.SeriesGetById(seriesId);
        }

        public void BookingSaveOrUpdate(Booking booking)
        {
            BookingRepository.SaveOrUpdate(booking);
        }

        public void BookingRoomSaveOrUpdate(BookingRoom bookingRoom)
        {
            BookingRoomRepository.SaveOrUpdate(bookingRoom);
        }

        public void CustomerSaveOrUpdate(Customer customer)
        {
            CustomerRepository.SaveOrUpdate(customer);
        }

        public RoomClass RoomClassGetById(int roomClassId)
        {
            return RoomClassRepository.RoomClassById(roomClassId);
        }

        public RoomTypex RoomTypeGetById(int roomTypeId)
        {
            return RoomTypeRepository.RoomTypeGetById(roomTypeId);
        }

        public Booking BookingGetById(int bookingId)
        {
            return BookingRepository.BookingGetById(bookingId);
        }
    }
}