using ErrorOr;
using HangfireForum.Domain.Common.Requests;

namespace HangfireForum.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<ErrorOr<Success>> ProcessPayment(PaymentRequest paymentRequest);
    }
}
