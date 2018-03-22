using System;
using System.Web.UI.WebControls;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class ExtraOptionEdit : SailsAdminBase
    {
        #region -- Private Member --

        private readonly ILog _logger = LogManager.GetLogger(typeof (ExtraOptionEdit));

        private ExtraOption _extraOption;

        private int ExtraId
        {
            get
            {
                if (ViewState["ExtraId"] != null)
                {
                    return Convert.ToInt32(ViewState["ExtraId"]);
                }
                return -1;
            }
            set { ViewState["ExtraId"] = value; }
        }

        #endregion

        #region -- Page Event --

        protected void Page_Load(object sender, EventArgs e)
        {
            string expense = string.Format("CostTypes.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);
            string config = string.Format("Costing.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);
            labelNote.Text = string.Format(Resources.textExtraNote, expense, config);
            try
            {
                Title = Resources.titleExtraOptionEdit;
                if (!IsPostBack)
                {
                    GetDataSource();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when Page_Load in ExtraOptionEdit", ex);
                ShowError(ex.Message);
            }
        }

        #endregion

        #region -- Private Method --

        private void ClearForm()
        {
            ExtraId = -1;
            textBoxName.Text = string.Empty;
            textBoxPrice.Text = string.Empty;
            chkIncluded.Checked = false;
            textBoxDescription.Text = string.Empty;
        }

        private void GetDataSource()
        {
            rptExtraOption.DataSource = Module.ExtraOptionGetAll();
            rptExtraOption.DataBind();
        }

        #endregion

        #region -- Control Event --

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxName.Text))
                {
                    if (ExtraId > 0)
                    {
                        _extraOption = Module.ExtraOptionGetById(ExtraId);
                    }
                    else
                    {
                        _extraOption = new ExtraOption();
                    }
                    _extraOption.Name = textBoxName.Text;
                    _extraOption.Description = textBoxDescription.Text;
                    _extraOption.Target = (ServiceTarget) ddlTargets.SelectedIndex;
                    double price;
                    if (!string.IsNullOrEmpty(textBoxPrice.Text) && double.TryParse(textBoxPrice.Text, out price))
                    {
                        _extraOption.Price = price;
                    }
                    else
                    {
                        _extraOption.Price = 0;
                    }

                    _extraOption.IsIncluded = chkIncluded.Checked;

                    if (ExtraId < 0)
                    {
                        Module.Save(_extraOption);
                        ExtraId = _extraOption.Id;
                    }
                    else
                    {
                        Module.Update(_extraOption);
                    }
                    GetDataSource();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when buttonSubmit_Click in ExtraOptionEdit", ex);
                ShowError(ex.Message);
            }
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            labelStatus.Text = Resources.stringAddnewExtraOption;
        }

        protected void rptExtraOption_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ExtraOption item = e.Item.DataItem as ExtraOption;
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

                #region Price 

                using (Label label_Price = e.Item.FindControl("label_Price") as Label)
                {
                    if (label_Price != null)
                    {
                        label_Price.Text = item.Price.ToString("####");
                    }
                }

                #endregion

                //#region Description

                //using (Label label_Description = e.Item.FindControl("label_Description") as Label)
                //{
                //    if (label_Description != null)
                //    {
                //        label_Description.Text = item.Description;
                //    }
                //}

                //#endregion
            }
        }

        protected void rptExtraOption_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                ExtraOption item = Module.ExtraOptionGetById(Convert.ToInt32(e.CommandArgument));
                switch (e.CommandName.ToLower())
                {
                    case "edit":
                        textBoxName.Text = item.Name;
                        textBoxPrice.Text = item.Price.ToString("####");
                        chkIncluded.Checked = item.IsIncluded;
                        textBoxDescription.Text = item.Description;
                        ddlTargets.SelectedIndex = (int) item.Target;
                        ExtraId = item.Id;
                        labelStatus.Text = item.Name;
                        break;
                    case "delete":
                        item.Deleted = true;
                        Module.SaveOrUpdate(item);
                        GetDataSource();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when rptExtraOption_ItemCommand in ExtraOptionEdit", ex);
                ShowError(ex.Message);
            }
        }

        #endregion
    }
}