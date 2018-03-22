using System;
using System.Web.UI.WebControls;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class RoomClassEdit : SailsAdminBase
    {
        #region -- Private Member --

        private readonly ILog _logger = LogManager.GetLogger(typeof (RoomClassEdit));

        private Cruise _cruise;
        private RoomClass _roomClass;

        private int RoomClassId
        {
            get
            {
                if (ViewState["RoomClassId"] != null)
                {
                    return Convert.ToInt32(ViewState["RoomClassId"]);
                }
                return -1;
            }
            set { ViewState["RoomClassId"] = value; }
        }

        #endregion

        #region -- Page Event --

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Title = Resources.titleRoomClassEdit;
                if (Request.QueryString["cruiseid"] != null)
                {
                    _cruise = Module.CruiseGetById(Convert.ToInt32(Request.QueryString["cruiseid"]));
                }
                if (!IsPostBack)
                {
                    GetDataSource();
                    ddlCruises.DataSource = Module.CruiseGetAll();
                    ddlCruises.DataTextField = "Name";
                    ddlCruises.DataValueField = "Id";
                    ddlCruises.DataBind();
                    ddlCruises.Items.Insert(0, "No cruise");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when Page_Load in RoomClassEdit", ex);
                ShowError(ex.Message);
            }
        }

        #endregion

        #region -- Private Method --

        private void ClearForm()
        {
            RoomClassId = -1;
            textBoxName.Text = string.Empty;
            textBoxDescription.Text = string.Empty;
            ddlCruises.SelectedIndex = 0;
        }

        private void GetDataSource()
        {
            rptRoomClass.DataSource = Module.RoomClassGetByCruise(_cruise);
            rptRoomClass.DataBind();
        }

        #endregion

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            labelFormTitle.Text = Resources.textNewRoomClass;
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxName.Text))
                {
                    if (RoomClassId > 0)
                    {
                        _roomClass = Module.RoomClassGetById(RoomClassId);
                    }
                    else
                    {
                        _roomClass = new RoomClass();
                        _roomClass.Order = Module.RooomClassCount();
                    }
                    _roomClass.Cruise = _cruise;
                    _roomClass.Name = textBoxName.Text;
                    _roomClass.Description = textBoxDescription.Text;
                    if (ddlCruises.SelectedIndex > 0)
                    {
                        _roomClass.Cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
                    }
                    else
                    {
                        _roomClass.Cruise = null;
                    }


                    if (RoomClassId < 0)
                    {
                        Module.Save(_roomClass);
                        RoomClassId = _roomClass.Id;
                    }
                    else
                    {
                        Module.Update(_roomClass);
                    }
                    GetDataSource();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when buttonSubmit_Click in RoomTypexEdit", ex);
                ShowError(ex.Message);
            }
        }

        protected void rptRoomClass_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RoomClass item = e.Item.DataItem as RoomClass;
            if (item != null)
            {
                #region Name

                using (Label label_Name = e.Item.FindControl("label_Name") as Label)
                {
                    if (label_Name != null)
                    {
                        label_Name.Text = item.Name;
                    }
                }

                #endregion

                #region Capacity

                using (Label label_Description = e.Item.FindControl("label_Description") as Label)
                {
                    if (label_Description != null)
                    {
                        label_Description.Text = item.Description;
                    }
                }

                #endregion
            }
        }

        protected void rptRoomClass_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                RoomClass item = Module.RoomClassGetById(Convert.ToInt32(e.CommandArgument));
                switch (e.CommandName)
                {
                    case "Edit":
                        textBoxName.Text = item.Name;
                        textBoxDescription.Text = item.Description;
                        RoomClassId = item.Id;
                        labelFormTitle.Text = item.Name;
                        if (item.Cruise!=null)
                        {
                            ddlCruises.SelectedValue = item.Cruise.Id.ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when rptRoomClass_ItemCommand in RoomClassEdit", ex);
                ShowError(ex.Message);
            }
        }
    }
}