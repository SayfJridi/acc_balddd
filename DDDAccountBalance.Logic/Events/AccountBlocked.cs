namespace DDDAccountBalance.Logic.Events
{
    public class AccountBlocked : IEvent
    {
        public Guid AccountId { get; set; }
        public BlockingReason BlockingReason { get; set; }

        public DateTime TransactionDate {
            get;
            set;
        }

        public AccountBlocked(Guid accountId, BlockingReason blockingReason, DateTime transactionDate)
        {
            AccountId=accountId;
            BlockingReason=blockingReason;
            TransactionDate=transactionDate;
        }
    }
}


public class BlockingReason
{
    public string Type { get; set; }
    public decimal Amount { get; set; }

    public BlockingReason(string type, decimal amount)
    {
        Type=type;
        Amount=amount;
    }
}
