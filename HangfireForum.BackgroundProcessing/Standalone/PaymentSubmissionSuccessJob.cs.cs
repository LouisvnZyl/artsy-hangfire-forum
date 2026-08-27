using Hangfire;
using Hangfire.Server;
using HangfireForum.BackgroundProcessing.Base;
using HangfireForum.BackgroundProcessing.JobArgs;
using HangfireForum.Services.PaymentSubmissionService;

namespace HangfireForum.BackgroundProcessing.Standalone
{
    [Queue("submission")]
    public class PaymentSubmissionSuccessJob : BaseJobAsync<PaymentSubmissionJobArgs>
    {
        private readonly IPaymentSubmissionService _paymentSubmissionService;

        public PaymentSubmissionSuccessJob(IPaymentSubmissionService paymentSubmissionService)
        {
            this._paymentSubmissionService = paymentSubmissionService;
        }

        public override async Task ExecuteJobAsync(PerformContext context, PaymentSubmissionJobArgs jobParams)
        {
            var result = await this._paymentSubmissionService.HandlePaymentSubmissionSuccess(jobParams.PaymentId);

            if (result.IsError)
            {
                throw new Exception($"Failed to transfer payment {jobParams.PaymentId} to suspense: {result.FirstError.Description}");
            }
        }
    }
}
