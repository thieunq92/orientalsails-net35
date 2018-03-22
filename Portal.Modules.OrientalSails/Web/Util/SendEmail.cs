using System.Net;
using System.Net.Mail;
using System.Text;
using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Util
{
    public class SendEmail
    {
        public void Send(string note, StatusType type, Booking booking)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("no-reply@bitcorp.vn", "8Z5235");
            smtpClient.EnableSsl = true;

            MailAddress fromAddress = new MailAddress("no-reply@bitcorp.vn");
            MailMessage message = new MailMessage();
            message.From = fromAddress;
            message.To.Add(booking.Partner.Email);
            message.Subject = "Comfirm Email";
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = GetEmailBody(note, booking, type);

            smtpClient.Send(message);
        }

        protected virtual string GetEmailBody(string note, Booking booking, StatusType type)
        {
            User partner = booking.Partner;
            string content = string.Format("Hello {0}, <br />", partner.FullName);
            switch (type)
            {
                case StatusType.Pending:
                    content =
                        string.Format(
                            "{0} Thank you for booking from OrientalSails.com.<br /> Your order number is {1} <br />",
                            content, booking.Id);
                    content =
                        string.Format(
                            "{0} Please do not reply to this email. For questions or comments, please go to OrientalSails.com. <br />",
                            content);
                    content =
                        string.Format(
                            "{0} Your order is pending. We will send you email after. <br /> This is your tour: <br /> {1}",
                            content, booking.Trip.Name);
                    content = string.Format("{0}<br /> Thank you for booking from OrientalSails.com! <br />", content);
                    break;
                case StatusType.Approved:
                    content = string.Format("{0} Your order number is {1} has been approved <br />", content, booking.Id);
                    break;
                case StatusType.Cancelled:
                    content = string.Format("{0} Your order number is {1} has been canceled <br />", content, booking.Id);
                    break;
                default:
                    break;
            }

            return content;
        }
    }


}