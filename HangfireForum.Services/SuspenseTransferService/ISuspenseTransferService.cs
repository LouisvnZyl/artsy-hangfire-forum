using ErrorOr;

namespace HangfireForum.Services.SuspenseTransferService
{
    public interface ISuspenseTransferService
    {
        Task<ErrorOr<Success>> TransferToSuspense(Guid paymentId);
    }
}
