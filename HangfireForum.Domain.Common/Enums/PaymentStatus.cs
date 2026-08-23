namespace HangfireForum.Domain.Common.Enums
{
    public enum PaymentStatus
    {
        Initiated,

        Received,
        ValidationFailed,
        Validated,

        TransferringToSuspense,
        TransferredToSuspense,

        TransferringToRecipient,
        PaymentPending,

        Completed,
        Failed
    }
}
