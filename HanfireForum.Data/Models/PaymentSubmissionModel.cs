namespace HanfireForum.Data.Models
{
    public class PaymentSubmissionModel
    {
        public Guid Id { get; set; }

        public Guid PaymentId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedDate { get; set; }

        public PaymentRequestModel Payment { get; set; } = null!;
    }
}
