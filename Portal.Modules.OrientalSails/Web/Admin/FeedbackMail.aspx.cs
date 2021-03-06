using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class FeedbackMail : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AnswerSheet sheet = Module.AnswerSheetGetById(Convert.ToInt32(Request.QueryString["sheetid"]));
                lblEmailTo.Text = sheet.Email;
                txtSubject.Text = @"Thank you for using Oriental Sails";

                string[] files = Directory.GetFiles(Server.MapPath("/Modules/Sails/Admin/EmailTemplate/"),
                                       string.Format("*.htm"), SearchOption.AllDirectories);
                ddlTemplates.Items.Clear();

                foreach (string file in files)
                {
                    string filename = Path.GetFileName(file);
                    if (!string.IsNullOrEmpty(filename))
                    {
                        try
                        {
                            string name = Path.GetFileName(file);
                            ddlTemplates.Items.Add(new ListItem(name, file));
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

                StreamReader appReader = new StreamReader(Server.MapPath("/Modules/Sails/Admin/EmailTemplate/Thankyou.htm"));
                string appFormat = appReader.ReadToEnd();
                fckContent.Value = appFormat.Replace("[NAME]", sheet.Name);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            // Đăng nhập            
            SmtpClient smtpClient = new SmtpClient("mail.orientalsails.com");
            smtpClient.Credentials = new NetworkCredential("marketing@orientalsails.com", "9RAfuweC");
            //smtpClient.EnableSsl = true;

            // Địa chỉ email người gửi
            //MailAddress fromAddress = new MailAddress(UserIdentity.Email);
            MailAddress fromAddress = new MailAddress("marketing@orientalsails.com");

            MailMessage message = new MailMessage();
            message.From = fromAddress;
            message.To.Add(lblEmailTo.Text);
            message.Subject = txtSubject.Text;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = fckContent.Value;
            message.CC.Add(UserIdentity.Email);

            smtpClient.Send(message);
            ClientScript.RegisterClientScriptBlock(typeof(FeedbackMail), "closure", "window.close()", true);

            AnswerSheet sheet = Module.AnswerSheetGetById(Convert.ToInt32(Request.QueryString["sheetid"]));
            sheet.IsSent = true;
            Module.SaveOrUpdate(sheet);
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            StreamReader appReader = new StreamReader(ddlTemplates.SelectedValue);
            string appFormat = appReader.ReadToEnd();
            AnswerSheet sheet = Module.AnswerSheetGetById(Convert.ToInt32(Request.QueryString["sheetid"]));
            fckContent.Value = appFormat.Replace("[NAME]", sheet.Name);
        }
    }
}
