namespace DDDAccountBalance.Logic.Commands
{
    public class SetDailyTransferLimit : ICommand 
    {
        public Guid AccountId { get; set; }
        public decimal DailyTransferLimit { get; set; }

        public SetDailyTransferLimit(Guid accountId, decimal dailyTransferLimit)
        {
            AccountId=accountId;
            DailyTransferLimit=dailyTransferLimit;
        }
        public bool IsValid()
        {
            if (DailyTransferLimit <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
