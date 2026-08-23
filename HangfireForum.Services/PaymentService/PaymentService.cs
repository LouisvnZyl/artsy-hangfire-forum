using ErrorOr;
using HanfireForum.Data.DataServices;
using HangfireForum.BackgroundProcessing.JobArgs;
using HangfireForum.BackgroundProcessing.Scheduler;
using HangfireForum.Domain.Common.Requests;
using HangfireForum.Domain.Common.Services;
using HangfireForum.Services.PaymentSubmissionService;
using HangfireForum.Services.SuspenseTransferService;
using HangfireForum.Services.Validation;

namespace HangfireForum.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentValidationService _validationService;
        private readonly ISuspenseTransferService _suspenseTransferService;
        private readonly IPaymentSubmissionService _paymentSubmissionService;
        private readonly IPaymentDataService _dataService;
        private readonly IPaymentProcessingScheduler _paymentProcessingScheduler;

        public PaymentService(IPaymentValidationService validationService,
                              IPaymentDataService dataService,
                              ISuspenseTransferService suspenseTransferService,
                              IPaymentSubmissionService paymentSubmissionService,
                              IPaymentProcessingScheduler paymentProcessingScheduler)
        {
            this._validationService = validationService;
            this._dataService = dataService;
            this._suspenseTransferService = suspenseTransferService;
            this._paymentSubmissionService = paymentSubmissionService;
            this._paymentProcessingScheduler = paymentProcessingScheduler;
        }

        public async Task<ErrorOr<Success>> ProcessPayment(PaymentRequest paymentRequest)
        {
            var validationResult = await this._validationService.ValidatePaymentRequest(paymentRequest);

            if (validationResult.IsError)
            {
                return validationResult.Errors;
            }

            await this._dataService.InsertPayment(paymentRequest);

            this._paymentProcessingScheduler.ScheduleSuspenseTransfer(new SuspenseTransferJobArgs(paymentRequest.PaymentId));

            //var suspenseTransferResult = await this._suspenseTransferService.TransferToSuspense(paymentRequest.PaymentId);

            //if (suspenseTransferResult.IsError)
            //{
            //    return suspenseTransferResult.Errors;
            //}

            //var paymentSubmissionResult = await this._paymentSubmissionService.SubmitPayment(paymentRequest.PaymentId);

            //if (paymentSubmissionResult.IsError)
            //{
            //    return paymentSubmissionResult.Errors;
            //}

            return Result.Success;
        }
    }
}
