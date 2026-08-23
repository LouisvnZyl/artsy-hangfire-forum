using ErrorOr;
using HanfireForum.Data.DataServices;
using HanfireForum.Data.Models;
using HangfireForum.BackgroundProcessing.JobArgs;
using HangfireForum.BackgroundProcessing.Scheduler;
using HangfireForum.Domain.Common.Enums;

namespace HangfireForum.Services.PaymentSubmissionService
{
    public class PaymentSubmissionService : IPaymentSubmissionService
    {
        private readonly IPaymentDataService _dataService;
        private readonly IPaymentProcessingScheduler _paymentProcessingScheduler;

        public PaymentSubmissionService(IPaymentDataService dataService,
                                        IPaymentProcessingScheduler paymentProcessingScheduler)
        {
            this._dataService = dataService;
            this._paymentProcessingScheduler = paymentProcessingScheduler;
        }

        public async Task<ErrorOr<Success>> SubmitPayment(Guid paymentId)
        {
            var payment = await _dataService.GetPayment(paymentId);

            if (payment is null)
            {
                return Error.NotFound(
                    "Payment.NotFound",
                    $"Payment {paymentId} was not found.");
            }

            if (payment.Status != PaymentStatus.TransferredToSuspense)
            {
                return Error.Validation(
                    "Payment.InvalidStatus",
                    $"Payment is not ready for submission. Current status: {payment.Status}");
            }

            payment.MarkAsPaymentTransferringToRecipient();

            await _dataService.SaveChanges();

            await Task.Delay(2000);

            if (Random.Shared.Next(0, 10) == 0)
            {
                this._paymentProcessingScheduler.SchedulePaymentSubmissionFailure(new PaymentSubmissionJobArgs(paymentId));
            }

            var paymentSubmission = new PaymentSubmissionModel
            {
                Id = Guid.NewGuid(),
                PaymentId = payment.PaymentId,
                Amount = payment.Value,
                CreatedDate = DateTime.UtcNow
            };

            await _dataService.CreatePaymentSubmission(paymentSubmission);

            payment.MarkAsPaymentTransferredToRecipient();

            await _dataService.SaveChanges();

            this._paymentProcessingScheduler.SchedulePaymentSubmissionSuccess(new PaymentSubmissionJobArgs(paymentId));

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> HandlePaymentSubmissionSuccess(Guid paymentId)
        {
            var payment = await _dataService.GetPayment(paymentId);

            if (payment is null)
            {
                return Error.NotFound(
                    "Payment.NotFound",
                    $"Payment {paymentId} was not found.");
            }

            payment.MarkAsPaymentSuccessful();

            await this._dataService.SaveChanges();

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> HandlePaymentSubmissionFailure(Guid paymentId)
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
