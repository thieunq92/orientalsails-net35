using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class CruiseCostType
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual Cruise Cruise
        {
            get;
            set;
        }

        public virtual CostType CostType
        {
            get;
            set;
        }
    }
}