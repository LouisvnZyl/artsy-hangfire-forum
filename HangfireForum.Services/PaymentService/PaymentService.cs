using HanfireForum.Data.DataServices;
using HangfireForum.Domain.Common.Requests;

namespace HangfireForum.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDataService _dataService;

        public PaymentService(IPaymentDataService dataService)
        {
            this._dataService = dataService;
        }

        public async Task ProcessPayment(PaymentRequest paymentRequest)
        {
            await this._dataService.InsertPayment(paymentRequest);
        }
    }
}
