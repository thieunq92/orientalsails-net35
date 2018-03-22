
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Domain
{
    public class CruiseTrip
    {
        public virtual int Id { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual SailsTrip Trip { get; set; }
    }
}
