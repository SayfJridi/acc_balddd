
namespace DDDAccountBalance.Logic.Classes
{
    using Interfaces;
    using Persistance;
    using DDDAccountBalance.Logic.Events;
    using DDDAccountBalance.Logic.Exceptions;

    public class AccountRepository : IRepository<Account>
    {
        readonly EventStoreService _eventStore;
        public AccountRepository(EventStoreService eventStore)
        {
            _eventStore = eventStore;
        }
        public async Task<Account> GetById(Guid id)
        {

            List<IEvent> events = await this._eventStore.GetEvents($"account-{id}");

            if(events == new List<IEvent>())
            {
                throw new AccountNotFoundException("Account does Not Exist"); 
            }
            var account = new Account();
            account.Apply(events);
            return account;
        }

        public async Task Save(Guid accountId, Account account)
        {

            foreach (var @event in account.EventsOnHold)
            {
                await _eventStore.AddEventAsync($"account-{accountId}", @event);
            }
        }
    }
    }

