using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class HaiPhongContract
    {
        public virtual int HaiPhongContractId { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime ApplyFrom { get; set; }
    }
}