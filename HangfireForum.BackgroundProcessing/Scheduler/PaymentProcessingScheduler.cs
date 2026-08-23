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
            this._scheduler.Enqueue<PaymentSuspenseTransferFailureJob, SuspenseTransferJobArgs>(jobParams);
        }

        public void ScheduleSuspenseTransferSuccess(SuspenseTransferJobArgs jobParams)
        {
            this._scheduler.Enqueue<PaymentSuspenseTransferSuccessJob, SuspenseTransferJobArgs>(jobParams);
        }

        public void SchedulePaymentSubmission(PaymentSubmissionJobArgs jobParams)
        {
            this._scheduler.Enqueue<PaymentSubmissionJob, PaymentSubmissionJobArgs>(jobParams);
        }

        public void SchedulePaymentSubmissionFailure(PaymentSubmissionJobArgs jobParams)
        {
            this._scheduler.Enqueue<PaymentSubmissionFailureJob, PaymentSubmissionJobArgs>(jobParams);
        }

        public void SchedulePaymentSubmissionSuccess(PaymentSubmissionJobArgs jobParams)
        {
            this._scheduler.Enqueue<PaymentSubmissionSuccessJob, PaymentSubmissionJobArgs>(jobParams);
        }
    }
}
