using Portal.Modules.OrientalSails.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    /// <summary>
    /// Summary description for AddSerialBookingsWebMethod
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AddSerialBookingsWebMethod : System.Web.Services.WebService
    {
        private AddSerialBookingsBLL addSerialBookingsBLL;

        public AddSerialBookingsBLL AddSerialBookingsBLL
        {
            get
            {
                if (addSerialBookingsBLL == null)
                    addSerialBookingsBLL = new AddSerialBookingsBLL();
                return addSerialBookingsBLL;
            }
        }
        public void ResourceDispose()
        {
            if (addSerialBookingsBLL != null)
            {
                addSerialBookingsBLL.Dispose();
                addSerialBookingsBLL = null;
            }
        }

        [WebMethod]
        public string GetData(int ai)
        {
            return "aaaaaaaaaaaa";
        }
    }
}
