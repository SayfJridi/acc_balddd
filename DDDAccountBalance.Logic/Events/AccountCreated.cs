namespace DDDAccountBalance.Logic.Events
{
    public  class AccountCreated : IEvent
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public DateTime TransactionDate { get; set; }

        public AccountCreated(Guid accountId, string name, DateTime transactionDate)
        {
            AccountId=accountId;
            Name=name;
            TransactionDate=transactionDate;
        }
    }


}
