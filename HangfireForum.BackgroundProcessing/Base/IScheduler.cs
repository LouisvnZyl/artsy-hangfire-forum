namespace HangfireForum.BackgroundProcessing.Base
{
    public interface IScheduler
    {
        string Enqueue<TJob, TParams>(
        TParams jobParams)
        where TJob : BaseJobAsync<TParams>;
    }
}
