using Hangfire;
using Hangfire.Server;
using HangfireForum.BackgroundProcessing.Base;
using HangfireForum.BackgroundProcessing.JobArgs;
using HangfireForum.Services.SuspenseTransferService;

namespace HangfireForum.BackgroundProcessing.Standalone
{
    [Queue("suspense")]
    public class PaymentSuspenseTransferSuccessJob : BaseJobAsync<SuspenseTransferJobArgs>
    {
        private readonly ISuspenseTransferService _suspenseTransferService;

        public PaymentSuspenseTransferSuccessJob(ISuspenseTransferService suspenseTransferService)
        {
            this._suspenseTransferService = suspenseTransferService;
        }

        public override async Task ExecuteJobAsync(PerformContext context, SuspenseTransferJobArgs jobParams)
        {
            var result = await this._suspenseTransferService.HandleSuspenseTransferSuccess(jobParams.PaymentId);

            if (result.IsError)
            {
                throw new Exception($"Failed to transfer payment {jobParams.PaymentId} to suspense: {result.FirstError.Description}");
            }
        }
    }
}
