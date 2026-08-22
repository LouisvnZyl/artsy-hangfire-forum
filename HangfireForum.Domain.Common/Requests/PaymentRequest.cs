namespace HangfireForum.Domain.Common.Requests
{
    public class PaymentRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTimeOffset SubmissionDate { get; set; }
    }
}
