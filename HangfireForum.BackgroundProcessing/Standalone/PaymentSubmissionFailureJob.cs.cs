using Hangfire.Server;
using HangfireForum.BackgroundProcessing.Base;
using HangfireForum.BackgroundProcessing.JobArgs;
using HangfireForum.Services.PaymentSubmissionService;

namespace HangfireForum.BackgroundProcessing.Standalone
{
    public class PaymentSubmissionFailureJob : BaseJobAsync<PaymentSubmissionJobArgs>
    {
        private readonly IPaymentSubmissionService _paymentSubmissionService;

        public PaymentSubmissionFailureJob(IPaymentSubmissionService paymentSubmissionService)
        {
            this._paymentSubmissionService = paymentSubmissionService;
        }

        public override async Task ExecuteJobAsync(PerformContext context, PaymentSubmissionJobArgs jobParams)
        {
            var result = await this._paymentSubmissionService.HandlePaymentSubmissionFailure(jobParams.PaymentId);

            if (result.IsError)
            {
                throw new Exception($"Failed to transfer payment {jobParams.PaymentId} to suspense: {result.FirstError.Description}");
            }
        }
    }
}
