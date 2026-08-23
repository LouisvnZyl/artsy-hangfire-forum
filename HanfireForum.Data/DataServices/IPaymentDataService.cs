using HanfireForum.Data.Models;
using HangfireForum.Domain.Common.Requests;

namespace HanfireForum.Data.DataServices
{
    public interface IPaymentDataService
    {
        Task InsertPayment(PaymentRequest paymentRequest);
        Task<PaymentRequestModel?> GetPayment(Guid paymentId);
        Task CreateSuspenseTransaction(SuspenseTransactionModel suspenseTransaction);
        Task CreatePaymentSubmission(PaymentSubmissionModel paymentSubmission);
        Task<IEnumerable<PaymentRequestModel>> GetExpiredPayments();
        Task SaveChanges();
    }
}
