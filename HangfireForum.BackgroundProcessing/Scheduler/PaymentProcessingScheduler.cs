using HangfireForum.BackgroundProcessing.Base;
using HangfireForum.BackgroundProcessing.JobArgs;
using HangfireForum.BackgroundProcessing.Standalone;

namespace HangfireForum.BackgroundProcessing.Scheduler
{
    public class PaymentProcessingScheduler : IPaymentProcessingScheduler
    {
        private readonly IScheduler _scheduler;

        public PaymentProcessingScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void ScheduleSuspenseTransfer(SuspenseTransferJobArgs jobParams)
        {
            this._scheduler.Enqueue<PaymentSuspenseTransferJob, SuspenseTransferJobArgs>(jobParams);
        }
    }
}
