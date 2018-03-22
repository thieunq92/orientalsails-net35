using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class SailsMasterBLL
    {
        public AgencyContactRepository AgencyContactRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }
        public UserRepository UserRepository { get; set; }

        public SailsMasterBLL()
        {
            AgencyContactRepository = new AgencyContactRepository();
            BookingRepository = new BookingRepository();
            UserRepository = new UserRepository();
        }

        public void Dispose()
        {
            if (AgencyContactRepository != null)
            {
                AgencyContactRepository.Dispose();
                AgencyContactRepository = null;
            }

            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }

            if (UserRepository != null)
            {
                UserRepository.Dispose();
                UserRepository = null;
            }
        }

        public int AgencyContactBirthdayCount()
        {
            return AgencyContactRepository.AgencyContactBirthdayCount();
        }

        public int MyBookingPendingCount()
        {
            return BookingRepository.MyBookingPendingCount();
        }

        public int MyTodayBookingPendingCount()
        {
            return BookingRepository.MyTodayBookingPendingCount();
        }

        public int SystemBookingPendingCount()
        {
            return BookingRepository.SystemBookingPendingCount();
        }

        public IList<AgencyContact> AgencyContactGetAllByBirthday()
        {
            return AgencyContactRepository.AgencyContactGetAllByBirthday();
        }

        public string UserGetName(int userId)
        {
            return UserRepository.UserGetName(userId);
        }
    }
}