using ErrorOr;

namespace HangfireForum.Services.SuspenseTransferService
{
    public interface ISuspenseTransferService
    {
        Task<ErrorOr<Success>> TransferToSuspense(Guid paymentId);
        Task<ErrorOr<Success>> HandleSuspenseTransferSuccess(Guid paymentId);
        Task<ErrorOr<Success>> HandleSuspenseTransferFailure(Guid paymentId);
    }
}
