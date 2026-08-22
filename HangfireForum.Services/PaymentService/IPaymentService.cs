using HangfireForum.Domain.Common.Requests;

namespace HangfireForum.Services.PaymentService
{
    public interface IPaymentService
    {
        Task ProcessPayment(PaymentRequest paymentRequest);
    }
}
