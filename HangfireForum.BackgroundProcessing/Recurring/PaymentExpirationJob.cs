using HanfireForum.Data.DataServices;
using Hangfire;

namespace HangfireForum.BackgroundProcessing.Recurring
{
    [Queue("expiration")]
    public class PaymentExpirationJob
    {
        private readonly IPaymentDataService _paymentDataService;

        public PaymentExpirationJob(IPaymentDataService paymentDataService)
        {
            this._paymentDataService = paymentDataService;
        }

        public async Task ExecuteJobAsync()
        {
            var expiredPayments = await _paymentDataService.GetExpiredPayments();

            foreach (var payment in expiredPayments)
            {
                payment.MarkAsPaymentFailed();
            }

            await _paymentDataService.SaveChanges();
        }
    }
}
