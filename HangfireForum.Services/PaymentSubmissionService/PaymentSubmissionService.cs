using ErrorOr;
using HanfireForum.Data.DataServices;
using HanfireForum.Data.Models;
using HangfireForum.Domain.Common.Enums;

namespace HangfireForum.Services.PaymentSubmissionService
{
    public class PaymentSubmissionService: IPaymentSubmissionService
    {
        private readonly IPaymentDataService _dataService;

        public PaymentSubmissionService(IPaymentDataService dataService)
        {
            this._dataService = dataService;
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

            payment.Status = PaymentStatus.TransferringToRecipient;

            await _dataService.SaveChanges();

            // Simulate processing
            await Task.Delay(200);

            // 50% chance of failure
            if (Random.Shared.Next(0, 2) == 0)
            {
                payment.Status = PaymentStatus.Failed;

                await _dataService.SaveChanges();

                return Error.Failure(
                    "PaymentSubmission.Failed",
                    "The payment submission failed.");
            }

            var paymentSubmission = new PaymentSubmissionModel
            {
                Id = Guid.NewGuid(),
                PaymentId = payment.PaymentId,
                Amount = payment.Value,
                CreatedDate = DateTime.UtcNow
            };

            await _dataService.CreatePaymentSubmission(paymentSubmission);

            payment.Status = PaymentStatus.PaymentPending;

            await _dataService.SaveChanges();

            return Result.Success;
        }
    }
}
