﻿using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Shiny.Infrastructure;


namespace Shiny.Jobs
{
    public class JobManager : AbstractJobManager
    {
        public JobManager(IServiceProvider container, IRepository repository) : base(container, repository, TimeSpan.FromMinutes(15))
        {
        }


        public override async Task<AccessState> RequestAccess()
        {
            var requestStatus = await BackgroundExecutionManager.RequestAccessAsync();
            switch (requestStatus)
            {
                //case BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity:
                //case BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity:
                case BackgroundAccessStatus.AllowedSubjectToSystemPolicy:
                case BackgroundAccessStatus.AlwaysAllowed:
                    return AccessState.Available;

                default:
                    return AccessState.Denied;
            }
        }


        protected override void ScheduleNative(JobInfo jobInfo)
        {
            //this.context.RegisterBackground<JobBackgroundTaskProcessor>(typeof(Shiny.ShinyBackgroundTask), jobInfo.Identifier, builder =>
            //{
            //    var runMins = Convert.ToUInt32(Math.Round(jobInfo.PeriodicTime.TotalMinutes, 0));
            //    builder.SetTrigger(new TimeTrigger(runMins, false));

            //    if (jobInfo.RequiredInternetAccess != InternetAccess.None)
            //    {
            //        var type = jobInfo.RequiredInternetAccess == InternetAccess.Any
            //            ? SystemConditionType.InternetAvailable
            //            : SystemConditionType.FreeNetworkAvailable;

            //        builder.AddCondition(new SystemCondition(type));
            //    }
            //});
        }


        protected override void CancelNative(JobInfo jobInfo) { }
     //       => this.context.UnRegisterBackground<JobBackgroundTaskProcessor>(jobInfo.Identifier);
    }
}

