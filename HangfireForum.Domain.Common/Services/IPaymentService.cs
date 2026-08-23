using ErrorOr;
using HangfireForum.Domain.Common.Requests;

namespace HangfireForum.Domain.Common.Services
{
    public interface IPaymentService
    {
        Task<ErrorOr<Success>> ProcessPayment(PaymentRequest paymentRequest);
    }
}
