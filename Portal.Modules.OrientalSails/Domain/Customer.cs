using System;
using System.Collections;
using Portal.Modules.OrientalSails.Web.Util;
using System.Collections.Generic;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Customer
    {
        private string fullname;
        private string passport;
        private string country;
        private IList bookingRooms;
        private IList customerosCustomerExtraOptions;

        public virtual int Id { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual BookingRoom BookingRoom { get; set; }
        public virtual CustomerType Type { get; set; }
        public virtual bool? IsMale { get; set; }
        public virtual string VisaNo { get; set; }
        public virtual DateTime? VisaExpired { get; set; }
        public virtual bool IsChild { get; set; }
        public virtual bool IsVietKieu { get; set; }
        public virtual string Purpose { get; set; }
        public virtual DateTime? StayFrom { get; set; }
        public virtual DateTime? StayTo { get; set; }
        public virtual string StayTerm { get; set; }
        public virtual string StayIn { get; set; }
        public virtual string Code { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Purpose StayPurpose { get; set; }
        public virtual double Total { get; set; }
        public virtual string NguyenQuan { set; get; }
        public virtual DateTime? Birthday { get; set; }

        public virtual string Fullname
        {
            get { return fullname; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Fullname", value, value);
                fullname = value;
            }
        }

        public virtual string Passport
        {
            get { return passport; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Passport", value, value);
                passport = value;
            }
        }

        public virtual string Country
        {
            get { return country; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Country", value, value);
                country = value;
            }
        }

        public virtual IList BookingRooms
        {
            get
            {
                if (bookingRooms == null)
                {
                    bookingRooms = new ArrayList();
                }
                return bookingRooms;
            }
            set { bookingRooms = value; }
        }

        public virtual IList CustomerExtraOptions
        {
            get
            {
                if (customerosCustomerExtraOptions == null)
                {
                    customerosCustomerExtraOptions = new ArrayList();
                }
                return customerosCustomerExtraOptions;
            }
            set { customerosCustomerExtraOptions = value; }
        }

    }
}