namespace DDDAccountBalance.Logic.Classes
{
    using Events;
    using NodaTime;

    public class Account
    {
        Guid _id;
        string _name;
        decimal _balance;
        decimal _overdraftLimit;
        decimal _dailyTransferLimit;
        decimal _todayTransferUsage;
        bool _isBlocked;
        decimal _fundOnHold;

        public List<IEvent> EventsOnHold { get; set; } = new List<IEvent>();

        public Account(Guid id, string name)
        {
            EventsOnHold.Add(new AccountCreated(id , name , DateTime.Now));
        }

        public Account()
        {
        }

        public void Apply(List<IEvent> @events , IClock clock)
        {

            foreach (var @event in @events)
            {
                switch (@event.GetType().Name)
                {
                    case "AccountCreated":
                        ApplyEvent((AccountCreated)@event, clock);
                        break;
                    case "FundsTransfered":
                        ApplyEvent((FundsTransfered)@event, clock);
                        break;
                    case "FundsDeposited":
                        ApplyEvent((FundsDeposited)@event, clock);
                        break;
                    case "FundsWithdrawn":
                        ApplyEvent((FundsWithdrawn)@event, clock);
                        break;
                    case "AccountBlocked":
                        ApplyEvent((AccountBlocked)@event, clock);
                        break;
                    case "AccountUnblocked":
                        ApplyEvent((AccountUnblocked)@event , clock);
                        break;
                    case "ChequeDeposited":
                        ApplyEvent((ChequeDeposited)@event, clock );
                        break;
                    case "OverDraftLimitUpdated":
                        ApplyEvent((OverDraftLimitUpdated)@event, clock);
                        break;
                    case "DailyTransferLimitUpdated":
                        ApplyEvent((DailyTransferLimitUpdated)@event , clock);
                        break;
                }
            }
        }



        public void ApplyEvent(AccountCreated @event,IClock clock)
        {
            _id = @event.AccountId;
            _name = @event.Name;
        }

        void IncreaseFund(decimal amount)
        {
            _balance += amount;
        }

        void DecreaseFund(decimal amount)
        {
            _balance -= amount;
        }

        public bool WithdrawFund(decimal amount, IClock clock)
        {

            if (_isBlocked)
            {
                return false;
            }

            if (_balance + _overdraftLimit < amount)
            {
                EventsOnHold.Add(new AccountBlocked(_id, new BlockingReason("WithDrawFundRequest", amount), DateTime.Now));
                return false;
            }

            EventsOnHold.Add(new FundsWithdrawn(_id, amount, DateTime.Now));
            return true;
        }


        public bool WireTransfer(decimal amount, IClock clock)
        {
            Console.WriteLine($"amount : {amount}\n  _todayTransferUsage : {_todayTransferUsage} \n _dailyTransferLimit : {_dailyTransferLimit} \n "); 
            if (amount + _todayTransferUsage > _dailyTransferLimit)
            {
                return false;
            }

            if (_balance + _overdraftLimit < amount)
            {
                EventsOnHold.Add(new AccountBlocked(_id, new BlockingReason("TransferFundRequest", amount), DateTime.Now));
                return false;
            }
            EventsOnHold.Add(new FundsTransfered(_id, amount, DateTime.Now));
            return true;
        }


        public bool DepositFund(decimal amount, IClock clock)
        {
            EventsOnHold.Add(new FundsDeposited(_id,amount,DateTime.Now));
            if (_isBlocked)
            {
                EventsOnHold.Add(new AccountUnblocked(_id, DateTime.Now, DateTime.Now));
            }

            ;
            return true;
        }

        public bool SetOverdraftLimit(decimal amount, IClock clock)
        {
            EventsOnHold.Add(new OverDraftLimitUpdated(_id, amount, DateTime.Now));
            return true;
        }

        public bool SetDailyTransferLimit(decimal amount, IClock clock)
        {
            EventsOnHold.Add(new DailyTransferLimitUpdated() { AccountId = _id, DailyTransferLimit = amount });
            return true;
        }

        public bool DepositCheque(decimal amount, IClock clock)
        {
            EventsOnHold.Add(new ChequeDeposited(_id, Utility.NextBusinessDay(), amount, DateTime.Now));
            EventsOnHold.Add(new AccountUnblocked(_id, Utility.NextBusinessDay(), DateTime.Now));
            return true;
        }



        void ApplyEvent(FundsTransfered @event, IClock clock)
        {
            if (@event.TransactionDate.Date == DateTime.Today)
            {
                _todayTransferUsage += @event.Amount;
            }

            DecreaseFund(@event.Amount);
        }


        public void ApplyEvent(FundsWithdrawn @event, IClock clock)
        {
            DecreaseFund(@event.Amount);
        }

        public void ApplyEvent(FundsDeposited @event, IClock clock)
        {
            IncreaseFund(@event.Amount);
        }

        public void ApplyEvent(ChequeDeposited @event, IClock clock)
        {
            if (DateTime.Now > @event.ValidDate)
            {
                IncreaseFund(@event.Amount);
                return;
            }

            _fundOnHold += @event.Amount;
            Console.WriteLine(_fundOnHold);
        }

        void ApplyEvent(AccountBlocked @event, IClock clock)
        {
            _isBlocked = true;
        }

        void ApplyEvent(AccountUnblocked @event, IClock clock)
        {
            if(DateTime.Now < @event.UnblockingDate)
            {
                return; 
            }
            _isBlocked = false;
        }

        void ApplyEvent(OverDraftLimitUpdated @event, IClock clock)
        {
            _overdraftLimit = @event.OverdraftLimit;
        }

        void ApplyEvent(DailyTransferLimitUpdated @event, IClock clock)
        {
            _dailyTransferLimit = @event.DailyTransferLimit;
        }
    }
}

