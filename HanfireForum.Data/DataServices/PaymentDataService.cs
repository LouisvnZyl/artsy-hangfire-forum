using HanfireForum.Data.EntityFramework;
using HanfireForum.Data.Models;
using HangfireForum.Domain.Common.Enums;
using HangfireForum.Domain.Common.Requests;

namespace HanfireForum.Data.DataServices
{
    public class PaymentDataService: IPaymentDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentDataService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task InsertPayment(PaymentRequest paymentRequest)
        {
            try
            {
                var paymentModel = new PaymentRequestModel
                {
                    PaymentId = paymentRequest.PaymentId,
                    Name = paymentRequest.Name,
                    SubmissionDate = paymentRequest.SubmissionDate,
                    Value = paymentRequest.Value,
                    Status = PaymentStatus.Initiated
                };

                await this._dbContext.AddAsync(paymentModel);

                await this._dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
