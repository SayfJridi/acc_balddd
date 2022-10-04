namespace DDDAccountBalance.Logic.Commands
{
    public class SetOverdraftLimit : ICommand
    {
        public Guid AccountId { get; set; }
        public decimal OverdraftLimit { get; set; }

        public SetOverdraftLimit(Guid accountId, decimal overdraftLimit)
        {
            AccountId=accountId;
            OverdraftLimit=overdraftLimit;
        }

        public bool IsValid()
        {
            if (OverdraftLimit <= 0)
            {
                return false;
            }
            return true; 
        }
    }
}
