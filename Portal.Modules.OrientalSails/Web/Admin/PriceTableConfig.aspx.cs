using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Controls;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class PriceTableConfig : SailsAdminBasePage
    {
        public Role Role
        {
            private set { if (value == null) throw new ArgumentNullException("value"); }
            get
            {
                if (!String.IsNullOrEmpty(Request.QueryString["roleid"]))
                {
                    return Module.RoleGetById(Convert.ToInt32(Request.QueryString["roleid"]));
                }
                return null;
            }
        }

        protected DateTime StartDate
        {
            get
            {
                if (!String.IsNullOrEmpty(Request.QueryString["startdate"]))
                    return DateTime.ParseExact(Request.QueryString["startdate"], "ddMMyyyy", CultureInfo.InvariantCulture);
                return new DateTime();
            }
        }

        protected IList AllCruises
        {
            get
            {
                return Module.CruiseGetAll();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var sailsPriceTable = GetSailsPriceTableReturnNotNull() as SailsPriceTable;
            var agency = GetAgencyReturnNotNull() as Agency;
            var cruise = GetCruiseReturnNotNull() as Cruise;
            var tripsAndOptions = GetTripsAndOptions() as IList<TripAndOption>;
            var criterion = GetCriterion() as ICriterion;

            SetStartEndDate();
            SetTitleLabel();
            HideSailsPriceTableTemplateDropDownList();
            VisibleBackSailsPriceTableButton();
            HideControlInMasterPageViewModePageLoad();

            if (!IsPostBack)
            {

                rptCruises.DataSource = Module.CruiseGetAll();
                rptCruises.DataBind();

                rptTrips.DataSource = tripsAndOptions;
                rptTrips.DataBind();

                rptRoomTypeHeader.DataSource = Module.RoomTypeGetByCruise(cruise);
                rptRoomTypeHeader.DataBind();

                for (int i = 0; i < cruise.Rooms.Count; i++)
                {
                    ddlRoomFrom.Items.Add(new ListItem((i + 1).ToString(CultureInfo.InvariantCulture), (i + 1).ToString(CultureInfo.InvariantCulture)));
                    ddlRoomTo.Items.Add(new ListItem((i + 1).ToString(CultureInfo.InvariantCulture), (i + 1).ToString(CultureInfo.InvariantCulture)));
                }


            }

            rptTripsCharterTable.DataSource = tripsAndOptions;
            rptTripsCharterTable.DataBind();

            rptRoomRangeTable.DataSource = Module.GetObject<Charter>(criterion, 0, 0);
            rptRoomRangeTable.DataBind();

            HideCharterTableIfEmpty();
            LoadTemplateTablePrice();

            if (GetSailsPriceTable() != null)
            {
                if (sailsPriceTable.Role != null)
                {
                    hplBack.NavigateUrl = string.Format("PriceConfiguration.aspx?NodeId={0}&SectionId={1}&roleid={2}",
                        Node.Id,
                        Section.Id, sailsPriceTable.Role.Id);
                }
                if (sailsPriceTable.Agency != null)
                {
                    hplBack.NavigateUrl = string.Format("PriceConfiguration.aspx?NodeId={0}&SectionId={1}&agentid={2}",
                 Node.Id,
                 Section.Id, sailsPriceTable.Agency.Id);
                }
            }
        }

        public SailsPriceTable GetSailsPriceTable()
        {
            if (GetAgency() != null && GetStartDate() != null)
            {
                return GetSailsPriceTableByAgencyAndStartDate();
            }
            if (!String.IsNullOrEmpty(Request.QueryString["tableid"]))
            {
                return Module.PriceTableGetById(Convert.ToInt32(Request.QueryString["tableid"]));
            }
            return null;
        }

        public SailsPriceTable GetSailsPriceTableReturnNotNull()
        {
            if (GetAgency() != null && GetStartDate() != null)
            {
                return GetSailsPriceTableByAgencyAndStartDate();
            }
            var sailsPriceTable = GetSailsPriceTable();
            if (sailsPriceTable == null)
            {
                sailsPriceTable = new SailsPriceTable();
                sailsPriceTable.Agency = GetAgencyReturnNotNull();
                sailsPriceTable.Role = GetAgencyReturnNotNull().Role;
            }
            if (GetSailsPriceTableTemplate() != null)
            {
                sailsPriceTable = GetSailsPriceTableTemplate();
            }
            return sailsPriceTable;
        }

        public SailsPriceTable GetSailsPriceTableTemplate()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["templateid"]))
            {
                return Module.PriceTableGetById(Convert.ToInt32(Request.QueryString["templateid"]));
            }
            return null;
        }

        public SailsPriceTable GetSailsPriceTableByAgencyAndStartDate()
        {
            var agency = GetAgency() as Agency;
            if (agency != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["startdate"]))
                {
                    var startDate = DateTime.ParseExact(Request.QueryString["startdate"], "ddMMyyyy",
                        CultureInfo.InvariantCulture);
                    var agencyCriterion = null as ICriterion;
                    if (agency.Role == null)
                    {
                        agencyCriterion = Expression.Eq("Agency", agency);
                    }
                    else
                    {
                        agencyCriterion = Expression.Eq("Role", agency.Role);
                    }
                    var startDateCriterion = Expression.Le("StartDate", startDate);
                    var endDateCriterion = Expression.Ge("EndDate", startDate);
                    var startAndEndDateCriterion = Expression.And(startDateCriterion, endDateCriterion);
                    var finalCriterion = Expression.And(startAndEndDateCriterion, agencyCriterion);
                    var sailsPriceTableList = Module.GetObject<SailsPriceTable>(finalCriterion, 0, 0);
                    if (sailsPriceTableList.Count > 0)
                    {
                        return sailsPriceTableList[0];
                    }
                }
            }
            return null;
        }

        public Agency GetAgency()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["agentid"]))
            {
                return Module.GetObject<Agency>(Convert.ToInt32(Request.QueryString["agentid"]));
            }
            return null;
        }

        public DateTime? GetStartDate()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["startdate"]))
            {
                return DateTime.ParseExact(Request.QueryString["startdate"], "ddMMyyyy", CultureInfo.InvariantCulture);
            }
            return null;
        }

        public Agency GetAgencyReturnNotNull()
        {
            var agency = GetAgency() as Agency;
            if (agency == null)
            {
                agency = new Agency();
            }
            return agency;
        }

        public Cruise GetCruise()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["cruiseid"]))
            {
                return Module.GetObject<Cruise>(Convert.ToInt32(Request.QueryString["cruiseid"]));
            }
            return null;
        }

        public Cruise GetCruiseReturnNotNull()
        {
            var cruise = GetCruise() as Cruise;
            if (cruise == null)
            {
                cruise = Module.GetObject<Cruise>(Expression.Ge("Id", 0), 0, 0)[0];
            }
            return cruise;
        }

        public ICriterion GetCriterion()
        {
            var cruise = GetCruiseReturnNotNull() as Cruise;
            var sailsPriceTable = GetSailsPriceTableReturnNotNull() as SailsPriceTable;
            var tripsAndOptions = GetTripsAndOptions() as IList<TripAndOption>;
            ICriterion cruiseCriterion = null;
            ICriterion sailsPriceTableCriterion = null;
            ICriterion cruiseSailsPriceTableCriterion = null;
            ICriterion finalCriterion = null;

            cruiseCriterion = Expression.Eq("Cruise", cruise);
            if (GetSailsPriceTable() != null)
            {
                sailsPriceTableCriterion = Expression.Eq("SailsPriceTable", sailsPriceTable);
            }
            else
            {
                sailsPriceTableCriterion = Expression.Eq("SailsPriceTable.Id", -1);
            }
            cruiseSailsPriceTableCriterion = Expression.And(cruiseCriterion, sailsPriceTableCriterion);
            ICriterion tripTripOptionCriterion = null;
            if (tripsAndOptions != null)
            {
                ICriterion tripCriterion = null;
                ICriterion tripOptionCriterion = null;
                for (int i = 0; i < tripsAndOptions.Count; i++)
                {
                    if (tripsAndOptions[i] != null)
                    {
                        tripCriterion = Expression.Eq("Trip", tripsAndOptions[i].Trip);
                        tripOptionCriterion = Expression.Eq("TripOption", tripsAndOptions[i].Option);
                    }
                }
                if (tripCriterion != null)
                {
                    tripTripOptionCriterion = tripCriterion;
                }
                if (tripOptionCriterion != null)
                {
                    tripTripOptionCriterion = tripOptionCriterion;
                }
                if (tripCriterion != null && tripOptionCriterion != null)
                {
                    tripTripOptionCriterion = Expression.And(tripOptionCriterion, tripCriterion);
                }
            }
            if (tripsAndOptions != null && cruiseSailsPriceTableCriterion != null)
            {
                finalCriterion = Expression.And(tripTripOptionCriterion, cruiseSailsPriceTableCriterion);
            }
            return finalCriterion;
        }

        public void HideCharterTableIfEmpty()
        {
            var criterion = GetCriterion();
            var charterList = Module.GetObject<Charter>(criterion, 0, 0);
            if (charterList.Count == 0)
            {
                plhCharter.Visible = false;
                if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
                {
                    plhCharterTitle.Visible = false;
                }
            }
        }

        public void SetTitleLabel()
        {
            var sailsPriceTable = GetSailsPriceTable();
            if (sailsPriceTable != null)
            {
                if (sailsPriceTable.Agency != null)
                    labelTitle.Text = string.Format("Price table for {0}", sailsPriceTable.Agency.Name);
                else
                {
                    if (sailsPriceTable.Role != null)
                    {
                        labelTitle.Text = string.Format("Price table for {0}", sailsPriceTable.Role.Name);
                    }
                }
            }
        }

        public void SetStartEndDate()
        {
            var sailsPriceTable = GetSailsPriceTable() as SailsPriceTable;
            if (sailsPriceTable != null)
            {
                if (sailsPriceTable.StartDate != null)
                    txtValidFrom.Text = sailsPriceTable.StartDate.Value.ToString("dd/MM/yyyy");
                if (sailsPriceTable.EndDate != null)
                    txtValidTo.Text = sailsPriceTable.EndDate.Value.ToString("dd/MM/yyyy");
            }
        }

        public void HideSailsPriceTableTemplateDropDownList()
        {
            var sailsPriceTable = GetSailsPriceTable() as SailsPriceTable;
            if (sailsPriceTable != null)
            {
                if (sailsPriceTable.Role != null)
                {
                    lblSailsPriceTableTemplate.Visible = false;
                    ddlTemplatePriceTable.Visible = false;
                    btnGetTemplatePriceTable.Visible = false;
                    btnBackSailPriceTable.Visible = false;
                }
            }
        }

        public void DeletePriceAddedSailsPriceTableTemplate()
        {
            var charterList = Session["charterlist"] as List<int>;
            if (charterList != null)
            {
                for (int i = 0; i < charterList.Count; i++)
                {
                    Module.Delete(Module.GetObject<Charter>(charterList[i]));
                }
                Session["charterlist"] = null;
            }
        }

        public void VisibleBackSailsPriceTableButton()
        {
            if (GetSailsPriceTableTemplate() != null)
            {
                btnBackSailPriceTable.Visible = true;
            }
            else
            {
                btnBackSailPriceTable.Visible = false;
            }
        }

        public IList<TripAndOption> GetTripsAndOptions()
        {
            var cruise = GetCruiseReturnNotNull();
            var trips = new List<TripAndOption>();
            foreach (SailsTrip cruiseTrips in cruise.Trips)
            {
                if (cruiseTrips.NumberOfOptions == 2)
                {
                    trips.Add(new TripAndOption(cruiseTrips, TripOption.Option1));
                    trips.Add(new TripAndOption(cruiseTrips, TripOption.Option2));
                }
                else
                {
                    trips.Add(new TripAndOption(cruiseTrips, TripOption.Option1));
                }
            }
            return trips;
        }

        public void LoadTemplateTablePrice()
        {
            var roleCriterion = Expression.IsNotNull("Role");
            var sailsPriceTableHaveRoleList = Module.GetObject<SailsPriceTable>(roleCriterion, 0, 0, new Order("Role", true));
            if (sailsPriceTableHaveRoleList.Count == 0)
            {
                lblSailsPriceTableTemplate.Visible = false;
                ddlTemplatePriceTable.Visible = false;
                btnGetTemplatePriceTable.Visible = false;
                btnBackSailPriceTable.Visible = false;
            }
            for (int i = 0; i < sailsPriceTableHaveRoleList.Count; i++)
            {
                var startDate = sailsPriceTableHaveRoleList[i].StartDate;
                if (startDate != null)
                {
                    var dateTime = sailsPriceTableHaveRoleList[i].EndDate;
                    if (dateTime != null)
                    {
                        var name = sailsPriceTableHaveRoleList[i].Role.Name + " Valid From : " +
                                   startDate.Value.ToString("dd/MM/yyyy") + " - Valid To : " +
                                   dateTime.Value.ToString("dd/MM/yyyy");
                        ddlTemplatePriceTable.Items.Add(new ListItem(name, sailsPriceTableHaveRoleList[i].Id.ToString(CultureInfo.InvariantCulture)));
                    }
                }
            }
            if (GetSailsPriceTableTemplate() != null)
            {
                ddlTemplatePriceTable.SelectedValue = GetSailsPriceTableTemplate().Id.ToString(CultureInfo.InvariantCulture);
            }
        }

        public void DeleteOldItemPriceConfig()
        {
            var sailsPriceTableTemplate = GetSailsPriceTableTemplate();
            var sailsPriceTable = GetSailsPriceTable();
            if (sailsPriceTableTemplate != null)
            {
                ICriterion sailsPriceTableCriterion = Expression.Eq("Table", sailsPriceTable);
                var priceConfigList = Module.GetObject<Domain.SailsPriceConfig>(sailsPriceTableCriterion, 0, 0) as IList;
                if (priceConfigList != null)
                {
                    for (int i = 0; i < priceConfigList.Count; i++)
                    {
                        Module.Delete(priceConfigList[i]);
                    }
                }

                ICriterion sailsPriceTableCharterCriterion = Expression.Eq("SailsPriceTable", sailsPriceTable);
                var charterConfigList = Module.GetObject<Charter>(sailsPriceTableCharterCriterion, 0, 0) as IList;
                if (charterConfigList != null)
                {
                    for (int i = 0; i < charterConfigList.Count; i++)
                    {
                        Module.Delete(charterConfigList[i]);
                    }
                }
            }
        }

        public void HideControlInMasterPageViewMode()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                this.MasterPageFile = "Blank.Master";
            }
        }

        public void HideControlInMasterPageViewModePageLoad()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                hplBack.Visible = false;
                labelTitle.Visible = false;
                imgAvatar.Visible = false;
                tblDateTime.Visible = false;
            }
        }

        protected void rptCruises_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var agency = GetAgencyReturnNotNull() as Agency;
            var sailsPriceTable = GetSailsPriceTable() as SailsPriceTable;
            var cruiseRequest = GetCruiseReturnNotNull() as Cruise;
            if (e.Item.DataItem is Cruise)
            {
                var cruise = (Cruise)e.Item.DataItem;
                var hplCruises = e.Item.FindControl("hplCruises") as HyperLink;
                if (hplCruises != null)
                {
                    hplCruises.Text = cruise.Name;
                    if (GetAgency() != null)
                    {
                        hplCruises.NavigateUrl = string.Format(
                            "PriceTableConfig.aspx?NodeId={0}&SectionId={1}&cruiseid={2}&agentid={3}&startdate={4}", Node.Id,
                            Section.Id,
                            cruise.Id, agency.Id, StartDate.ToString("ddMMyyyy"));
                        if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
                        {
                            hplCruises.NavigateUrl = string.Format(
                            "PriceTableConfig.aspx?NodeId={0}&SectionId={1}&cruiseid={2}&agentid={3}&startdate={4}&mode=view", Node.Id,
                            Section.Id,
                            cruise.Id, agency.Id, StartDate.ToString("ddMMyyyy"));
                        }
                    }
                    else
                    {
                        if (GetSailsPriceTable() != null)
                        {
                            hplCruises.NavigateUrl = string.Format(
                                "PriceTableConfig.aspx?NodeId={0}&SectionId={1}&cruiseid={2}&tableid={3}", Node.Id,
                                Section.Id,
                                cruise.Id, sailsPriceTable.Id);
                            if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
                            {
                                hplCruises.NavigateUrl = string.Format(
                                "PriceTableConfig.aspx?NodeId={0}&SectionId={1}&cruiseid={2}&tableid={3}&mode=view", Node.Id,
                                Section.Id,
                                cruise.Id, sailsPriceTable.Id);
                            }
                        }
                    }

                    var liMenu = e.Item.FindControl("liMenu") as HtmlGenericControl;
                    if (cruiseRequest != null)
                    {
                        if (cruiseRequest.Name == cruise.Name)
                            if (liMenu != null)
                                liMenu.Attributes.Add("style", "background-color:#f4f900");
                    }
                }
            }
        }

        protected void rptTrips_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var agency = GetAgency() as Agency;
            var sailsPriceTable = GetSailsPriceTableReturnNotNull() as SailsPriceTable;
            var cruise = GetCruiseReturnNotNull() as Cruise;
            if (e.Item.DataItem is TripAndOption)
            {
                var data = (TripAndOption)e.Item.DataItem;
                var cpt = e.Item.FindControl("priceTable") as CruisePriceTable;
                if (cpt != null)
                {
                    //cpt.Module = Module;
                    cpt.SailsPriceTable = sailsPriceTable;
                    cpt.ActiveCruise = cruise;
                    cpt.AllRoomClasses = AllRoomClasses;
                    cpt.AllRoomTypes = AllRoomTypes;
                    cpt.Trip = data.Trip;
                    cpt.Option = data.Option;
                    cpt.Bind();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var sailsPriceTable = GetSailsPriceTable() as SailsPriceTable;
            var sailsPriceTableTemplate = GetSailsPriceTableTemplate() as SailsPriceTable;

            DeleteOldItemPriceConfig();
            if (sailsPriceTable == null)
            {
                throw new Exception("sailsPriceTable = null");
            }
            if (Request.Form[txtValidFrom.UniqueID].Trim() != "")
            {
                sailsPriceTable.StartDate = DateTime.ParseExact(Request.Form[txtValidFrom.UniqueID], "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
            }
            if (Request.Form[txtValidTo.UniqueID].Trim() != "")
            {
                sailsPriceTable.EndDate = DateTime.ParseExact(Request.Form[txtValidTo.UniqueID], "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
            }

            Module.SaveOrUpdate(sailsPriceTable);

            int hiddenCruiseId;
            if (!String.IsNullOrEmpty(Request.QueryString["cruiseid"]))
                hiddenCruiseId = Module.GetObject<Cruise>(Convert.ToInt32(Request.QueryString["cruiseid"])).Id;
            else
                hiddenCruiseId = Module.GetObject<Cruise>(Expression.Ge("Id", 0), 0, 0)[0].Id;

            if (rptTrips != null)
            {
                Cruise cruise = Module.CruiseGetById(Convert.ToInt32(hiddenCruiseId));
                foreach (RepeaterItem item in rptTrips.Items)
                {
                    var priceTable = item.FindControl("priceTable") as CruisePriceTable;
                    var hiddenTripId = item.FindControl("hiddenTripId") as HiddenField;
                    var hiddenTripOption = item.FindControl("hiddenTripOption") as HiddenField;

                    if (priceTable != null && hiddenTripId != null && hiddenTripOption != null)
                    {
                        SailsTrip trip = Module.TripGetById(Convert.ToInt32(hiddenTripId.Value));
                        priceTable.SailsPriceTable = sailsPriceTable;
                        priceTable.SailsPriceTableTemplate = sailsPriceTableTemplate;
                        priceTable.Trip = trip;
                        priceTable.Option = (TripOption)Enum.Parse(typeof(TripOption), hiddenTripOption.Value);
                        priceTable.ActiveCruise = cruise;
                        priceTable.Save();
                    }
                }
            }

            if (rptTripsCharterTable != null)
            {
                Cruise cruise = Module.CruiseGetById(Convert.ToInt32(hiddenCruiseId));
                foreach (RepeaterItem item in rptTripsCharterTable.Items)
                {
                    var priceTable = item.FindControl("priceTable") as CruisePriceTableCharter;
                    var hiddenTripId = item.FindControl("hiddenTripId") as HiddenField;
                    var hiddenTripOption = item.FindControl("hiddenTripOption") as HiddenField;

                    if (priceTable != null && hiddenTripId != null && hiddenTripOption != null)
                    {
                        SailsTrip trip = Module.TripGetById(Convert.ToInt32(hiddenTripId.Value));
                        priceTable.SailsPriceTable = sailsPriceTable;
                        priceTable.SailsPriceTableTemplate = sailsPriceTableTemplate;
                        priceTable.Trip = trip;
                        priceTable.Option = (TripOption)Enum.Parse(typeof(TripOption), hiddenTripOption.Value);
                        priceTable.ActiveCruise = cruise;
                        priceTable.Save();
                    }
                }
            }

            DeletePriceAddedSailsPriceTableTemplate();

            var queryString = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            if (queryString["templateid"] != null)
            {
                queryString.Remove("templateid");
            }

            Response.Redirect(Request.Url.AbsolutePath + "?" + queryString.ToString());
        }

        protected void rptRoomTypeHeader_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ltrCurrency.Text = ltrCurrency.Text + @"<th>USD</th><th>VND</th>";
        }

        protected void btnAddRoomRange_OnClick(object sender, EventArgs e)
        {
            var cruise = GetCruiseReturnNotNull() as Cruise;
            var tripsAndOptions = GetTripsAndOptions() as IList<TripAndOption>;
            var sailsPriceTable = GetSailsPriceTableReturnNotNull() as SailsPriceTable;
            //cần tạo method cho đoạn code này , đoạn code này để lưu các charter của pricetabletemplate khi duoc add them
            var charterList = Session["charterList"] as List<int>;
            if (charterList == null)
            {
                charterList = new List<int>();
            }

            for (int i = 0; i < tripsAndOptions.Count; i++)
            {
                var charter = new Charter();
                charter.RoomFrom = Convert.ToInt32(ddlRoomFrom.SelectedValue);
                charter.RoomTo = Convert.ToInt32(ddlRoomTo.SelectedValue);
                charter.SailsPriceTable = sailsPriceTable;
                charter.Cruise = cruise;
                charter.Trip = tripsAndOptions[i].Trip;
                charter.TripOption = tripsAndOptions[i].Option;
                Module.SaveOrUpdate(charter);
                if (GetSailsPriceTableTemplate() != null)
                {
                    charterList.Add(charter.Id);
                }
            }
            Session["charterlist"] = charterList;
            //
            Response.Redirect(Request.RawUrl);
        }

        protected void rptRoomRangeTable_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex == 0)
            {
                ltrCurrencyRoomRange.Text = "";
            }
            ltrCurrencyRoomRange.Text = ltrCurrencyRoomRange.Text + @"<th>USD</th><th>VND</th>";
        }

        protected void rptTripsCharterTable_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cruise = GetCruiseReturnNotNull() as Cruise;
            var sailsPriceTable = GetSailsPriceTableReturnNotNull() as SailsPriceTable;

            if (e.Item.DataItem is TripAndOption)
            {
                var data = (TripAndOption)e.Item.DataItem;
                var cpt = e.Item.FindControl("priceTable") as CruisePriceTableCharter;
                if (cpt != null)
                {
                    //cpt.Module = Module;
                    cpt.SailsPriceTable = sailsPriceTable;
                    cpt.ActiveCruise = cruise;
                    ICriterion cruiseCriterion = Expression.Eq("Cruise", cruise);
                    ICriterion sailsPriceTableCriterion = null;
                    if (GetSailsPriceTable() == null)
                    {
                        sailsPriceTableCriterion = Expression.Eq("SailsPriceTable.Id", -1);
                    }
                    else
                    {
                        sailsPriceTableCriterion = Expression.Eq("SailsPriceTable", sailsPriceTable);
                    }
                    ICriterion tripCriterion = Expression.Eq("Trip", data.Trip);
                    ICriterion tripOptionCriterion = Expression.Eq("TripOption", data.Option);
                    ICriterion tripTripOptionCriterion = Expression.And(tripCriterion, tripOptionCriterion);
                    ICriterion cruiseSailsPriceTableCriterion = Expression.And(cruiseCriterion, sailsPriceTableCriterion);
                    ICriterion finalCriterion = Expression.And(tripTripOptionCriterion, cruiseSailsPriceTableCriterion);
                    cpt.CharterList = Module.GetObject<Charter>(finalCriterion, 0, 0);
                    cpt.Trip = data.Trip;
                    cpt.Option = data.Option;
                }
            }
        }

        protected void btnGetTemplatePriceTable_OnClick(object sender, EventArgs e)
        {
            var queryString = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            if (queryString["templateid"] == null)
            {
                queryString.Add("templateid", Request.Form[ddlTemplatePriceTable.UniqueID]);
            }
            else
            {
                queryString.Set("templateid", Request.Form[ddlTemplatePriceTable.UniqueID]);
            }
            Response.Redirect(Request.Url.AbsolutePath + "?" + queryString.ToString());
        }

        protected void btnBackSailPriceTable_OnClick(object sender, EventArgs e)
        {
            DeletePriceAddedSailsPriceTableTemplate();
            var queryString = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            if (queryString["templateid"] != null)
            {
                queryString.Remove("templateid");
            }
            Response.Redirect(Request.Url.AbsolutePath + "?" + queryString.ToString());
        }

        protected override void OnPreInit(EventArgs e)
        {
            HideControlInMasterPageViewMode();
            base.OnPreInit(e);
        }
    }
}