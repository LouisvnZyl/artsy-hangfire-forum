using Hangfire;

namespace HangfireForum.BackgroundProcessing.Base
{
    public class HangfireJobScheduler: IScheduler
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HangfireJobScheduler(
            IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public string Enqueue<TJob, TParams>(
            TParams jobParams)
            where TJob : BaseJobAsync<TParams>
        {
            return _backgroundJobClient.Enqueue<TJob>(
                job => job.ExecuteJobAsync(null!, jobParams));
        }
    }
}
