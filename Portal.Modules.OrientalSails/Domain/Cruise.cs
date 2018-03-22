using System;
using System.Collections;
using CMS.Core.Domain;
using System.Collections.Generic;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Cruise
    {
        private IList<SailsTrip> trips;
        private string name;

        public virtual int Id { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual string Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }
        public virtual string Description { get; set; }
        public virtual string Image { get; set; }
        public virtual string Code { get; set; }
        public virtual string RoomPlan { get; set; }
        public virtual string CruiseCode { set; get; }
        public virtual IList Rooms { set; get; }
        public virtual IList<CruiseHaiPhongExpenseType> CruiseHaiPhongExpenseTypes { get; set; }

        public virtual IList<SailsTrip> Trips
        {
            get
            {
                if (trips == null)
                {
                    trips = new List<SailsTrip>();
                }
                return trips;
            }
            set { trips = value; }
        }

        public virtual string GetModifiedCruiseName()
        {
            if (name == "Oriental Sails")
            {
                return name + " " + "1";
            }
            return name;
        }
    }
}