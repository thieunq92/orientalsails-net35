using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class VoucherTemplates : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] files = Directory.GetFiles(Server.MapPath("/UserFiles/VoucherTemplates/"),
                                       string.Format("*.doc"), SearchOption.AllDirectories);
                rptFiles.DataSource = files;
                rptFiles.DataBind();
            }
        }

        protected void rptFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is string)
            {
                string filename = Path.GetFileName(e.Item.DataItem.ToString());
                ValueBinder.BindLiteral(e.Item, "litFileName", filename);

                var hplDownload = e.Item.FindControl("hplDownload") as HyperLink;
                if (hplDownload != null)
                {
                    hplDownload.NavigateUrl = "/UserFiles/VoucherTemplates/" + filename;
                }

                var lbtDelete = e.Item.FindControl("lbtDelete") as LinkButton;
                if (lbtDelete != null)
                {
                    lbtDelete.CommandArgument = filename;
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUploadTemplate.HasFile)
            {
                FileHelper.Upload(fileUploadTemplate, "VoucherTemplates/", false);
            }

            PageRedirect(Request.RawUrl);
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            LinkButton lbtDelete = (LinkButton) sender;
            string path = Server.MapPath("/UserFiles/VoucherTemplates/" + lbtDelete.CommandArgument);
            
            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            PageRedirect(Request.RawUrl);
        }
    }
}