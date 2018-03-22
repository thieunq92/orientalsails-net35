using System;
using System.Globalization;
using System.Web.UI.WebControls;
using CMS.ServerControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class CustomerList : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pagerCustomers.AllowCustomPaging = true;
            if (!IsPostBack)
            {
                LoadQueryString();
                GetDataSource();
            }
        }

        protected void LoadQueryString()
        {
            if (Request.QueryString["name"]!=null)
            {
                txtFullName.Text = Request.QueryString["name"];
            }
            if (Request.QueryString["birth"] != null)
            {
                DateTime date = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["birth"]));
                txtBirthdate.Text = date.ToString("dd/MM/yyyy");
            }
            if (Request.QueryString["passport"] != null)
            {
                txtPassport.Text = Request.QueryString["passport"];
            }

            if (Request.QueryString["bookingid"] != null)
            {
                txtBookingId.Text = Request.QueryString["bookingid"];
                string stayUrl = string.Format("StayDetail.aspx?NodeId={0}&SectionId={1}&bookingid={2}", Node.Id, Section.Id,
                           Request.QueryString["bookingid"]);
                btnProvisionalDetail.Attributes.Add("onclick", string.Format("window.location='{0}';", stayUrl));
                btnProvisionalDetail.Value = Resources.textProvisionalDetail;
                btnProvisionalDetail.Visible = true;
            }
            if (Request.QueryString["bookingcode"] != null)
            {
                txtBookingId.Text = Request.QueryString["bookingcode"];
                Booking bk = Module.BookingGetByCode(Convert.ToInt32(Request.QueryString["bookingcode"]));
                string stayUrl = string.Format("StayDetail.aspx?NodeId={0}&SectionId={1}&bookingid={2}", Node.Id, Section.Id,
                           bk.Id);
                btnProvisionalDetail.Attributes.Add("onclick", string.Format("window.location='{0}';", stayUrl));
                btnProvisionalDetail.Value = Resources.textProvisionalDetail;
                btnProvisionalDetail.Visible = true;
            }

            if (Request.QueryString["gender"] != null)
            {
                ddlGender.SelectedIndex = Convert.ToInt32(Request.QueryString["gender"]);
            }
            if (Request.QueryString["nationality"] != null)
            {
                txtNationality.Text = Request.QueryString["nationality"];
            }
        }

        protected void GetDataSource()
        {
            int count;
            rptCustomers.DataSource = Module.CustomerGetDistinctByQueryString(Request.QueryString, pagerCustomers.PageSize, pagerCustomers.CurrentPageIndex, out count);
            pagerCustomers.VirtualItemCount = count;
            rptCustomers.DataBind();
        }

        protected void rptCustomers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Customer)
            {
                Customer customer = (Customer) e.Item.DataItem;

                Literal litBirthdate = e.Item.FindControl("litBirthdate") as Literal;
                if (litBirthdate!=null)
                {
                    if (customer.Birthday.HasValue)
                    {
                        litBirthdate.Text = customer.Birthday.Value.ToString("dd/MM/yyyy");
                    }
                }

                Literal litGender = e.Item.FindControl("litGender") as Literal;
                if (litGender!=null)
                {
                    if (customer.IsMale.HasValue)
                    {
                        if (customer.IsMale.Value)
                        {
                            litGender.Text = "Male";
                        }
                        else
                        {
                            litGender.Text = "Female";
                        }
                    }
                }

                Literal litNationality = e.Item.FindControl("litNationality") as Literal;
                if (litNationality!=null)
                {
                    litNationality.Text = customer.Country;
                }

                Literal litPassport = e.Item.FindControl("litPassport") as Literal;
                if (litPassport!=null)
                {
                    litPassport.Text = customer.Passport;
                }
            }
        }

        protected void pagerCustomers_PageChanged(object sender, PageChangedEventArgs e)
        {
            GetDataSource();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string query = string.Empty;
            if (!string.IsNullOrEmpty(txtFullName.Text))
            {
                query += "&name=" + txtFullName.Text;
            }
            if (!string.IsNullOrEmpty(txtBirthdate.Text))
            {
                DateTime date = DateTime.ParseExact(txtBirthdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                query += "&birth=" + date.ToOADate();
            }
            if (!string.IsNullOrEmpty(txtPassport.Text))
            {
                query += "&passport=" + txtPassport.Text;
            }
            if (!string.IsNullOrEmpty(txtBookingId.Text))
            {
                if (UseCustomBookingId)
                {
                    query += "&bookingcode=" + txtBookingId.Text;
                }
                else
                {
                    query += "&bookingid=" + txtBookingId.Text;
                }
            }
            if (!string.IsNullOrEmpty(txtNationality.Text))
            {
                query += "&nationality=" + txtNationality.Text;
            }
            if (ddlGender.SelectedIndex > 0)
            {
                query += "&gender=" + ddlGender.SelectedIndex;
            }
            PageRedirect(string.Format("CustomerList.aspx?NodeId={0}&SectionId={1}{2}", Node.Id, Section.Id, query));
        }
    }
}
