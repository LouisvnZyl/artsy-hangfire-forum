using ErrorOr;
using HanfireForum.Data.DataServices;
using HanfireForum.Data.Models;
using HangfireForum.Domain.Common.Enums;

namespace HangfireForum.Services.SuspenseTransferService
{
    public class SuspenseTransferService : ISuspenseTransferService
    {
        private readonly IPaymentDataService _dataService;

        public SuspenseTransferService(IPaymentDataService dataService)
        {
            _dataService = dataService;
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

            payment.Status = PaymentStatus.TransferringToSuspense;

            await this._dataService.SaveChanges();

            await Task.Delay(300);

            if (Random.Shared.Next(0, 10) == 0)
            {
                payment.Status = PaymentStatus.Failed;

                await this._dataService.SaveChanges();

                return Error.Failure(
                    "SuspenseTransfer.Failed",
                    "The transfer to suspense failed.");
            }

            var suspenseTransaction = new SuspenseTransactionModel
            {
                Id = Guid.NewGuid(),
                PaymentId = payment.PaymentId,
                Amount = payment.Value,
                CreatedDate = DateTime.UtcNow
            };

            await _dataService.CreateSuspenseTransaction(suspenseTransaction);

            payment.Status = PaymentStatus.TransferredToSuspense;

            await this._dataService.SaveChanges();

            return Result.Success;
        }
    }
}
