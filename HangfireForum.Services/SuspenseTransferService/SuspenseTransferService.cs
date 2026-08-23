using ErrorOr;
using HanfireForum.Data.DataServices;
using HanfireForum.Data.Models;
using HangfireForum.BackgroundProcessing.JobArgs;
using HangfireForum.BackgroundProcessing.Scheduler;

namespace HangfireForum.Services.SuspenseTransferService
{
    public class SuspenseTransferService : ISuspenseTransferService
    {
        private readonly IPaymentDataService _dataService;
        private readonly IPaymentProcessingScheduler _paymentProcessingScheduler;

        public SuspenseTransferService(IPaymentDataService dataService,
                                       IPaymentProcessingScheduler paymentProcessingScheduler)
        {
            this._dataService = dataService;
            this._paymentProcessingScheduler = paymentProcessingScheduler;
        }

        public async Task<ErrorOr<Success>> TransferToSuspense(Guid paymentId)
        {
            var payment = await _dataService.GetPayment(paymentId);

            if (payment is null)
            {
                return Error.NotFound(
                    "Payment.NotFound",
                    $"Payment {paymentId} was not found.");
            }

            payment.MarkAsPaymentTransferringToSuspense();

            await this._dataService.SaveChanges();

            await Task.Delay(300);

            // Forced Failure
            if (Random.Shared.Next(0, 10) == 0)
            {
                this._paymentProcessingScheduler.ScheduleSuspenseTransferFailure(new SuspenseTransferJobArgs(paymentId));

                return Error.Failure("Payment Suspense Transfer Failed.");
            }

            var suspenseTransaction = new SuspenseTransactionModel
            {
                Id = Guid.NewGuid(),
                PaymentId = payment.PaymentId,
                Amount = payment.Value,
                CreatedDate = DateTime.UtcNow
            };

            await _dataService.CreateSuspenseTransaction(suspenseTransaction);

            await this._dataService.SaveChanges();

            this._paymentProcessingScheduler.ScheduleSuspenseTransferSuccess(new SuspenseTransferJobArgs(paymentId));

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> HandleSuspenseTransferSuccess(Guid paymentId)
        {
            var payment = await _dataService.GetPayment(paymentId);

            if (payment is null)
            {
                return Error.NotFound(
                    "Payment.NotFound",
                    $"Payment {paymentId} was not found.");
            }

            payment.MarkAsPaymentTransferredToSuspense();

            await this._dataService.SaveChanges();

            this._paymentProcessingScheduler.SchedulePaymentSubmission(new PaymentSubmissionJobArgs(paymentId));

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> HandleSuspenseTransferFailure(Guid paymentId)
        {
            var payment = await _dataService.GetPayment(paymentId);

            if (payment is null)
            {
                return Error.NotFound(
                    "Payment.NotFound",
                    $"Payment {paymentId} was not found.");
            }

            payment.MarkAsPaymentFailed();

            await this._dataService.SaveChanges();

            return Result.Success;
        }
    }
}
