

namespace UnitTesting.Mocks
{
    using DDDAccountBalance.Logic.Classes;
    using DDDAccountBalance.Logic.Exceptions;
    using DDDAccountBalance.Logic.Events;
    using DDDAccountBalance.Logic.Interfaces;
    public class MockedAccountRepository : IRepository<Account>
    {
        private Dictionary<Guid, List<IEvent>> _events = new Dictionary<Guid, List<IEvent>>();

        public async Task<Account> GetById(Guid id)
        {
            if (_events.ContainsKey(id))
            {
                var account = new Account();
                account.Apply(_events[id]);
                return account;
            }

            throw new AccountNotFoundException("Account not found");
            ;
        }

        public async Task Save(Guid id, Account account)
        {
            if (!_events.ContainsKey(id))
            {
                _events.Add(id, account.EventsOnHold);
            }

            else
            {
                foreach (var @event in account.EventsOnHold)
                {
                    _events[id].Add(@event);
                }

            }

        }
    }
}