using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class AgencyUser
    {
        public virtual int Id { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual User User { get; set; }
    }
}
