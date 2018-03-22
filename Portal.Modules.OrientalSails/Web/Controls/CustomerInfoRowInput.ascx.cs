using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;

namespace Portal.Modules.OrientalSails.Web.Controls
{
    public partial class CustomerInfoRowInput : System.Web.UI.UserControl
    {
        private bool _childAllowed;
        public bool ChildAllowed
        {
            get { return _childAllowed; }
            set { _childAllowed = value; }
        }

        private CustomerInforRowInputBLL customerInforRowInputBLL;
        public CustomerInforRowInputBLL CustomerInforRowInputBLL
        {
            get
            {
                if (customerInforRowInputBLL == null)
                {
                    customerInforRowInputBLL = new CustomerInforRowInputBLL();
                }
                return customerInforRowInputBLL;
            }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlNationalities.Items.Insert(0, new ListItem("--Nationality--", "-1"));
                ddlNationalities.Items.Insert(1, new ListItem("Unknown", "290"));
                ddlNationalities.DataSource = CustomerInforRowInputBLL.NationalityGetAll().OrderBy(x=>x.Name);
                ddlNationalities.DataTextField = "Name";
                ddlNationalities.DataValueField = "Id";
                ddlNationalities.DataBind();
                
                var itemRemove = ddlNationalities.Items.FindByText("KHONG RO");
                if (itemRemove != null)
                    ddlNationalities.Items.Remove(itemRemove);
                if (string.IsNullOrEmpty(txtBirthDay.Text)) {
                    txtBirthDay.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }

        protected virtual void Page_UnLoad(object sender, EventArgs e)
        {
            if (customerInforRowInputBLL != null)
            {
                customerInforRowInputBLL.Dispose();
                customerInforRowInputBLL = null;
            }
        }

        public Customer NewCustomer(SailsModule module)
        {
            Customer customer;
            if (CustomerId > 0)
            {
                customer = module.CustomerGetById(CustomerId);
            }
            else
            {
                customer = new Customer();
            }
            SetCustomer(customer, module);
            return customer;
        }

        public int CustomerId
        {
            get
            {
                if (!string.IsNullOrEmpty(hiddenId.Value))
                {
                    return Convert.ToInt32(hiddenId.Value);
                }
                return 0;
            }
        }

        public void SetCustomer(Customer customer, SailsModule module)
        {
            customer.Fullname = txtName.Text;
            switch (ddlGender.SelectedIndex)
            {
                case 1:
                    customer.IsMale = true;
                    break;
                case 2:
                    customer.IsMale = false;
                    break;
                default:
                    customer.IsMale = null;
                    break;
            }
            customer.Passport = txtPassport.Text;
            customer.VisaNo = txtVisaNo.Text;
            DateTime birthdate;
            if (DateTime.TryParseExact(txtBirthDay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdate))
            {
                customer.Birthday = birthdate;
            }
            else
            {
                customer.Birthday = DateTime.Now;
            }

            DateTime expired;
            if (DateTime.TryParseExact(txtVisaExpired.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expired))
            {
                customer.VisaExpired = expired;
            }
            else
            {
                customer.VisaExpired = null;
            }
            customer.IsVietKieu = chkVietKieu.Checked;
            customer.Code = txtCode.Text;

            if (ddlNationalities.SelectedValue == "-1")
                customer.Nationality = null;
            else
                customer.Nationality = module.NationalityGetById(Convert.ToInt32(ddlNationalities.SelectedValue));

            if (!string.IsNullOrEmpty(txtTotal.Text))
            {
                customer.Total = Convert.ToDouble(txtTotal.Text);
            }

            customer.NguyenQuan = txtNguyenQuan.Text;
        }

        public void GetCustomer(Customer customer, SailsModule module)
        {
            if (customer.Nationality != null)
            {
                ddlNationalities.SelectedValue = customer.Nationality.Id.ToString();
            }

            txtName.Text = customer.Fullname;
            if (customer.IsMale.HasValue)
            {
                if (customer.IsMale.Value)
                {
                    ddlGender.SelectedIndex = 1;
                }
                else
                {
                    ddlGender.SelectedIndex = 2;
                }
            }
            else
            {
                ddlGender.SelectedIndex = 0;
            }
            txtPassport.Text = customer.Passport;
            txtVisaNo.Text = customer.VisaNo;
            if (customer.Birthday.HasValue)
            {
                txtBirthDay.Text = customer.Birthday.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                txtBirthDay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            if (customer.VisaExpired.HasValue)
            {
                txtVisaExpired.Text = customer.VisaExpired.Value.ToString("dd/MM/yyyy");
            }

            chkVietKieu.Checked = customer.IsVietKieu;
            txtCode.Text = customer.Code;
            txtTotal.Text = customer.Total.ToString();
            if (!String.IsNullOrEmpty(customer.NguyenQuan))
            {
                txtNguyenQuan.Text = customer.NguyenQuan;
            }
            if (module.ModuleSettings(SailsModule.CUSTOMER_PRICE) == null || Convert.ToBoolean(module.ModuleSettings(SailsModule.CUSTOMER_PRICE)))
            {
                txtTotal.Visible = true;
            }
            else
            {
                txtTotal.Visible = false;
            }

            hiddenId.Value = customer.Id.ToString();
        }

        public double Total
        {
            get
            {
                if (string.IsNullOrEmpty(txtTotal.Text))
                {
                    return 0;
                }
                return Convert.ToDouble(txtTotal.Text);
            }
        }
    }
}