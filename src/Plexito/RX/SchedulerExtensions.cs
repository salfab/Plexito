namespace Plexito.RX
{
    using System;
    using System.Reactive.Concurrency;

    public static class SchedulerExtensions
    {
        public static IDisposable ScheduleRecurringAction(this IScheduler scheduler, TimeSpan interval, Action action)
        {
            return scheduler.Schedule(interval, scheduleNext =>
            {
                action();
                scheduleNext(interval);
            });
        }    
    }
}