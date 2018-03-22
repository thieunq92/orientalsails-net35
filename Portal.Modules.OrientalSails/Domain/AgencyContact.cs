using System;
using System.Collections.Generic;

namespace Portal.Modules.OrientalSails.Domain
{
    public class AgencyContact
    {
        public AgencyContact()
        {
            Id = -1;
            Enabled = true;
        }
        public virtual int Id { get; set; }
        public virtual bool IsBooker { get; set; }
        public virtual string Name { get; set; }
        public virtual string Position { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual Agency Agency { get; set; }

        public virtual int AgencyId
        {
            get
            {
                if (Agency != null)
                {
                    return Agency.Id;
                }
                return -1;
            }
        }

        public virtual DateTime? Birthday { set; get; }
        public virtual String Note { set; get; }
        public virtual IList<Series> ListSeries { get; set; }
    }
}
