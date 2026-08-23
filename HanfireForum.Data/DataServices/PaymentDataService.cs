using HanfireForum.Data.EntityFramework;
using HanfireForum.Data.Models;
using HangfireForum.Domain.Common.Enums;
using HangfireForum.Domain.Common.Requests;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PaymentRequestModel?> GetPayment(Guid paymentId)
        {
            return await this._dbContext.Payments
                                        .Include(x => x.SuspenseTransaction)
                                        .Include(x => x.PaymentSubmission)
                                        .FirstOrDefaultAsync(x => x.PaymentId == paymentId);
        }

        public async Task<IEnumerable<PaymentRequestModel>> GetExpiredPayments()
        {
            var expirationTime = DateTime.UtcNow.AddMinutes(-30);

            return await this._dbContext.Payments
                                        .Where(x => (x.Status != PaymentStatus.Completed && x.Status != PaymentStatus.Failed) &&
                                                    x.SubmissionDate <= expirationTime)
                                        .ToListAsync();
        }

        public async Task CreateSuspenseTransaction(SuspenseTransactionModel suspenseTransaction)
        {
            await _dbContext.SuspenseTransactions.AddAsync(suspenseTransaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreatePaymentSubmission(PaymentSubmissionModel paymentSubmission)
        {
            await _dbContext.PaymentSubmissions.AddAsync(paymentSubmission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
