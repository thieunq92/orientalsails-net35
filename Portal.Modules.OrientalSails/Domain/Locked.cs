using System;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Locked
    {
        public virtual int Id { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual string Description { get; set; }
        public virtual Booking Charter { get; set; }
        public virtual Cruise Cruise { get; set; }
    }

}