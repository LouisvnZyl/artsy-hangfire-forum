namespace HangfireForum.Domain.Common.Services
{
    public interface IReportingService
    {
        Task GenerateReport(Guid paymentId);
    }
}
