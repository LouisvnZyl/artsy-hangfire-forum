using HanfireForum.Data.DataServices;
using HangfireForum.Domain.Common.Requests;
using HangfireForum.Services.PaymentSubmissionService;
using HangfireForum.Services.SuspenseTransferService;

namespace HangfireForum.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ISuspenseTransferService _suspenseTransferService;
        private readonly IPaymentSubmissionService _paymentSubmissionService;
        private readonly IPaymentDataService _dataService;

        public PaymentService(IPaymentDataService dataService,
                              ISuspenseTransferService suspenseTransferService,
                              IPaymentSubmissionService paymentSubmissionService)
        {
            this._dataService = dataService;
            this._suspenseTransferService = suspenseTransferService;
            this._paymentSubmissionService = paymentSubmissionService;
        }

        public async Task ProcessPayment(PaymentRequest paymentRequest)
        {
            await this._dataService.InsertPayment(paymentRequest);
        }
    }
}
