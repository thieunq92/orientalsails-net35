using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Portal.Modules.OrientalSails.Web.Ajax
{
    public partial class AjaxHandler : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod]
        public static string SaveScreenCapture(string imgBase64){
            byte[] bytes = Convert.FromBase64String(imgBase64);

            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(ms);
            }

            return "good";
        }
    }
}