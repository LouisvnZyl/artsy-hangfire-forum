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

        public void ScheduleSuspenseTransferFailure(SuspenseTransferJobArgs jobParams)
        {
            throw new NotImplementedException();
        }

        public void ScheduleSuspenseTransferSuccess(SuspenseTransferJobArgs jobParams)
        {
            throw new NotImplementedException();
        }

        public void SchedulePaymentSubmission(PaymentSubmissionJobArgs jobParams)
        {
            throw new NotImplementedException();
        }

        public void SchedulePaymentSubmissionFailure(PaymentSubmissionJobArgs jobParams)
        {
            throw new NotImplementedException();
        }

        public void SchedulePaymentSubmissionSuccess(PaymentSubmissionJobArgs jobParams)
        {
            throw new NotImplementedException();
        }
    }
}
