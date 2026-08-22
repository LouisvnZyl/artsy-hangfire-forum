using HangfireForum.Domain.Common.Enums;

namespace HanfireForum.Data.Models
{
    public class PaymentRequestModel
    {
        public int PaymentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTimeOffset SubmissionDate { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
