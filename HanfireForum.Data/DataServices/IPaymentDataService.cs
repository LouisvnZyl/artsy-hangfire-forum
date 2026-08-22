using HangfireForum.Domain.Common.Requests;

namespace HanfireForum.Data.DataServices
{
    public interface IPaymentDataService
    {
        Task InsertPayment(PaymentRequest paymentRequest);
    }
}
