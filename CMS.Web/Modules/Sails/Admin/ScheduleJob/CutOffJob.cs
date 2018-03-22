using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Hosting;
using CMS.Core.Domain;
using CMS.Web.Admin.UI;
using CMS.Web.Components;
using CMS.Web.Util;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Impl;
using Org.BouncyCastle.Asn1.Ocsp;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;
using Quartz;
using Common.Logging;

namespace Portal.Modules.OrientalSails.Web.Admin.ScheduleJob
{
    public class CutOffJob : IJob
    {
        private static readonly ILog log = LogManager.GetLogger(
 System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Configuration config = new Configuration();
        private static ISession session =
            config.Configure(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/hibernate.cfg.xml"))
            .AddAssembly(typeof(Booking).Assembly)
            .AddAssembly(typeof(User).Assembly)
            .BuildSessionFactory().OpenSession();
        /// <summary>
        /// phần xử lý của chức năng cutoff
        /// </summary>
        public void Execute(IJobExecutionContext context)
        {
            /* kết nối đến cơ sở dữ liệu */
            /* tìm những booking có trạng thái cut off */
            var criteria = session.CreateCriteria(typeof(Booking));
            criteria.Add(Expression.Eq("Status", StatusType.CutOff));
            var listBookingCutOff = criteria.List<Booking>();
            foreach (var booking in listBookingCutOff)
            {
                var cutOffDays = booking.CutOffDays; //lấy số ngày cutoff của booking
                var startDate = booking.StartDate; //lấy ngày bắt đầu chạy booking
                var daystogo = 0;
                var mailTimeSpan = startDate.AddDays(-cutOffDays);//ngày bắt đầu gửi mail là thời điểm trước ngày bắt đầu chạy booking số ngày bằng số ngày cutoff
                var daysToMail = (int)(Math.Ceiling(DateTime.Now.Subtract(mailTimeSpan).TotalDays));
                /* nếu ngày cutoff dưới 30 ngày thì sẽ gửi mail thông báo trong 3 ngày liên tục từ thời điểm bắt đầu phải gửi mail
                 * nếu sales không xác nhận trong 3 ngày đó booking tự chuyển cancelled*/
                if (cutOffDays < 30)
                {
                    if (daysToMail >= 0 && daysToMail < 3)
                    {
                        daystogo = 3 - daysToMail;
                        SendEmail(booking, context, daystogo);
                    }
                    if (daysToMail > 3)
                    {
                        booking.Status = StatusType.Cancelled;
                        session.SaveOrUpdate(booking);
                    }
                }
                /* nếu ngày cutoff lớn hơn 30 ngày thì sẽ gửi mail thông báo trong 5 ngày liên tục từ thời điểm bắt đầu phải gửi mail
                 * nếu sales không xác nhận trong 5 ngày đó booking tự chuyển cancelled*/
                else if (cutOffDays > 30)
                {
                    if (daysToMail >= 0 && daysToMail < 5)
                    {
                        daystogo = 5 - daysToMail;
                        SendEmail(booking, context, daystogo);
                    }
                    if (daysToMail > 5)
                    {
                        booking.Status = StatusType.Cancelled;
                        session.SaveOrUpdate(booking);
                    }
                }
                session.Flush();
            }
            listBookingCutOff = null;
            session.Close();
            GC.Collect();
        }

        /// <summary>
        /// chức năng gửi email nhắc nhở đến sales và reservation tự động khi đến thời hạn cutoff 
        /// </summary>
        /// <param name="booking">booking</param>
        /// <param name="context">tham khảo tại http://quartznet.sourceforge.net/apidoc/1.0/html/ </param>
        /// <param name="daystogo">số ngày còn lại</param>
        public void SendEmail(Booking booking, IJobExecutionContext context, int daystogo)
        {
            var httpContext = context.JobDetail.JobDataMap["context"] as HttpContext;
            var streamReader =
                new StreamReader(
                    HostingEnvironment.MapPath("/Modules/Sails/Admin/EmailTemplate/CutOffBookingNotify.txt"));
            var content = streamReader.ReadToEnd();
            if (httpContext != null)
            {
                var appPath = string.Format("{0}://{1}{2}{3}",
                                 httpContext.Request.Url.Scheme,
                                 httpContext.Request.Url.Host,
                                 httpContext.Request.Url.Port == 80
                                     ? string.Empty
                                     : ":" + httpContext.Request.Url.Port,
                                 httpContext.Request.ApplicationPath);
                content = content.Replace("{url}",
                    appPath + "Modules/Sails/Admin/BookingView.aspx?NodeId=1&SectionId=15&bi=" + booking.Id);
                content = content.Replace("{days}",
                    daystogo.ToString());
            }

            var smtpClient = new SmtpClient("mail.orientalsails.com", 26)
            {
                Credentials = new NetworkCredential("mo@orientalsails.com", "EGGaXBwuEWa+")

            };

            var message = new MailMessage
            {
                From = new MailAddress("mo@orientalsails.com"),
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                Body = content,
                Subject = "CutOff Date Booking Reminder"
            };

            message.To.Add(new MailAddress("reservation@orientalsails.com"));
            message.To.Add(new MailAddress("it2@atravelmate.com"));
            if (booking.BookingSale != null)
            {
                if (booking.BookingSale.Sale != null)
                {
                    message.To.Add(new MailAddress(booking.BookingSale.Sale.Email));
                }
            }
            //smtpClient.Send(message);
        }
    }
}