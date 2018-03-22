using System;
using System.Collections;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class CruiseRole
    {
        public virtual int Id { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual Role Role { get; set; }
        public virtual Agency Agency { get; set; }
    }
}
