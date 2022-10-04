namespace DDDAccountBalance.Logic.Events
{
    public class AccountUnblocked : IEvent
    {
        public Guid AccountId { get; set; }
        public DateTime UnblockingDate { get; set; }
        public DateTime TransactionDate { get; set; }

        public AccountUnblocked(Guid accountId, DateTime unblockingDate, DateTime transactionDate)
        {
            AccountId=accountId;
            UnblockingDate=unblockingDate;
            TransactionDate=transactionDate;
        }
    }
}
