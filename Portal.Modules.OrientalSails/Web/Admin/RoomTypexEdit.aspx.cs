using System;
using System.Web.UI.WebControls;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class RoomTypexEdit : SailsAdminBase
    {
        #region -- Private Member --
        private int RoomTypexId
        {
            get
            {
                if (ViewState["RoomTypexId"]!=null)
                {
                    return Convert.ToInt32(ViewState["RoomTypexId"]);
                }
                return -1;
            }
            set { ViewState["RoomTypexId"] = value;}
        }

        private readonly ILog _logger = LogManager.GetLogger(typeof (RoomTypexEdit));
        private RoomTypex _roomTypex;
        #endregion

        #region -- Page Event --

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Title = Resources.labelRoomTypes;
                if (!IsPostBack)
                {
                    GetDataSource();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when Page_Load in RoomTypexEdit", ex);
                ShowError(ex.Message);
            }
        }
        #endregion

        #region -- Private Method --
        private void ClearForm()
        {
            RoomTypexId = -1;
            textBoxName.Text = string.Empty;
            textBoxCapacity.Text = string.Empty;
            chkAllowSingle.Checked = false;
            chkShared.Checked = false;
        }
        private void GetDataSource()
        {
            rptRoomTypex.DataSource = Module.RoomTypexGetAll();
            rptRoomTypex.DataBind();
        }
        #endregion
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxName.Text))
                {
                    if (RoomTypexId>0)
                    {
                        _roomTypex = Module.RoomTypexGetById(RoomTypexId);
                    }
                    else
                    {
                        _roomTypex= new RoomTypex();
                        _roomTypex.Order = Module.RoomTypeCount();
                    }
                    _roomTypex.Name = textBoxName.Text;
                    int capacity;
                    if (!string.IsNullOrEmpty(textBoxCapacity.Text) && Int32.TryParse(textBoxCapacity.Text,out capacity))
                    {
                        _roomTypex.Capacity = capacity;
                    }
                    else
                    {
                        _roomTypex.Capacity = 0;
                    }

                    _roomTypex.AllowSingBook = chkAllowSingle.Checked;
                    _roomTypex.IsShared = chkShared.Checked;

                    if (RoomTypexId<0)  
                    {
                        Module.Save(_roomTypex);
                        RoomTypexId = _roomTypex.Id;
                    }
                    else
                    {
                        Module.Update(_roomTypex);
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

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            labelFormTitle.Text = Resources.textNewRoomType;
        }

        protected void rptRoomTypex_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RoomTypex item = e.Item.DataItem as RoomTypex;
            if (item!=null)
            {
                #region Name

                using (Label label_Name=e.Item.FindControl("label_Name") as Label)
                {
                    if (label_Name!=null)
                    {
                        label_Name.Text = item.Name;
                    }
                }
                #endregion

                #region Capacity

                using (Label label_Capacity=e.Item.FindControl("label_Capacity") as Label)
                {
                    if (label_Capacity!=null)
                    {
                        label_Capacity.Text = item.Capacity.ToString();
                    }
                }
                #endregion
            }
        }

        protected void rptRoomTypex_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                RoomTypex item = Module.RoomTypexGetById(Convert.ToInt32(e.CommandArgument));
                switch (e.CommandName)
                {
                    case "Edit":
                        textBoxName.Text = item.Name;
                        textBoxCapacity.Text = item.Capacity.ToString();
                        chkAllowSingle.Checked = item.AllowSingBook;
                        chkShared.Checked = item.IsShared;
                        RoomTypexId = item.Id;
                        labelFormTitle.Text = item.Name;
                        break;
                    default :
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when rptRoomTypex_ItemCommand in RoomTypexEdit", ex);
                ShowError(ex.Message);
            }
        }

        protected void rptServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }
    }
}