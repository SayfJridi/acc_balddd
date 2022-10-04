namespace DDDAccountBalance.Logic.Events
{
    public class FundsDeposited : IEvent
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public FundsDeposited(Guid accountId, decimal amount, DateTime transactionDate)
        {
            AccountId=accountId;
            Amount=amount;
            TransactionDate=transactionDate;
        }
    }
}
