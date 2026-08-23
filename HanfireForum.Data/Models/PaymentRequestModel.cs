using HangfireForum.Domain.Common.Enums;

namespace HanfireForum.Data.Models
{
    public class PaymentRequestModel
    {
        public Guid PaymentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTimeOffset SubmissionDate { get; set; }
        public PaymentStatus Status { get; set; }
        public SuspenseTransactionModel? SuspenseTransaction { get; set; }

        public PaymentSubmissionModel? PaymentSubmission { get; set; }

        public void MarkAsPaymentTransferringToSuspense()
        {
            this.Status = PaymentStatus.TransferringToSuspense;
        }

        public void MarkAsPaymentTransferredToSuspense()
        {
            this.Status = PaymentStatus.TransferredToSuspense;
        }

        public void MarkAsPaymentSuccessful()
        {
            this.Status = PaymentStatus.Completed;
        }

        public void MarkAsPaymentFailed()
        {
            this.Status = PaymentStatus.Failed;
        }
    }
}
