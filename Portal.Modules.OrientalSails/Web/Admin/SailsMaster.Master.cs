using System;
using System.Collections;
using System.Reflection;
using System.Web.UI;
using CMS.Core.Domain;
using CMS.Web.HttpModules;
using Portal.Modules.OrientalSails.Domain;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SailsMaster : MasterPage
    {
        protected IList AgencyContactBirthdayList = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            #region -- Bookings --
            hyperLinkBookingList.NavigateUrl = string.Format("BookingList.aspx?NodeId={0}&SectionId={1}",
                                                             Request.QueryString["NodeId"],
                                                             Request.QueryString["SectionId"]);
            hplAddBooking.NavigateUrl = string.Format("AddBooking.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            hplOrders.NavigateUrl = string.Format("OrderReport.aspx?NodeId={0}&SectionId={1}",
                                                  Request.QueryString["NodeId"],
                                                  Request.QueryString["SectionId"]);
            hplAllPending.NavigateUrl = string.Format("OrderReport.aspx?NodeId={0}&SectionId={1}&mode=all",
                                                  Request.QueryString["NodeId"],
                                                  Request.QueryString["SectionId"]);
            hplBookingDate.NavigateUrl = string.Format("BookingReport.aspx?NodeId={0}&SectionId={1}",
                                           Request.QueryString["NodeId"],
                                           Request.QueryString["SectionId"]);
            hplBookingPeriod.NavigateUrl = string.Format("BookingReportPeriodAll.aspx?NodeId={0}&SectionId={1}",
                                                         Request.QueryString["NodeId"],
                                                         Request.QueryString["SectionId"]);
            hplRevenueChanged.NavigateUrl = string.Format("TrackingReport.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);

            //hplBookingPeriod.Text = Resources.hplBookingPeriod;
            //hplAddBooking.Text = Resources.hplAddBooking;
            //hyperLinkBookingList.Text = Resources.hyperLinkBookingList;
            //hplOrders.Text = Resources.hplOrders;
            //hplBookingDate.Text = Resources.hplBookingDate;
            //hplRevenueChanged.Text = Resources.hplRevenueChanged;
            #endregion

            #region -- Reports --
            hplIncomeReport.NavigateUrl = string.Format("IncomeReport.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            hplIncomeOwn.NavigateUrl = string.Format("PaymentReport.aspx?NodeId={0}&SectionId={1}",
                                                     Request.QueryString["NodeId"],
                                                     Request.QueryString["SectionId"]);
            hplExpenseReport.NavigateUrl = string.Format("ExpenseReport.aspx?NodeId={0}&SectionId={1}",
                                                         Request.QueryString["NodeId"],
                                                         Request.QueryString["SectionId"]);
            hplExpenseDebt.NavigateUrl = string.Format("PayableList.aspx?NodeId={0}&SectionId={1}",
                                                       Request.QueryString["NodeId"],
                                                       Request.QueryString["SectionId"]);
            hplBalance.NavigateUrl = string.Format("BalanceReport.aspx?NodeId={0}&SectionId={1}",
                                                   Request.QueryString["NodeId"],
                                                   Request.QueryString["SectionId"]);
            hplInspection.NavigateUrl = string.Format("InspectionReport.aspx?NodeId={0}&SectionId={1}",
                Request.QueryString["NodeId"], Request.QueryString["SectionId"]);
            #endregion

            #region -- Partners --
            hplAgencyEdit.NavigateUrl = string.Format("AgencyEdit.aspx?NodeId={0}&SectionId={1}",
                                                      Request.QueryString["NodeId"],
                                                      Request.QueryString["SectionId"]);
            hplAgencyList.NavigateUrl = string.Format("AgencyList.aspx?NodeId={0}&SectionId={1}",
                                                      Request.QueryString["NodeId"],
                                                      Request.QueryString["SectionId"]);
            hyperLinkAgencyList.NavigateUrl = string.Format("AgentList.aspx?NodeId={0}&SectionId={1}",
                                                            Request.QueryString["NodeId"],
                                                            Request.QueryString["SectionId"]);
            //hplAgencyEdit.Text = Resources.hplAgencyEdit;
            //hplAgencyList.Text = Resources.hplAgencyList;
            //hyperLinkAgencyList.Text = Resources.hyperLinkAgencyList;
            #endregion

            #region -- Trips --
            hyperLinkTripList.NavigateUrl = string.Format("SailsTripList.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            hyperLinkTripEdit.NavigateUrl = string.Format("SailsTripEdit.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            hplCruiseEdit.NavigateUrl = string.Format("CruisesEdit.aspx?NodeId={0}&SectionId={1}",
                                                      Request.QueryString["NodeId"],
                                                      Request.QueryString["SectionId"]);
            hplCruiseList.NavigateUrl = string.Format("CruisesList.aspx?NodeId={0}&SectionId={1}",
                                                      Request.QueryString["NodeId"],
                                                      Request.QueryString["SectionId"]);
            #endregion

            #region -- Room manager --
            #endregion

            #region -- Basic links --

            hyperLinkHomePage.NavigateUrl = string.Format("Home.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);

            hyperLinkRoomList.NavigateUrl = string.Format("RoomList.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            hyperLinkRoomEdit.NavigateUrl = string.Format("RoomEdit.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            hplPriceSettings.NavigateUrl = string.Format("DefaultSetting.aspx?NodeId={0}&SectionId={1}",
                                                             Request.QueryString["NodeId"],
                                                             Request.QueryString["SectionId"]);
            hyperLinkExtraOption.NavigateUrl = string.Format("ExtraOptionEdit.aspx?NodeId={0}&SectionId={1}",
                                                             Request.QueryString["NodeId"],
                                                             Request.QueryString["SectionId"]);
            hyperLinkRoomClass.NavigateUrl = string.Format("RoomClassEdit.aspx?NodeId={0}&SectionId={1}",
                                                           Request.QueryString["NodeId"],
                                                           Request.QueryString["SectionId"]);
            hyperLinkRoomType.NavigateUrl = string.Format("RoomTypexEdit.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            hplMedia.NavigateUrl = string.Format("Media.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            #endregion

            #region -- Report --
            hplUSDRate.NavigateUrl = string.Format("ExchangeRate.aspx?NodeId={0}&SectionId={1}",
                                                      Request.QueryString["NodeId"],
                                                      Request.QueryString["SectionId"]);
            hplCosting.NavigateUrl = string.Format("Costing.aspx?NodeId={0}&SectionId={1}",
                                                      Request.QueryString["NodeId"],
                                                      Request.QueryString["SectionId"]);
            #endregion

            #region -- Task --
            hplHaiPhong.NavigateUrl = string.Format("HaiPhongContracts.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            hplCostTypes.NavigateUrl = string.Format("CostTypes.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);

            hplHaiPhongExpenseReport.NavigateUrl = string.Format("HaiPhongExpenseReport.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);

            hplCustomerList.NavigateUrl = string.Format("CustomerList.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            hplExpensePeriod.NavigateUrl = string.Format("ExpensePeriod.aspx?NodeId={0}&SectionId={1}",
                                                          Request.QueryString["NodeId"],
                                                          Request.QueryString["SectionId"]);
            hplDailyCost.NavigateUrl = string.Format("DailyExpenseConfig.aspx?NodeId={0}&SectionId={1}",
                                                      Request.QueryString["NodeId"],
                                                      Request.QueryString["SectionId"]);


            hplExpenseDate.NavigateUrl = hplBookingDate.NavigateUrl;
            #endregion

            #region -- Tabs --

            hyperLinkHomePage.Text = Resources.hplHome;
            hplTabBooking.Text = Resources.tabBooking;
            hplTabConfiguration.Text = Resources.tabConfiguration;
            hplTabCost.Text = Resources.tabCost;
            hplTabCustomers.Text = Resources.tabCustomers;
            hplTabReports.Text = Resources.tabReports;
            hplTabRoom.Text = Resources.tabRoom;
            hplTabSetting.Text = Resources.tabSetting;
            hplTabTrips.Text = Resources.tabTrips;
            #endregion





            if (hplComments != null)
            {
                hplComments.NavigateUrl = string.Format("CustomerComment.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            }
            if (hplPermissions != null)
            {
                hplPermissions.NavigateUrl = string.Format("SetPermission.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            }
            if (hplReceiablePayable != null)
            {
                hplReceiablePayable.NavigateUrl = string.Format("ReceivableTotal.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            }

            hplAddQuestion.NavigateUrl = string.Format("QuestionGroupEdit.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            hplQuestionList.NavigateUrl = string.Format("QuestionView.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            hplFeedbackReport.NavigateUrl = string.Format("FeedbackReport.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);

            hplFeedbackResponse.NavigateUrl = string.Format("FeedbackResponse.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);

            hplDocumentManage.NavigateUrl = string.Format("DocumentManage.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            hplViewDocument.NavigateUrl = string.Format("DocumentView.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            hplViewMeetings.NavigateUrl = string.Format("ViewMeetings.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);

            if (hplUserPanel != null)
            {
                hplUserPanel.NavigateUrl = string.Format("User.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            }

            hplVoucherEdit.NavigateUrl = string.Format("VoucherEdit.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            hplVoucherList.NavigateUrl = string.Format("VoucherList.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            hplAgencyLocation.NavigateUrl = string.Format("AgencyLocations.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            hplVoucherTemplates.NavigateUrl = string.Format("VoucherTemplates.aspx?NodeId={0}&SectionId={1}",
                                                        Request.QueryString["NodeId"],
                                                        Request.QueryString["SectionId"]);
            hplAddSerialBookings.NavigateUrl = string.Format("AddSeriesBookings.aspx?NodeId={0}&SectionId={1}",
                Request.QueryString["NodeId"],
                Request.QueryString["SectionId"]);
        }

        public void BindDataToRptBirdthday(SailsModule module)
        {
            AgencyContactBirthdayList = module.AgencyContactGetByTodayBirdthday();
            if (AgencyContactBirthdayList.Count > 0)
            {
                rptBirthday.DataSource = AgencyContactBirthdayList;
                rptBirthday.DataBind();
            }
        }

        public class CheckSentEmailModel
        {
            public AgencyContact AgencyContact { get; set; }
        }

        public void SetRoleToSale()
        {
            //plhReports.Visible = false;
            //plhCosting.Visible = false;
        }

        public void SetTitle(string title)
        {
            labelAdminHeader.Text = title;
        }

        private bool _panelVisible;
        public void CheckPermisson(SailsModule module, User user)
        {
            IList permissions = module.PermissionsGetByUserRole(user);
            IList userPermissions = module.PermissionsGetByUser(user);

            foreach (string str in userPermissions)
            {
                if (!permissions.Contains(str))
                {
                    permissions.Add(str);
                }
            }

            if (user.HasPermission(AccessLevel.Administrator))
            {
                return;
            }

            tabSetting.Visible = false;

            #region-- Booking --
            _panelVisible = false;
            SetVisible(pAddBooking, permissions.Contains(Permission.FORM_ADDBOOKING));
            SetVisible(pBookingList, permissions.Contains(Permission.FORM_BOOKINGLIST));
            SetVisible(pOrders, permissions.Contains(Permission.FORM_ORDERREPORT));
            SetVisible(pBookingDate, permissions.Contains(Permission.FORM_BOOKINGREPORT));
            SetVisible(pRevenueChanged, permissions.Contains(Permission.FORM_TRACKINGREPORT));
            SetVisible(pBookingReport, permissions.Contains(Permission.FORM_BOOKINGREPORTRERIOD));
            SetVisible(pAddSerialBookings, permissions.Contains(Permission.FORM_ADDBOOKING));
            tabBooking.Visible = _panelVisible;
            #endregion

            #region -- Report --
            _panelVisible = false;
            SetVisible(pIncomeReport, permissions.Contains(Permission.FORM_INCOMEREPORT));
            SetVisible(pReceivable, permissions.Contains(Permission.FORM_PAYMENTREPORT));
            SetVisible(pExpenseReport, permissions.Contains(Permission.FORM_EXPENSEREPORT));
            SetVisible(pPayable, permissions.Contains(Permission.FORM_PAYABLELIST));
            SetVisible(pBalance, permissions.Contains(Permission.FORM_BALANCEREPORT));
            SetVisible(pSummary, permissions.Contains(Permission.FORM_RECEIVABLETOTAL));
            tabReports.Visible = _panelVisible;
            #endregion

            #region -- Agency --
            _panelVisible = false;
            SetVisible(pAgencyEdit, permissions.Contains(Permission.FORM_AGENCYEDIT));
            SetVisible(pAgencyList, permissions.Contains(Permission.FORM_AGENCYLIST));
            SetVisible(pAgencyPolicies, permissions.Contains(Permission.FORM_AGENTLIST));
            tabConfiguration.Visible = _panelVisible;
            #endregion

            #region -- Cruise & trip --
            _panelVisible = false;
            SetVisible(pTripEdit, permissions.Contains(Permission.FORM_SAILSTRIPEDIT));
            SetVisible(pTripList, permissions.Contains(Permission.FORM_SAILSTRIPLIST));
            SetVisible(pCruiseEdit, permissions.Contains(Permission.FORM_CRUISESEDIT));
            SetVisible(pCruiseList, permissions.Contains(Permission.FORM_CRUISESLIST));
            tabTrips.Visible = _panelVisible;
            #endregion

            #region -- Room manager --
            _panelVisible = false;
            SetVisible(pRoomClass, permissions.Contains(Permission.FORM_ROOMCLASSEDIT));
            SetVisible(pRoomType, permissions.Contains(Permission.FORM_ROOMTYPEXEDIT));
            SetVisible(pRoomEdit, permissions.Contains(Permission.FORM_ROOMEDIT));
            SetVisible(pRoomList, permissions.Contains(Permission.FORM_ROOMLIST));
            tabRoom.Visible = _panelVisible;
            #endregion

            #region-- Cost --
            _panelVisible = false;
            SetVisible(pExtraService, permissions.Contains(Permission.FORM_EXTRAOPTIONEDIT));
            SetVisible(pCostingConfig, permissions.Contains(Permission.FORM_COSTING));
            //SetVisible(pDailyAutoCost, permissions.Contains(Permission.FORM_));
            SetVisible(pDailyManualCost, permissions.Contains(Permission.FORM_BOOKINGREPORT));
            SetVisible(pHaiPhong, permissions.Contains(Permission.FORM_CRUISECONFIG));
            SetVisible(pExpensePeriod, permissions.Contains(Permission.FORM_EXPENSEPERIOD));
            SetVisible(pCostTypes, permissions.Contains(Permission.FORM_COSTTYPES));
            SetVisible(pUSDRate, permissions.Contains(Permission.FORM_EXCHANGERATE));
            tabCost.Visible = _panelVisible;
            #endregion
        }

        private void SetVisible(Control ctrl, bool visible)
        {
            if (ctrl != null)
            {
                ctrl.Visible = visible;
                _panelVisible |= visible;
            }
        }

        protected void lbtLogOut_Click(object sender, EventArgs e)
        {
            var am = (AuthenticationModule)Context.ApplicationInstance.Modules["AuthenticationModule"];
            am.Logout();

            Context.Response.Redirect(Context.Request.RawUrl);
        }
    }
}