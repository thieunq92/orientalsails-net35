using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class EditMeeting : SailsAdminBasePage
    {
        protected AgencyContact Contact = new AgencyContact();
        protected Activity Activity = new Activity();
        /// <summary>
        /// sự kiện khi tải trang load tất cả dữ liệu meeting vào các control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["contact"]))
                {
                    Contact = Module.ContactGetById(Convert.ToInt32(Request.QueryString["contact"]));
                    txtDateMeeting.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                if (!String.IsNullOrEmpty(Request.QueryString["Activity"]))
                {
                    Activity = Module.GetObject<Activity>(Convert.ToInt32(Request.QueryString["activity"]));
                    Contact = Module.GetObject<AgencyContact>(Activity.ObjectId);
                    txtDateMeeting.Text = Activity.DateMeeting.ToString("dd/MM/yyyy");
                }
                litContact.Text = Contact.Name;
                litPosition.Text = Contact.Position;
                txtNote.Text = Activity.Note;
            }
        }

        /// <summary>
        /// sự kiện của nút save khi click sẽ lưu dữ liệu meeting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["contact"]))
            {
                Contact = Module.ContactGetById(Convert.ToInt32(Request.QueryString["contact"]));
            }
            if (!String.IsNullOrEmpty(Request.QueryString["Activity"]))
            {
                Activity = Module.GetObject<Activity>(Convert.ToInt32(Request.QueryString["activity"]));
                Contact = Module.GetObject<AgencyContact>(Activity.ObjectId);
            }
            Activity.ObjectId = Contact.Id;
            Activity.ObjectType = "MEETING";
            Activity.Note = txtNote.Text;
            Activity.Level = ImportantLevel.Important;
            Activity.User = UserIdentity;
            Activity.Time = DateTime.Now;
            Activity.Params = Contact.Agency.Id.ToString();
            Activity.Url = string.Format("AgencyView.aspx?NodeId={0}&SectionId={1}&agencyid={2}", Node.Id,
                                         Section.Id, Contact.Agency.Id);
            Activity.DateMeeting = DateTime.ParseExact(Request.Params[txtDateMeeting.UniqueID], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Activity.UpdateTime = DateTime.Now;
            Module.SaveOrUpdate(Activity);
            Page.ClientScript.RegisterStartupScript(typeof(EditMeeting), "close", "window.opener.location = window.opener.location; window.close();", true);

        }
    }
}