using HangfireForum.BackgroundProcessing.JobArgs;

namespace HangfireForum.BackgroundProcessing.Scheduler
{
    public interface IPaymentProcessingScheduler
    {
        void ScheduleSuspenseTransfer(SuspenseTransferJobArgs jobParams);
        void ScheduleSuspenseTransferSuccess(SuspenseTransferJobArgs jobParams);
        void ScheduleSuspenseTransferFailure(SuspenseTransferJobArgs jobParams);

        void SchedulePaymentSubmission(PaymentSubmissionJobArgs jobParams);
        void SchedulePaymentSubmissionSuccess(PaymentSubmissionJobArgs jobParams);
        void SchedulePaymentSubmissionFailure(PaymentSubmissionJobArgs jobParams);
    }
}
