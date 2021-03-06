using System;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class Purposes : SailsAdminBasePage
    {
        #region -- PRIVATE MEMBERS --

        private Purpose _activeCost;

        /// <summary>
        /// Biến ViewState lưu Service hiện tại
        /// </summary>
        private Purpose ActiveCost
        {
            get
            {
                if (_activeCost != null)
                {
                    return _activeCost;
                }
                if (ViewState["serviceId"] != null && Convert.ToInt32(ViewState["serviceId"]) > 0)
                {
                    return Module.PurposeGetById(Convert.ToInt32(ViewState["serviceId"]));
                }
                _activeCost = new Purpose();
                return _activeCost;
            }
            set
            {
                _activeCost = value;
                ViewState["serviceId"] = value.Id;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Services";
            if (!IsPostBack)
            {
                rptPurposes.DataSource = Module.PurposeGetAll();
                rptPurposes.DataBind();
                labelFormTitle.Text = "New Purpose";
                btnDelete.Visible = false;
                btnDelete.Enabled = false;
            }
        }

        protected void rptPurposes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Purpose service = e.Item.DataItem as Purpose;
            if (service != null)
            {
                using (LinkButton lbtEdit = e.Item.FindControl("lbtEdit") as LinkButton)
                {
                    if (lbtEdit != null)
                    {
                        // Gán text và command argument, điều này cũng có thể làm ngay trên aspx
                        lbtEdit.Text = service.Name;
                        lbtEdit.CommandArgument = service.Id.ToString();
                    }
                }
            }
        }

        protected void rptPurposes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "edit":

                    #region -- Lấy thông tin dịch vụ

                    ActiveCost = Module.PurposeGetById(Convert.ToInt32(e.CommandArgument));
                    txtServiceName.Text = ActiveCost.Name;
                    txtCode.Text = ActiveCost.Code;

                    #endregion

                    btnDelete.Visible = true;
                    btnDelete.Enabled = true;
                    labelFormTitle.Text = ActiveCost.Name;
                    break;
                default:
                    break;
            }
        }

        #region -- Insert, Update, Delete --

        /// <summary>
        /// Khi ấn nút Save, lưu Service nếu đang edit, insert nếu đang thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ActiveCost.Name = txtServiceName.Text;
            ActiveCost.Code = txtCode.Text;

            // Kiểm tra trong View State
            Module.SaveOrUpdate(ActiveCost);
            ActiveCost = ActiveCost;
            labelFormTitle.Text = ActiveCost.Name;
            rptPurposes.DataSource = Module.PurposeGetAll();
            rptPurposes.DataBind();
        }

        /// <summary>
        /// Khi ấn nút Add New, đưa trạng thái trở về thêm mới, xóa các textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ActiveCost = new Purpose();
            txtServiceName.Text = "New Purpose";
            txtCode.Text = string.Empty;
            labelFormTitle.Text = "New service";
            btnDelete.Visible = false;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// Khi ấn nút Delete, xóa service hiện thời (lưu trong ViewState)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Module.Delete(ActiveCost);
            btnAdd_Click(sender, e);
            rptPurposes.DataSource = Module.PurposeGetAll();
            rptPurposes.DataBind();
        }

        #endregion
    }
}