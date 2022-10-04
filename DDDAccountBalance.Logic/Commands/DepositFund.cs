namespace DDDAccountBalance.Logic.Commands
{
    public  class DepositFund : ICommand
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public DepositFund(Guid accountId, decimal amount)
        {
            AccountId=accountId;
            Amount=amount;
        }
        public bool IsValid()
        {
            if (Amount <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
