using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Service;

namespace CMS.Web.UI
{
    public class KitGenericBasePage : PortalPage
    {
        #region properties


        /// <summary>
        /// The core repository for persisting Cuyahoga objects.
        /// </summary>
        public CoreRepository CoreRepository
        {
            get { return HttpContext.Current.Items["CoreRepository"] as CoreRepository; }
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
        }

        #endregion
    }
}
