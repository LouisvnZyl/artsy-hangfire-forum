using Hangfire;
using Hangfire.Server;
using HangfireForum.BackgroundProcessing.Base;
using HangfireForum.Domain.Common.Services;

namespace HangfireForum.BackgroundProcessing.Standalone
{
    [Queue("report")]
    public class PaymentReportGenerationJob : BaseJobAsync<Guid>
    {

        private readonly IReportingService _reportingService;

        public PaymentReportGenerationJob(IReportingService reportingService)
        {
            this._reportingService = reportingService;
        }

        public override async Task ExecuteJobAsync(PerformContext context, Guid jobParams)
        {
            await this._reportingService.GenerateReport(jobParams);
        }
    }
}
