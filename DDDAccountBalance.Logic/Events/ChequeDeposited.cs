namespace DDDAccountBalance.Logic.Events
{
    using Classes;
    public class ChequeDeposited : IEvent
    {
        
        public Guid AccountId { get; set;  }
        public DateTime ValidDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public ChequeDeposited(Guid accountId, DateTime validDate, decimal amount, DateTime transactionDate)
        {
            AccountId=accountId;
            ValidDate=validDate;
            Amount=amount;
            TransactionDate=transactionDate;
        }
    }
}
