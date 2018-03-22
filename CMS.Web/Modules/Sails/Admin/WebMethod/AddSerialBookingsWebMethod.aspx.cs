using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    public partial class AddSerialBookingsWebMethod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [System.Web.Services.WebMethod]
        public static string GetData()
        {
            return HttpUtility.HtmlEncode("<b>aaaaa</b>");
        }
    }
}