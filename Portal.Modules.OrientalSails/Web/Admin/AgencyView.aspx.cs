using System;
using System.Text;
using System.Web.Services.Discovery;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class AgencyView : SailsAdminBasePage
    {
        private bool _editPermission;
        private bool _viewBookingPermission;
        private bool _contactsPermission;
        private bool _recentActivitiesPermission;
        private bool _contractsPermission;

        protected void Page_Load(object sender, EventArgs e)
        {                  
            if (!IsPostBack)
            {
                _editPermission = Module.PermissionCheck(Permission.ACTION_EDITAGENCY, UserIdentity);
                _viewBookingPermission = Module.PermissionCheck("VIEWBOOKINGBYAGENCY", UserIdentity);
                _contactsPermission = Module.PermissionCheck("CONTACTS", UserIdentity);
                _recentActivitiesPermission = Module.PermissionCheck("RECENTACTIVITIES",UserIdentity);
                _contractsPermission = Module.PermissionCheck("CONTRACTS",UserIdentity);

                if (Request.QueryString["agencyid"] != null)
                {
                    var agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["agencyid"]));

                    if (agency.Sale == UserIdentity)
                    {
                        _editPermission = true;
                    }
                    litName1.Text = agency.Name;
                    litName.Text = agency.Name;
                    if (agency.Role != null)
                        litRole.Text = agency.Role.Name;
                    else
                    {
                        litRole.Text = "Customize Role";
                    }
                    if (agency.Sale != null)
                    {
                        litSale.Text = agency.Sale.AllName;
                        litSalePhone.Text = agency.Sale.Website;
                    }
                    else
                        litSale.Text = @"Unbound";
                    litTax.Text = agency.TaxCode;
                    if (agency.Location != null)
                        litLocation.Text = agency.Location.Name;
                    litAddress.Text = agency.Address;

                    if (!string.IsNullOrEmpty(agency.Email))
                    {
                        hplEmail.NavigateUrl = string.Format("mailto:{0}", agency.Email);
                        hplEmail.Text = agency.Email;
                    }
                    litPhone.Text = agency.Phone;
                    litFax.Text = agency.Fax;

                    litAccountant.Text = agency.Accountant;
                    litPayment.Text = agency.PaymentPeriod.ToString();

                    litNote.Text = agency.Description;

                    rptContracts.DataSource = Module.ContractGetByAgency(agency);
                    rptContracts.DataBind();

                    rptContacts.DataSource = Module.ContactGetByAgency(agency, !_editPermission); // nếu không có quyền edit thì ko có quyền view
                    rptContacts.DataBind();

                    hplEditAgency.Visible = _editPermission;
                    hplEditAgency.NavigateUrl = string.Format("AgencyEdit.aspx?NodeId={0}&SectionId={1}&agencyid={2}", Node.Id,
                                                        Section.Id, agency.Id);

                    hplAddContact.Visible = _editPermission;
                    hplAddContact.NavigateUrl = "javascript:";
                    string url = string.Format("AgencyContactEdit.aspx?NodeId={0}&SectionId={1}&agencyid={2}",
                                                    Node.Id, Section.Id, agency.Id);
                    hplAddContact.Attributes.Add("onclick", CMS.ServerControls.Popup.OpenPopupScript(url, "Contact", 300, 400));

                    url = string.Format("AgencyContractEdit.aspx?NodeId={0}&SectionId={1}&agencyid={2}",
                                                    Node.Id, Section.Id, agency.Id);
                    hplAddContract.Visible = _editPermission;
                    hplAddContract.NavigateUrl = "javascript:";
                    hplAddContract.Attributes.Add("onclick", CMS.ServerControls.Popup.OpenPopupScript(url, "Contract", 300, 400));

                    hplBookingList.NavigateUrl = string.Format(
                        "BookingList.aspx?NodeId={0}&SectionId={1}&agencyid={2}", Node.Id, Section.Id, agency.Id);
                    hplReceivable.NavigateUrl =
                        string.Format("PaymentReport.aspx?NodeId={0}&SectionId={1}&agencyid={2}&from={3}&to={4}",
                                      Node.Id, Section.Id, agency.Id, DateTime.Today.AddMonths(-3).ToOADate(), DateTime.Today.ToOADate());

                    rptActivities.DataSource = Module.GetObject<Activity>(Expression.And(Expression.Eq("ObjectType", "MEETING"),Expression.Eq("Params",Convert.ToString(agency.Id))), 0, 0,
                                                                          Order.Desc("DateMeeting"));
                    rptActivities.DataBind();
                }
            }
            RenderViewBookingByThisAgency();
            RenderContacts();
            RenderRecentActivities();
            RenderContracts();
        }

        public void RenderViewBookingByThisAgency()
        {
            if (!_viewBookingPermission)
            {
                hplBookingList.CssClass = hplBookingList.CssClass + " " + "disable";
                hplBookingList.Attributes["href"] = "javascript:";
                var script = @"<script type = 'text/javascript'>";
                script = script +
                         @"$('#"+hplBookingList.ClientID+"').click(function(){$('#disableInform').dialog({resiable:false,modal:true,draggable:false})})";
                script = script + "</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(),"disableInform",script);
            }
        }

        public void RenderContacts()
        {
            if (!_contactsPermission)
            {
                plhContacts.Visible = false;
                lblContacts.Visible = true;
            }
        }

        public void RenderRecentActivities()
        {
            if (!_recentActivitiesPermission)
            {
                plhActivities.Visible = false;
                lblActivities.Visible = true;
            }
        }

        public void RenderContracts()
        {
            if (!_contractsPermission)
            {
                plhContracts.Visible = false;
                lblContracts.Visible = true;
            }
        }

        protected void rptContracts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is AgencyContract)
            {
                var contract = (AgencyContract)e.Item.DataItem;

                ValueBinder.BindLiteral(e.Item, "litName", contract.ContractName);
                ValueBinder.BindLiteral(e.Item, "litExpired", contract.ExpiredDate.ToString("dd/MM/yyyy"));
                if(contract.CreateDate != null)
                    ValueBinder.BindLiteral(e.Item, "litCreatedDate", contract.CreateDate.Value.ToString("dd/MM/yyyy"));

                if (contract.Received == true)
                {
                    ValueBinder.BindLiteral(e.Item, "litReceived", "Yes");
                }
                else
                {
                    ValueBinder.BindLiteral(e.Item, "litReceived", "No");
                }

                var hplDownload = (HyperLink)e.Item.FindControl("hplDownload");
                hplDownload.NavigateUrl = contract.FilePath;
                //hplDownload.NavigateUrl = contract.FileName;
                hplDownload.Text = contract.FileName;

                var linkEdit = (HyperLink)e.Item.FindControl("hplEdit");

                var url = string.Format("AgencyContractEdit.aspx?NodeId={0}&SectionId={1}&contractid={2}",
                                Node.Id, Section.Id, contract.Id);
                linkEdit.NavigateUrl = "javascript:";
                linkEdit.Attributes.Add("onclick", CMS.ServerControls.Popup.OpenPopupScript(url, "Contract", 300, 400));
                linkEdit.Visible = _editPermission;
            }
        }

        protected void rptContacts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is AgencyContact)
            {
                var contact = (AgencyContact)e.Item.DataItem;

                if (!contact.Enabled)
                {
                    e.Item.Visible = false;
                    return;
                }         

                var ltrName = (Literal) e.Item.FindControl("ltrName");
                ltrName.Text = contact.Name;

                var hplName = (HyperLink) e.Item.FindControl("hplName");
                hplName.NavigateUrl = "javascript:";

                if (_editPermission)
                {
                    string url = string.Format("AgencyContactEdit.aspx?NodeId={0}&SectionId={1}&contactid={2}",
                                               Node.Id, Section.Id, contact.Id);
                    hplName.Attributes.Add("onclick", CMS.ServerControls.Popup.OpenPopupScript(url, "Contact", 300, 400));
                }

                var linkEmail = (HyperLink)e.Item.FindControl("hplEmail");
                linkEmail.Text = contact.Email;
                linkEmail.NavigateUrl = string.Format("mailto:{0}", contact.Email);

                ValueBinder.BindLiteral(e.Item, "litPosition", contact.Position);
                ValueBinder.BindLiteral(e.Item, "litPhone", contact.Phone);

                if (contact.IsBooker)
                {
                    ValueBinder.BindLiteral(e.Item, "litBooker", "Booker");
                }

                var lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
                lbtDelete.Visible = _editPermission;
                lbtDelete.CommandArgument = contact.Id.ToString();

                {
                    var hplCreateMeeting = (HyperLink)e.Item.FindControl("hplCreateMeeting");
                    hplCreateMeeting.NavigateUrl = "javascript:";
                    string url = string.Format("EditMeeting.aspx?NodeId={0}&SectionId={1}&contact={2}",
                                               Node.Id, Section.Id, contact.Id);
                    hplCreateMeeting.Attributes.Add("onclick",
                                                 CMS.ServerControls.Popup.OpenPopupScript(url, "Contract", 300, 400));
                }
            }
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            var btn = (IButtonControl)sender;
            var contact = Module.ContactGetById(Convert.ToInt32(btn.CommandArgument));

            contact.Enabled = false;
            Module.SaveOrUpdate(contact);

            PageRedirect(Request.RawUrl);
        }

        protected void rptActivities_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var activity = e.Item.DataItem as Activity;
            var name = e.Item.FindControl("ltrName") as Literal;
            var position = e.Item.FindControl("ltrPosition") as Literal;
            var dateMeeting = e.Item.FindControl("ltrDateMeeting") as Literal;
            if (activity != null)
            { 
                if (dateMeeting != null) dateMeeting.Text = activity.DateMeeting.ToString("dd/MM/yyyy");
                if (name != null) name.Text = Module.GetObject<AgencyContact>(activity.ObjectId).Name;
                if (position != null) position.Text = Module.GetObject<AgencyContact>(activity.ObjectId).Position;

                var note = e.Item.FindControl("ltrNote") as Literal;
                var strBuilder = new StringBuilder();
                string[] noteWord = activity.Note.Split(new char[]{' '});
                bool isLessWords = false;
                for (int i = 0; i <= 50; i++)
                {
                    try
                    {
                        strBuilder.Append(noteWord[i] + " ");
                    }
                    catch (IndexOutOfRangeException ex)
                    {               
                        isLessWords = true;
                        break;
                    }
                }
                if (!isLessWords) strBuilder.Append("...");
                if (note != null) note.Text = strBuilder.ToString();

                var lbtEditActivity = (LinkButton)e.Item.FindControl("lbtEditActivity");

                var ltrSale = (Literal) e.Item.FindControl("ltrSale");
                ltrSale.Text = activity.User.FullName;

                lbtEditActivity.PostBackUrl = "javascript:";
                string url = string.Format("EditMeeting.aspx?NodeId={0}&SectionId={1}&activity={2}",
                                           Node.Id, Section.Id, activity.Id);
                lbtEditActivity.Attributes.Add("onclick",
                                             CMS.ServerControls.Popup.OpenPopupScript(url, "Contract", 300, 400));
            }  
        }

        protected void lbtDeleteActivity_Click(object sender, EventArgs e)
        {
            var btn = (IButtonControl)sender;
            Activity activity = Module.GetObject<Activity>(Convert.ToInt32(btn.CommandArgument));
            Module.Delete(activity);
            PageRedirect(Request.RawUrl);
        }
    }

}