namespace DDDAccountBalance.Logic.Commands
{
    public class DepositCheque : ICommand
    {
        public Guid AccountId { get; set; } 
        public decimal Value { get; set; }

        public DepositCheque(Guid accountId, decimal value)
        {
            AccountId=accountId;
            Value=value;
        }
        public bool IsValid()
        {
            if (Value <= 0)
            {
                return false;
            }
            return true;
        }

    }
    }
