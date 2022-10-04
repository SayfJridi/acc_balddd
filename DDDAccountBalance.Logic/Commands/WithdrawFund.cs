namespace DDDAccountBalance.Logic.Commands
{
    public class WithdrawFund : ICommand
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public WithdrawFund(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
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
