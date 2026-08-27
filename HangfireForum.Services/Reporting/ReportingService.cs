using HangfireForum.Domain.Common.Services;

namespace HangfireForum.Services.Reporting
{
    public class ReportingService : IReportingService
    {
        public async Task GenerateReport(Guid paymentId)
        {
            await Task.Delay(10000);
        }
    }
}
