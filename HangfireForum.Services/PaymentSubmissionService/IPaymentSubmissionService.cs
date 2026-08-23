using ErrorOr;

namespace HangfireForum.Services.PaymentSubmissionService
{
    public interface IPaymentSubmissionService
    {
        Task<ErrorOr<Success>> SubmitPayment(Guid paymentId);
    }
}
