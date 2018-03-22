using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Modules.TourManagement.Domain;
using CMS.Web.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class AgencyContractEdit : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["contractid"] != null)
                {
                    var contract = Module.ContractGetById(Convert.ToInt32(Request.QueryString["contractid"]));

                    txtName.Text = contract.FileName;
                    txtExpiredDate.Text = contract.ExpiredDate.ToString("dd/MM/yyyy");

                    if (!string.IsNullOrEmpty(contract.FilePath))
                    {
                        hplOldFile.Text = contract.FileName;
                        hplOldFile.NavigateUrl = contract.FilePath;
                    }
                    else
                    {
                        hplOldFile.Visible = false;
                    }

                    if (contract.CreateDate != null)
                        litCreateDate.Text = "Created at " + contract.CreateDate.Value.ToString("dd/MM/yyyy HH:mm");

                    if (contract.Received)
                    {
                        chkReceived.Checked = true;
                    }
                }
                else
                {
                    txtExpiredDate.Text = DateTime.Today.AddYears(1).ToString("dd/MM/yyyy");
                }

                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AgencyContract contract;
            if (Request.QueryString["contractid"] != null)
            {
                contract = Module.ContractGetById(Convert.ToInt32(Request.QueryString["contractid"]));
            }
            else
            {
                contract = new AgencyContract();
                contract.Agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["agencyid"]));
            }
            contract.ContractName = txtName.Text;
            contract.ExpiredDate = DateTime.ParseExact(txtExpiredDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (fileUpload.HasFile)
            {
                contract.FilePath = FileHelper.Upload(fileUpload, "Contracts", false);
                contract.FileName = fileUpload.FileName;
            }
            contract.Received = chkReceived.Checked;
            contract.CreateDate = DateTime.Now;
            Module.SaveOrUpdate(contract);
            Page.ClientScript.RegisterStartupScript(typeof(AgencyContractEdit), "close", "window.opener.location = window.opener.location; window.close();", true);
        }
    }
}