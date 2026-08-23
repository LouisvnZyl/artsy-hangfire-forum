using ErrorOr;
using HangfireForum.Domain.Common.Requests;

namespace HangfireForum.Services.Validation
{
    public interface IPaymentValidationService
    {
        Task<ErrorOr<Success>> ValidatePaymentRequest(PaymentRequest paymentRequest);
    }
}
