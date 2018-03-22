using CMS.Core.Domain;
using log4net;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Portal.Modules.OrientalSails.Web.Admin.ScheduleJob
{
    public class SendBirthdayEmailJob : IJob
    {
        private static readonly ILog log = LogManager.GetLogger(
System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Configuration config = new Configuration();
        private static ISession session =
            config.Configure(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/hibernate.cfg.xml"))
            .AddAssembly(typeof(Booking).Assembly)
            .AddAssembly(typeof(User).Assembly)
            .BuildSessionFactory().OpenSession();

        public void Execute(IJobExecutionContext context)
        {
            var criteria = session.CreateCriteria(typeof(AgencyContact));
            int month = DateTime.Today.Month;
            int day = DateTime.Today.Day;
            ICriterion criterion = Expression.Sql("DATEPART(d,Birthday) = " + day);
            criterion = Expression.And(criterion, Expression.Sql("DATEPART(m,Birthday) = " + month));
            criteria.Add(criterion);
            var agencyContactBirthday = criteria.List<AgencyContact>();

            foreach (AgencyContact agencyContact in agencyContactBirthday)
            {
                var streamReader =
            new StreamReader(
                HostingEnvironment.MapPath("/Modules/Sails/Admin/EmailTemplate/HappyBirthday.txt"));
                var content = streamReader.ReadToEnd();
                content = content.Replace("{tenkhach}", agencyContact.Name);
                SmtpClient smtpClient = new SmtpClient("mail.atravelmate.com");
                smtpClient.Credentials = new NetworkCredential("it2@atravelmate.com", "Thieudeptrai02");
                MailAddress fromAddress = new MailAddress("it2@atravelmate.com", "Orientalsails Team");
                MailMessage message = new MailMessage();
                message.From = fromAddress;
                try
                {
                    message.To.Add(agencyContact.Email);
                }catch(Exception){
                    message.To.Add("it2@atravelmate.com");
                    message.Body = "Lỗi địa chỉ email AgencyContactId: " + agencyContact.Id;
                    smtpClient.Send(message);
                }
                message.To.Add("it2@atravelmate.com");
                message.Subject = "Happy Birthday To " + agencyContact.Name;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Body = content;
                smtpClient.Send(message);
            }
        }
    }
}