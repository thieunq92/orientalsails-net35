using System;
using System.Collections;
using System.Web.UI.WebControls;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SailsTripList : SailsAdminBase
    {
        #region -- Private Member --

        private readonly ILog _logger = LogManager.GetLogger(typeof (SailsTripList));
        private SailsTrip _currentTrip;
        private int _currentOption;
        private Agency _agency;

        #endregion

        #region -- Page Event --

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["agencyid"]!=null)
                {
                    _agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["agencyid"]));
                    titleSailsTripList.Text = "Sails trip list for contract price of " + _agency.Name;
                }
                else
                {
                    Title = Resources.titleSailsTripList;
                }
                if (!IsPostBack)
                {
                    GetDataSource();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when Page_Load in SailsTripList", ex);
                ShowError(ex.Message);
            }
        }

        #endregion

        #region -- Private Method --

        private void GetDataSource()
        {
            rptTripList.DataSource = Module.TripGetAll(true);
            rptTripList.DataBind();
        }

        #endregion

        #region -- Control Event --

        protected void rptTripList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            SailsTrip item = e.Item.DataItem as SailsTrip;
            if (item != null)
            {
                #region Name

                using (HyperLink hyperLink_Name = e.Item.FindControl("hyperLink_Name") as HyperLink)
                {
                    if (hyperLink_Name != null)
                    {
                        hyperLink_Name.Text = item.Name;
                        hyperLink_Name.NavigateUrl = string.Format(
                            "SailsTripEdit.aspx?NodeId={0}&SectionId={1}&TripId={2}", Node.Id, Section.Id, item.Id);
                    }
                }

                #endregion

                #region Edit

                using (HyperLink hyperLinkEdit = e.Item.FindControl("hyperLinkEdit") as HyperLink)
                {
                    if (hyperLinkEdit != null)
                    {
                        hyperLinkEdit.NavigateUrl =
                            string.Format("SailsTripEdit.aspx?NodeId={0}&SectionId={1}&TripId={2}",
                                          Node.Id, Section.Id, item.Id);
                    }
                }

                #endregion

                #region Number Of Days

                using (Label label_NumberOfDays = e.Item.FindControl("label_NumberOfDays") as Label)
                {
                    if (label_NumberOfDays != null)
                    {
                        label_NumberOfDays.Text = item.NumberOfDay.ToString();
                    }
                }

                #endregion

                #region Number Of Option

                Repeater rptOptions = e.Item.FindControl("rptOptions") as Repeater;
                if (rptOptions!=null)
                {
                    IList options = new ArrayList();
                    int num = item.NumberOfOptions;
                    if (num == 0) num = 1;
                    for (int ii = 1; ii <= num; ii++)
                    {
                        options.Add(ii);
                    }
                    _currentTrip = item;
                    rptOptions.DataSource = options;
                    rptOptions.DataBind();
                }

                DropDownList ddlOption = (DropDownList)e.Item.FindControl("ddlOption");
                using (Label label_NumberofOption=e.Item.FindControl("label_NumberofOption") as Label)
                {
                    
                    if (label_NumberofOption != null)
                    {
                        label_NumberofOption.Text = item.NumberOfOptions.ToString();

                        switch (item.NumberOfOptions)
                        {
                            case 2:
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option1),"1"));
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option2), "2"));
                                break;
                            case 3:
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option1), "1"));
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option2), "2"));
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option3), "3"));
                                break;
                            default :
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption),TripOption.Option1),"1"));
                                break;
                        }
                        ddlOption.DataBind();
                    }
                }
                #endregion
                
            }
        }

        protected void rptTripList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                SailsTrip item = Module.TripGetById(Convert.ToInt32(e.CommandArgument));
                switch (e.CommandName)
                {
                    case "Delete":
                        Module.Delete(item);
                        GetDataSource();
                        break;
                    case "Price":
                        DropDownList ddlOption = (DropDownList)e.Item.FindControl("ddlOption");
                        PageRedirect(string.Format("SailsPriceConfig.aspx?NodeId={0}&SectionId={1}&TripId={2}&Option={3}",Node.Id,Section.Id,item.Id,ddlOption.SelectedValue));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when rptTripList_ItemCommand in RoomList", ex);
                ShowError(ex.Message);
            }
        }

        protected void rptOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            _currentOption = (int) e.Item.DataItem;
            Literal litOption = e.Item.FindControl("litOption") as Literal;
            if (litOption!=null)
            {
                litOption.Text = string.Format("Option {0}", e.Item.DataItem);
            }

            Repeater rptCruises = e.Item.FindControl("rptCruises") as Repeater;
            if (rptCruises!=null)
            {
                rptCruises.DataSource = Module.CruiseGetAll();
                rptCruises.DataBind();
            }
        }

        protected void rptCruises_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Cruise)
            {
                string agency = string.Empty;
                if (_agency!=null)
                {
                    agency = "&agencyid=" + _agency.Id;
                }
                Cruise cruise = (Cruise) e.Item.DataItem;
                if (!cruise.Trips.Contains(_currentTrip))
                {
                    e.Item.Visible = false;
                }
                else
                {
                    HyperLink hplCruise = e.Item.FindControl("hplCruise") as HyperLink;
                    if (hplCruise!=null)
                    {
                        hplCruise.Text = cruise.Name;
                        hplCruise.NavigateUrl =
                            string.Format(
                                "SailsPriceConfig.aspx?NodeId={0}&SectionId={1}&TripId={2}&Option={3}&cruiseid={4}{5}",
                                Node.Id, Section.Id, _currentTrip.Id, _currentOption, cruise.Id, agency);
                    }
                }
            }
        }

        #endregion
    }
}