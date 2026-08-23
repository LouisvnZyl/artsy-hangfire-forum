using HangfireForum.BackgroundProcessing.JobArgs;

namespace HangfireForum.BackgroundProcessing.Scheduler
{
    public interface IPaymentProcessingScheduler
    {
        void ScheduleSuspenseTransfer(SuspenseTransferJobArgs jobParams);
    }
}
