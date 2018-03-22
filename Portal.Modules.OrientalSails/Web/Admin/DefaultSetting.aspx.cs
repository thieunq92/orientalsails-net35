using System;
using System.Collections;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class DefaultSetting : SailsAdminBase
    {
        private IList _agencies;

        protected IList Suppliers
        {
            get
            {
                if (_agencies == null)
                {
                    _agencies = Module.SupplierGetAll();
                }
                return _agencies;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            Title = Resources.titleSettings;
            if (!IsPostBack)
            {
                BindAgencies(ddlTicketSuppliers);
                GetDefault();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Module.SaveModuleSetting("CHILD_PRICE", txtChildPrice.Text);
            Module.SaveModuleSetting("CUSTOM_BK_ID", chkCustomBookingId.Checked.ToString());
            Module.SaveModuleSetting("BOOKING_FORMAT", txtBookingFormat.Text);
            Module.SaveModuleSetting("SHOW_CUSTOMER", chkShowCustomerName.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.ADD_BK_CUSTOMPRICE,chkCustomPriceAddBooking.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.ROOM_CUSTOMPRICE, chkCustomPriceForRoom.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.PARTNERSHIP, chkPartnership.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.ACCOUNT_STATUS, chkAccountStatus.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.CHECK_CHARTER, chkAccountStatus.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.SHOW_EXPENSE_BY_DATE, chkShowExpenseByDate.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.BAR_REVENUE, chkShowExpenseByDate.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.NO_AGENCY_BK, chkNoAgency.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.DETAIL_SERVICE, chkDetail.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.OVERALL_EXPENSE, chkOverall.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.USE_VND_EXPENSE, chkVND.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.APPROVED_DEFAULT, chkApprovedDefault.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.PUREQUIRED, chkPuRequired.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.PERIOD_EXPENSE_AVG, chkPeriodExpenseAvg.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.APPROVED_LOCK, chkApprovedLock.Checked.ToString());
            Module.SaveModuleSetting(SailsModule.CUSTOMER_PRICE, chkCustomerPrice.Checked.ToString());
        }

        protected void GetDefault()
        {
            txtChildPrice.Text = ChildPrice.ToString("0.#");
            chkCustomBookingId.Checked = UseCustomBookingId;
            chkShowCustomerName.Checked = ShowCustomerName;
            txtBookingFormat.Text = BookingFormat;
            chkCustomPriceAddBooking.Checked = CustomPriceAddBooking;
            chkCustomPriceForRoom.Checked = CustomPriceForRoom;
            chkPartnership.Checked = PartnershipManager;
            chkAccountStatus.Checked = CheckAccountStatus;
            chkCharter.Checked = CheckCharter;
            chkShowExpenseByDate.Checked = ShowExpenseByDate;
            chkBarRevenue.Checked = ShowBarRevenue;
            chkNoAgency.Checked = AllowNoAgency;
            chkDetail.Checked = DetailService;
            chkOverall.Checked = ShowOverallDailyExpense;
            chkApprovedDefault.Checked = ApprovedDefault;
            chkPuRequired.Checked = PuRequired;
            chkPeriodExpenseAvg.Checked = PeriodExpenseAvg;
            chkApprovedLock.Checked = ApprovedLock;
            chkCustomerPrice.Checked = CustomerPrice;
            chkVND.Checked = UseVNDExpense;
        }

        protected void BindAgencies(DropDownList ddl)
        {
            ddl.DataSource = Suppliers;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataBind();
        }
    }
}