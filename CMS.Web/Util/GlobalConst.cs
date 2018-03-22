using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CMS.Core.Util;

namespace CMS.Web.Util
{
    public class GlobalConst
    {
        public const string NO_IMAGE = "/Images/no_image.gif";
        public const string DELETED_USER = "Deleted user";

        //public static Functionality FUNCTIONALITY
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(Config.GetConfiguration()["Functionality"]))
        //        {
        //            return Functionality.Basic;
        //        }
        //        string function = Config.GetConfiguration()["Functionality"];
        //        return (Functionality)Enum.Parse(typeof(Functionality), function);
        //    }
        //}
    }
}
