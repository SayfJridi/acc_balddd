namespace DDDAccountBalance.Logic.Events
{
    public interface IEvent
    { 
        public DateTime TransactionDate
        {
            get;
            set;
        }
    }
}
