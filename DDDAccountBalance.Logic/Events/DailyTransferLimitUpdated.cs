namespace DDDAccountBalance.Logic.Events
{
    public  class DailyTransferLimitUpdated : IEvent
    {
        public Guid AccountId { get; set; }
        public decimal DailyTransferLimit { get; set; }
        public DateTime TransactionDate { get; set;  } 
    }
}
