using System;
using System.Globalization;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class AgencyContactEdit : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["contactid"]!=null)
                {
                    AgencyContact contact = Module.ContactGetById(Convert.ToInt32(Request.QueryString["contactid"]));
                    txtName.Text = contact.Name;
                    txtPhone.Text = contact.Phone;
                    txtEmail.Text = contact.Email;
                    txtPosition.Text = contact.Position;
                    chkBooker.Checked = contact.IsBooker;
                    if (contact.Birthday != null) txtBirthday.Text = contact.Birthday.Value.ToString("dd/MM/yyyy");
                    txtNote.Text = contact.Note;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AgencyContact contact;
            if (Request.QueryString["contactid"] != null)
            {
                contact = Module.ContactGetById(Convert.ToInt32(Request.QueryString["contactid"]));
            }
            else
            {
                contact = new AgencyContact();
                Agency agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["agencyid"]));
                contact.Agency = agency;
            }
            contact.Name = txtName.Text;
            contact.Phone = txtPhone.Text;
            contact.Email = txtEmail.Text;
            contact.Position = txtPosition.Text;
            contact.IsBooker = chkBooker.Checked;
            try
            {
                if (!String.IsNullOrEmpty(txtBirthday.Text))
                    contact.Birthday = DateTime.ParseExact(txtBirthday.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                else
                    contact.Birthday = null;
            }
            catch (System.FormatException fe)
            {
                contact.Birthday = null;
            }
            Module.SaveOrUpdate(contact);
            Page.ClientScript.RegisterStartupScript(typeof(AgencyContactEdit), "close", "window.opener.location = window.opener.location; window.close();", true);
        }
    }
}
