using ErrorOr;
using HangfireForum.Domain.Common.Requests;

namespace HangfireForum.Services.Validation
{
    public class PaymentValidationService: IPaymentValidationService
    {
        public async Task<ErrorOr<Success>> ValidatePaymentRequest(PaymentRequest paymentRequest)
        {
            await Task.Delay(200);

            if (paymentRequest == null)
                return Error.Failure("No Request Found");

            if (paymentRequest.PaymentId == Guid.Empty)
                return Error.Failure("No Request Found");

            if (string.IsNullOrWhiteSpace(paymentRequest.Name))
                return Error.Failure("No Request Found");

            if (paymentRequest.Value <= 0)
                return Error.Failure("No Request Found");

            if (paymentRequest.SubmissionDate == default)
                return Error.Failure("No Request Found");

            return Result.Success;
        }
    }
}
