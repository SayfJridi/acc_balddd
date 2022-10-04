namespace DDDAccountBalance.Logic.Events
{
    public class FundsWithdrawn : IEvent
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public FundsWithdrawn(Guid accountId, decimal amount, DateTime transactionDate)
        {
            AccountId=accountId;
            Amount=amount;
            TransactionDate=transactionDate;
        }
    }
}
