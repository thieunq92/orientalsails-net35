using System;

namespace Portal.Modules.OrientalSails.Domain
{
    public class USDRate
    {
        public virtual int Id { get; set; }
        public virtual DateTime ValidFrom { get; set; }
        public virtual DateTime? ValidTo { get; set; }
        public virtual double Rate { get; set; }

    }
}
