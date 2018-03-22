using System;
using System.Collections;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class SailsTrip
    {
        private string image;
        private string name;
        private IList triposBookings;
        private IList triposSailsPriceConfigs;

        public virtual int Id { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual int NumberOfDay { get; set; }
        public virtual int HalfDay { get; set; }
        public virtual string WhatToTake { get; set; }
        public virtual string Itinerary { get; set; }
        public virtual string Inclusions { get; set; }
        public virtual string Exclusions { get; set; }
        public virtual string Description { get; set; }
        public virtual int NumberOfOptions { get; set; }
        public virtual string TripCode { get; set; }
        public virtual string Name
        {
            get { return name; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                name = value;
            }
        }

        public virtual string Image
        {
            get { return image; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Image", value, value.ToString());
                image = value;
            }
        }


        public virtual IList Bookings
        {
            get
            {
                if (triposBookings == null)
                {
                    triposBookings = new ArrayList();
                }
                return triposBookings;
            }
            set { triposBookings = value; }
        }

        public virtual IList SailsPriceConfigs
        {
            get
            {
                if (triposSailsPriceConfigs == null)
                {
                    triposSailsPriceConfigs = new ArrayList();
                }
                return triposSailsPriceConfigs;
            }
            set { triposSailsPriceConfigs = value; }
        }

    }
}