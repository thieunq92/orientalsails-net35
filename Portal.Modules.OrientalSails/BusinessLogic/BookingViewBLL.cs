using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using Portal.Modules.OrientalSails.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class BookingViewBLL
    {
        public BookingRepository BookingRepository { get; set; }

        public TripRepository TripRepository { get; set; }

        public CruiseRepository CruiseRepository { get; set; }

        public AgencyRepository AgencyRepository { get; set; }

        public BookingRoomRepository BookingRoomRepository { get; set; }

        public BookingHistoryRepository BookingHistoryRepository { get; set; }

        public LockedRepository LockedRepository { get; set; }

        public CustomerRepository CustomerRepository { get; set; }

        public UserUtil UserUtil { get; set; }

        public BookingViewBLL()
        {
            BookingRepository = new BookingRepository();
            TripRepository = new TripRepository();
            CruiseRepository = new CruiseRepository();
            AgencyRepository = new AgencyRepository();
            BookingRoomRepository = new BookingRoomRepository();
            BookingHistoryRepository = new BookingHistoryRepository();
            LockedRepository = new LockedRepository();
            CustomerRepository = new CustomerRepository();
            UserUtil = new UserUtil();
        }

        public void Dispose()
        {
            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }
            if (TripRepository != null)
            {
                TripRepository.Dispose();
                TripRepository = null;
            }
            if (CruiseRepository != null)
            {
                CruiseRepository.Dispose();
                CruiseRepository = null;
            }
            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }
            if (BookingRoomRepository != null)
            {
                BookingRoomRepository.Dispose();
                BookingRoomRepository = null;
            }
            if (BookingHistoryRepository != null)
            {
                BookingHistoryRepository.Dispose();
                BookingHistoryRepository = null;
            }
            if (LockedRepository != null)
            {
                LockedRepository.Dispose();
                LockedRepository = null;
            }
            if (CustomerRepository != null)
            {
                CustomerRepository.Dispose();
                CustomerRepository = null;
            }
            if (UserUtil != null)
            {
                UserUtil.Dispose();
                UserUtil = null;
            }
        }

        public Booking BookingGetById(int bookingId)
        {
            return BookingRepository.BookingGetById(bookingId);
        }

        public IList<SailsTrip> TripGetAll()
        {
            return TripRepository.TripGetAll();
        }

        public IList<Cruise> CruiseGetAll()
        {
            return CruiseRepository.CruiseGetAll();
        }

        public IList<Agency> AgencyGetAll()
        {
            return AgencyRepository.AgencyGetAll();
        }

        public void BookingSaveOrUpdate(Booking Booking)
        {
            if (Booking.Id > 0)
            {
                Booking.ModifiedBy = UserUtil.UserGetCurrent();
                Booking.ModifiedDate = DateTime.Now;
            }
            else
            {
                Booking.CreatedBy = UserUtil.UserGetCurrent();
                Booking.CreatedDate = DateTime.Now;
            }
            BookingRepository.SaveOrUpdate(Booking);
        }

        public void BookingRoomSaveOrUpdate(BookingRoom bookingRoom)
        {
            BookingRoomRepository.SaveOrUpdate(bookingRoom);
        }

        public BookingRoom BookingRoomGetById(int bookingRoomId)
        {
            return BookingRoomRepository.BookingRoomGetById(bookingRoomId);
        }

        public void BookingRoomDelete(BookingRoom bookingRoom)
        {
            BookingRoomRepository.Delete(bookingRoom);
        }

        public IList<BookingHistory> BookingHistoryGetByBookingId(int bookingId)
        {
            return BookingHistoryRepository.BookingHistoryGetByBookingId(bookingId);
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return CruiseRepository.CruiseGeById(cruiseId);
        }

        public SailsTrip TripGetById(int tripId)
        {
            return TripRepository.TripGetById(tripId);
        }

        public IList<Locked> LockedGetBy(DateTime? startDate, DateTime? endDate, int cruiseId)
        {
            return LockedRepository.LockedGetBy(startDate, endDate, cruiseId);
        }

        public void CustomerSaveOrUpdate(Customer customer)
        {
            CustomerRepository.CustomerSaveOrUpdate(customer);
        }
    }
}