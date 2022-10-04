namespace DDDAccountBalance.Logic.Events
{
    public class OverDraftLimitUpdated : IEvent
    {
        public Guid AccountId { get; set; }
        public decimal OverdraftLimit;
        public DateTime TransactionDate { get; set; }

        public OverDraftLimitUpdated(Guid accountId, decimal overdraftLimit, DateTime transactionDate)
        {
            AccountId=accountId;
            OverdraftLimit=overdraftLimit;
            TransactionDate=transactionDate;
        }
    }
}
