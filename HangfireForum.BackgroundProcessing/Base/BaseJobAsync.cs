using ErrorOr;
using Hangfire.Server;

namespace HangfireForum.BackgroundProcessing.Base
{
    public abstract class BaseJobAsync<T>
    {
        public virtual void Dispose() { }

        public abstract Task ExecuteJobAsync(PerformContext context, T jobParams);

        private const int RetryCountBreach = int.MaxValue;

        protected void RetryIfTooManyRequests(PerformContext context, ErrorOr<Success> result)
        {
            if (!result.IsError)
            {
                return;
            }

            throw new Exception(result.FirstError.Description);
        }

        protected void LogAndThrowError<TResult>(ErrorOr<TResult> error)
        {
            throw new Exception(error.FirstError.Description);
        }
    }
}
