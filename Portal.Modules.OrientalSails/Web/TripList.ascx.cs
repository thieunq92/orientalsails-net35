using System;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Util;
using CMS.ServerControls;
using CMS.Web.UI;
using CMS.Web.Util;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Resources;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web
{
    public partial class TripList : BaseModuleControl
    {
        #region -- Private Member --

        private readonly ILog _logger = LogManager.GetLogger(typeof (TripList));

        private new SailsModule Module
        {
            get { return base.Module as SailsModule; }
        }

        #endregion

        #region -- Page Event --

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LocalizeControls();
                    GetDataSource();
                    rptTripList.DataBind();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when Page_Load in TripList control", ex);
                throw;
            }
        }

        #endregion

        #region -- Private Method --

        private void GetDataSource()
        {
            rptTripList.DataSource = Module.TripGetAll(true);
        }

        #endregion

        #region -- Control Event --

        protected void rptTripList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            SailsTrip item = e.Item.DataItem as SailsTrip;
            if (item != null)
            {
                #region Image

                using (Image imageTrip = e.Item.FindControl("imageTrip") as Image)
                {
                    if (imageTrip != null)
                    {
                        imageTrip.ImageUrl = item.Image;
                    }
                }

                #endregion

                #region Name

                using (HyperLink hyperLink_Name = e.Item.FindControl("hyperLink_Name") as HyperLink)
                {
                    if (hyperLink_Name != null)
                    {
                        string itemUrl = VnFontConverter.Convert(item.Name, FontIndex.iUNI, FontIndex.iNOSIGN);
                        hyperLink_Name.Text = item.Name;
                        hyperLink_Name.NavigateUrl = string.Format("{0}/{1}/{2}/{3}{4}",
                                                                   UrlHelper.GetUrlFromSection(Module.Section),
                                                                   SailsModule.ACTION_VIEW_TRIP_PARAM, item.Id, itemUrl,
                                                                   UrlHelper.EXTENSION);
                    }
                }

                #endregion

                #region Number Of Days

                using (Label label_NumberofDays = e.Item.FindControl("label_NumberofDays") as Label)
                {
                    if (label_NumberofDays != null)
                    {
                        label_NumberofDays.Text = string.Format("{0}: {1}", Strings.labelNumberOfDays, item.NumberOfDay);
                    }
                }

                #endregion

                #region HyperLink Detail

                using (HyperLink hyperLink_Detail = e.Item.FindControl("hyperLink_Detail") as HyperLink)
                {
                    if (hyperLink_Detail != null)
                    {
                        string itemUrl = VnFontConverter.Convert(item.Name, FontIndex.iUNI, FontIndex.iNOSIGN);
                        hyperLink_Detail.Text = Strings.stringDetail;
                        hyperLink_Detail.NavigateUrl = string.Format("{0}/{1}/{2}/{3}{4}",
                                                                     UrlHelper.GetUrlFromSection(Module.Section),
                                                                     SailsModule.ACTION_VIEW_TRIP_PARAM, item.Id,
                                                                     itemUrl, UrlHelper.EXTENSION);
                    }
                }

                #endregion

                #region Description

                using (HtmlGenericControl description = e.Item.FindControl("description") as HtmlGenericControl)
                {
                    if (description != null)
                    {
                        description.InnerHtml = Text.TruncateText(item.Description, 200);
                    }
                }

                #endregion

                #region Option

                using (DropDownList ddlOption = e.Item.FindControl("ddlOption") as DropDownList)
                {
                    if (ddlOption != null)
                    {
                        switch (item.NumberOfOptions)
                        {
                            case 2:
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option1), "0"));
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option2), "1"));
                                break;
                            case 3:
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option1), "0"));
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option2), "1"));
                                ddlOption.Items.Add(new ListItem(Enum.GetName(typeof(TripOption), TripOption.Option3), "2"));
                                break;
                            default:
                                ddlOption.Enabled = false;
                                ddlOption.Visible = false;
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
                DropDownList ddlOption = (DropDownList)e.Item.FindControl("ddlOption");
                TextBox textBoxStartDate = (TextBox) e.Item.FindControl("textBoxStartDate");
                CultureInfo cultureInfo= new CultureInfo("vi-VN");
                switch (e.CommandName)
                {
                    case "Book":
                        DateTime startDate;
                        try
                        {
                            startDate = DateTime.ParseExact(textBoxStartDate.Text, "dd/MM/yyyy",
                                                            cultureInfo.DateTimeFormat);
                        }
                        catch (Exception)
                        {
                            startDate = DateTime.Today.AddDays(1);
                        }
                        
                        if (startDate < DateTime.Today.AddDays(1))
                        {
                            Label labelError = e.Item.FindControl("labelError") as Label;
                            if (labelError!=null)
                            {
                                labelError.Text = "You can not book in the past!";
                                return;
                            }
                        }
                        Session.Add("StartDate",startDate.ToString("dd/MM/yyyy"));
                        Session.Add("TripId",item.Id);
                        Session.Add("TripOption",ddlOption.SelectedValue);
                        if (item.NumberOfOptions > 1)
                        {
                            PageEngine.PageRedirect(string.Format("{0}/{1}/{2}/{3}/{4}{5}",
                                                                  UrlHelper.GetUrlFromSection(Module.Section),
                                                                  SailsModule.ACTION_ORDER_PARAM, item.Id, ddlOption.SelectedValue,
                                                                  item.Name, UrlHelper.EXTENSION));
                        }
                        else
                        {
                            PageEngine.PageRedirect(string.Format("{0}/{1}/{2}/{3}/{4}{5}",
                                                                  UrlHelper.GetUrlFromSection(Module.Section),
                                                                  SailsModule.ACTION_ORDER_PARAM, item.Id, 0, item.Name,
                                                                  UrlHelper.EXTENSION));
                        }
                        break;
                    default :
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when rptTripList_ItemCommand in TripList control", ex);
                throw;
            }
        }
        #region Pager

        protected void pagerTripList_CacheEmpty(object sender, EventArgs e)
        {
            GetDataSource();
        }

        protected void pagerTripList_PageChanged(object sender, PageChangedEventArgs e)
        {
            GetDataSource();
            rptTripList.DataBind();
        }

        #endregion

        #endregion

        
    }
}