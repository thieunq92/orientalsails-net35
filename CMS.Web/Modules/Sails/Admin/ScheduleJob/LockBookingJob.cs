using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using CMS.Core.Domain;
using log4net;
using log4net.Repository.Hierarchy;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Impl;
using Portal.Modules.OrientalSails.Web.Util;
using Quartz;
using Portal.Modules.OrientalSails.Domain;
using NHibernate;

namespace Portal.Modules.OrientalSails.Web.Admin.ScheduleJob
{
    public class LockBookingJob : IJob
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
        /// xử lý chức năng lock booking
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            /* tìm những booking chưa bị lock */
            var criteria = session.CreateCriteria(typeof(Booking));
            criteria.Add(Expression.IsNull("LockDate"));
            criteria.Add(Expression.Ge("StartDate", new DateTime(2015, 10, 7)));
            criteria.Add(Expression.Le("StartDate", DateTime.Today));
            var notLockBookings = criteria.List<Booking>();
            foreach (var notLockBooking in notLockBookings)
            {
                var dateToLock = (int)(Math.Floor(DateTime.Now.Subtract(notLockBooking.EndDate).TotalDays));
                var paymentPeriod = notLockBooking.Agency.PaymentPeriod;
                //if (paymentPeriod == PaymentPeriod.None)
                //{
                /* nếu payment period của agency là none thì sau khi booking kết thúc 2 ngày sẽ bị tự động khóa */
                if (dateToLock > 2)
                {
                    notLockBooking.LockDate = DateTime.Now;
                    session.SaveOrUpdate(notLockBooking);
                    session.Flush();
                }
                //}
                /* nếu payment period của agency là monthly thì sau khi booking kết thúc 1 tháng sẽ bị tự động khóa */
                //if (paymentPeriod == PaymentPeriod.Monthly || paymentPeriod == PaymentPeriod.MonthlyCK)
                //{
                //    if (dateToLock >= 31)
                //    {
                //        notLockBooking.LockDate = DateTime.Now;
                //        session.SaveOrUpdate(notLockBooking);
                //        session.Flush();
                //    }
                //} 
            }
            criteria = null;
            notLockBookings = null;
        }
    }
}