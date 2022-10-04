namespace DDDAccountBalance.Logic.Events
{
    public class FundsTransfered : IEvent
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } 
    public FundsTransfered(Guid accountId, decimal amount, DateTime transactionDate)
        {
            AccountId=accountId;
            Amount=amount;
            TransactionDate=transactionDate;
        }
    }
    
}
