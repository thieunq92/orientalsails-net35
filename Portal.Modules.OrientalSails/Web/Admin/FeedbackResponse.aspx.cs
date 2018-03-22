using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class FeedbackResponse : SailsAdminBasePage
    {
        private string[] _files;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _files = Directory.GetFiles(Server.MapPath("/Modules/Sails/Admin/EmailTemplate/"),
                                       string.Format("*.htm"), SearchOption.AllDirectories);
                if (Request.QueryString["date"] != null)
                {
                    IList feedback = Module.FeedbackGet(Request.QueryString);
                    rptFeedbacks.DataSource = feedback;
                    rptFeedbacks.DataBind();

                    plhSummary.Visible = true;
                    litNumberOfFeedback.Text = feedback.Count.ToString(CultureInfo.InvariantCulture);
                    litNumberOfBookings.Text = CountBooking().ToString(CultureInfo.InvariantCulture);

                    DateTime date = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["date"]));
                    txtDate.Text = date.ToString("dd/MM/yyyy");
                }
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                PageRedirect(string.Format("FeedbackResponse.aspx?NodeId={0}&SectionId={1}&date={2}", Node.Id, Section.Id, date.ToOADate()));
            }
        }

        protected void rptFeedbacks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is AnswerSheet && e.Item.DataItem!=null)
            {
                try
                {                
                var ddlSend = (DropDownList)e.Item.FindControl("ddlSend");
                var sheet = (AnswerSheet)e.Item.DataItem;
                ValueBinder.BindLiteral(e.Item, "litName", sheet.Name);
                if (sheet.Booking != null)
                {
                    ValueBinder.BindLiteral(e.Item, "litCode", string.Format(BookingFormat, sheet.Booking.Id));
                }
                ValueBinder.BindLiteral(e.Item, "litName", sheet.Name);
                if (sheet.IsSent)
                {
                    ValueBinder.BindLiteral(e.Item, "litStatus", "Sent");
                    ddlSend.SelectedValue = "0";                    
                }
                else if (string.IsNullOrEmpty(sheet.Email))
                {
                    ValueBinder.BindLiteral(e.Item, "litStatus", "No email address");
                }
                else
                {
                    ValueBinder.BindLiteral(e.Item, "litStatus", "Not yet");
                }

                DropDownList ddlTemplates = (DropDownList) e.Item.FindControl("ddlTemplates");
                ddlTemplates.Items.Clear();

                int good = 0;
                int all = 0;
                // Kiểm tra số good choice
                foreach (AnswerOption option in sheet.Options)
                {
                    if (option.Option == option.Question.Group.GoodChoice && option.Question !=null)
                    {
                        good++;
                    }
                    all++;
                }

                foreach (string file in _files)
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

                double percent = ((double)good) / all * 100;
                if (percent > 70)
                {
                    ddlTemplates.Items.FindByText("thankyou.htm").Selected = true;
                }
                else if (percent > 30)
                {
                    ddlTemplates.Items.FindByText("good.htm").Selected = true;
                }
                else
                {
                    ddlSend.SelectedValue = "0";
                }
                ValueBinder.BindLiteral(e.Item, "litOverall", percent.ToString("0.##") + "% excellent");
                }
                catch (Exception ex)
                {
                    ValueBinder.BindLiteral(e.Item, "litOverall", ex.Message);
                    return;
                }
            }
        }

        protected int CountBooking()
        {
            DateTime date = DateTime.FromOADate(Convert.ToDouble(Request.QueryString["date"]));

            // Điều kiện bắt buộc: chưa xóa và có status là Approved
            ICriterion criterion = Expression.Eq("Deleted", false);
            criterion = Expression.And(criterion, Expression.Eq("Status", StatusType.Approved));

            // Không bao gồm booking đã transfer
            criterion = Expression.And(criterion, Expression.Not(Expression.Eq("IsTransferred", true)));


            criterion = Module.AddDateExpression(criterion, date);
            int count = Module.CountBookingByCriterion(criterion);
            return count;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            Button btnSend = (Button) sender;
            RepeaterItem item = (RepeaterItem)(btnSend.Parent);
            DropDownList ddlTemplates = (DropDownList) item.FindControl("ddlTemplates");
            var ddlSend = (DropDownList) item.FindControl("ddlSend");

            StreamReader appReader = new StreamReader(ddlTemplates.SelectedValue);
            string appFormat = appReader.ReadToEnd();
            AnswerSheet sheet = Module.AnswerSheetGetById(Convert.ToInt32(btnSend.CommandArgument));

            if (string.IsNullOrEmpty(sheet.Email))
            {
                return;
            }
            if (ddlSend.SelectedValue == "0")
            {
                return;
            }
            string content = appFormat.Replace("[NAME]", sheet.Name);

            // Đăng nhập            
            SmtpClient smtpClient = new SmtpClient("mail.orientalsails.com");
            smtpClient.Credentials = new NetworkCredential("marketing@orientalsails.com", "9RAfuweC");
            //smtpClient.EnableSsl = true;

            // Địa chỉ email người gửi
            //MailAddress fromAddress = new MailAddress(UserIdentity.Email);
            MailAddress fromAddress = new MailAddress("marketing@orientalsails.com");           

            MailMessage message = new MailMessage();
            message.From = fromAddress;
            message.To.Add(sheet.Email);
            message.Subject = @"Thank you for using Oriental Sails";
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = content;
            message.Bcc.Add(UserIdentity.Email);
            message.Bcc.Add("thach@etravelvn.com");

            smtpClient.Send(message);
            ClientScript.RegisterClientScriptBlock(typeof(FeedbackMail), "closure", "window.close()", true);
            
            sheet.IsSent = true;
            Module.SaveOrUpdate(sheet);

            ShowMessage("Email sent!");
        }

        protected void btnSendAll_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptFeedbacks.Items)
            {
                Button btnSend = (Button) item.FindControl("btnSend");
                btnSend_Click(btnSend, new EventArgs());
            }

            ShowMessage("All email sent!");
        }
    }
}