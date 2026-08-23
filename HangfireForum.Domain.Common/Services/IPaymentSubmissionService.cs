using ErrorOr;

namespace HangfireForum.Services.PaymentSubmissionService
{
    public interface IPaymentSubmissionService
    {
        Task<ErrorOr<Success>> SubmitPayment(Guid paymentId);
        Task<ErrorOr<Success>> HandlePaymentSubmissionSuccess(Guid paymentId);
        Task<ErrorOr<Success>> HandlePaymentSubmissionFailure(Guid paymentId);
    }
}
