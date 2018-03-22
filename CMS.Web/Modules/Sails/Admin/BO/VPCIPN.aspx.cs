using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using CMS.Core.Util;
using CMS.Core.Service;
using CMS.Web.Components;
using CMS.Core.Domain;
using CMS.Web.Util;
using NHibernate;
using System.Net.Mail;
using System.Net;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Admin.BO
{
    public partial class VPCIPN : System.Web.UI.Page
    {
        private static bool TripBased
        {
            get
            {
                if (string.IsNullOrEmpty(Config.GetConfiguration()["BookingMode"]))
                {
                    return true;
                }
                return Config.GetConfiguration()["BookingMode"] == "TripBased";
            }
        }

        public static CoreRepository CoreRepository
        {
            get { return HttpContext.Current.Items["CoreRepository"] as CoreRepository; }
        }

        private static ModuleLoader ModuleLoader
        {
            get
            {
                return ContainerAccessorUtil.GetContainer().Resolve<ModuleLoader>();
            }
        }

        private static Section Section { get; set; }

        private static SailsModule Module
        {
            get
            {
                return (SailsModule)ModuleLoader.GetModuleFromSection(Section);
            }
        }

        private static ISession NSession
        {
            get
            {
                return Module.CommonDao.OpenSession();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int sectionId = Int32.Parse(Request.QueryString["SectionId"]);
            Section = (Section)CoreRepository.GetObjectById(typeof(Section), sectionId);

            string SECURE_SECRET = "6D0870CDE5F24F34F3915FB0045120DB";
            string hashvalidateResult = "";
            // Khoi tao lop thu vien
            VPCRequest conn = new VPCRequest("http://onepay.vn");
            conn.SetSecureSecret(SECURE_SECRET);
            // Xu ly tham so tra ve va kiem tra chuoi du lieu ma hoa
            hashvalidateResult = conn.Process3PartyResponse(Page.Request.QueryString);
            // Lay gia tri tham so tra ve tu cong thanh toan
            String vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");
            string amount = conn.GetResultField("vpc_Amount", "Unknown");
            string localed = conn.GetResultField("vpc_Locale", "Unknown");
            string command = conn.GetResultField("vpc_Command", "Unknown");
            string version = conn.GetResultField("vpc_Version", "Unknown");
            string cardType = conn.GetResultField("vpc_Card", "Unknown");
            string orderInfo = conn.GetResultField("vpc_OrderInfo", "Unknown");
            string merchantID = conn.GetResultField("vpc_Merchant", "Unknown");
            string authorizeID = conn.GetResultField("vpc_AuthorizeId", "Unknown");
            string merchTxnRef = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
            string transactionNo = conn.GetResultField("vpc_TransactionNo", "Unknown");
            string acqResponseCode = conn.GetResultField("vpc_AcqResponseCode", "Unknown");
            string txnResponseCode = vpc_TxnResponseCode;
            string message = conn.GetResultField("vpc_Message", "Unknown");

            var bookingId = merchTxnRef;
            var pBookingId = Int32.Parse(bookingId);

            if (hashvalidateResult == "CORRECTED" && txnResponseCode.Trim() == "0")
            {
                SendSuccessEmail();
                NSession.Save(new CheckingTransaction() { MerchTxnRef = merchTxnRef, Processed = true });
                Response.Write("responsecode=1&desc=confirm-success");
            }
            else
            {
                SendFailedEmail();
                NSession.Save(new CheckingTransaction() { MerchTxnRef = merchTxnRef, Processed = true });
                Response.Write("responsecode=0&desc=confirm-fail");
            }
          
        }
        private void SendSuccessEmail()
        {
            SmtpClient smtpClient = new SmtpClient("mail.atravelmate.com");
            smtpClient.Credentials = new NetworkCredential("it2@atravelmate.com", "Thieudeptrai02");
            MailAddress fromAddress = new MailAddress("it2@atravelmate.com", "Hệ thống MO Orientalsails");
            MailMessage message = new MailMessage();
            message.From = fromAddress;
            message.To.Add("it2@atravelmate.com");
            message.Subject = "Dat Booking Thanh Cong";
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = "Dat Booking Thanh Cong";
            smtpClient.Send(message);
        }

        private void SendFailedEmail()
        {
            SmtpClient smtpClient = new SmtpClient("mail.atravelmate.com");
            smtpClient.Credentials = new NetworkCredential("it2@atravelmate.com", "Thieudeptrai02");
            MailAddress fromAddress = new MailAddress("it2@atravelmate.com", "Hệ thống MO Orientalsails");
            MailMessage message = new MailMessage();
            message.From = fromAddress;
            message.To.Add("it2@atravelmate.com");
            message.Subject = "Dat Booking Khong Thanh Cong";
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = "Dat Booking Khong Thanh Cong";
            smtpClient.Send(message);
        }

        private void DeleteBooking(int bookingId)
        {
            var booking = NSession.QueryOver<Booking>().Where(x => x.Id == bookingId).SingleOrDefault();
            var lBookingRoom = NSession.QueryOver<BookingRoom>().Where(x => x.Book.Id == bookingId).List();
            foreach (var bookingRoom in lBookingRoom)
            {
                NSession.Delete(bookingRoom);
            }
            NSession.Delete(booking);
        }
    }
}