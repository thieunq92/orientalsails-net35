using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Admin.UI;
using Portal.Modules.OrientalSails.Web.Admin.ScheduleJob;
using Portal.Modules.OrientalSails.Web.UI;
using Quartz;
using Quartz.Impl;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class JobKickStart : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            sched.DeleteJob(new JobKey("job1", "group1"));
            sched.DeleteJob(new JobKey("job2", "group1"));
            sched.DeleteJob(new JobKey("job3", "group1"));

            sched.UnscheduleJob(new TriggerKey("trigger1", "group1"));
            sched.UnscheduleJob(new TriggerKey("trigger2", "group1"));
            sched.UnscheduleJob(new TriggerKey("trigger3", "group1"));

            IJobDetail job1 = JobBuilder.Create<CutOffJob>()
                .WithIdentity("job1", "group1")
                .Build();

            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10, 0))
                .Build();

            IJobDetail job2 = JobBuilder.Create<LockBookingJob>()
              .WithIdentity("job2", "group1")
              .Build();

            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group1")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10, 0))
                .Build();

            IJobDetail job3 = JobBuilder.Create<SendBirthdayEmailJob>()
                .WithIdentity("job3", "group1")
                .Build();

            ITrigger trigger3 = TriggerBuilder.Create()
                .WithIdentity("trigger3", "group1")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10, 0))
                .Build();

            sched.ScheduleJob(job1, trigger1);
            sched.ScheduleJob(job2, trigger2);
            sched.ScheduleJob(job3, trigger3);

            txtTest.Text = "Đã hoạt động";
        }
    }
}