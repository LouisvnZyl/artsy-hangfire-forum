using Hangfire.Server;
using HangfireForum.BackgroundProcessing.Base;
using HangfireForum.BackgroundProcessing.JobArgs;
using HangfireForum.Services.SuspenseTransferService;

namespace HangfireForum.BackgroundProcessing.Standalone
{
    public class PaymentSuspenseTransferFailureJob : BaseJobAsync<SuspenseTransferJobArgs>
    {
        private readonly ISuspenseTransferService _suspenseTransferService;

        public PaymentSuspenseTransferFailureJob(ISuspenseTransferService suspenseTransferService)
        {
            this._suspenseTransferService = suspenseTransferService;
        }

        public override async Task ExecuteJobAsync(PerformContext context, SuspenseTransferJobArgs jobParams)
        {
            var result = await this._suspenseTransferService.HandleSuspenseTransferFailure(jobParams.PaymentId);

            if (result.IsError)
            {
                throw new Exception($"Failed to transfer payment {jobParams.PaymentId} to suspense: {result.FirstError.Description}");
            }
        }
    }
}
