namespace DDDAccountBalance.Logic.Persistance
{
    using Events;
    public interface IEventStoreService
    {
        public Task AddEventAsync(string streamId, object @event);
        public Task<List<IEvent>> GetEvents(string streamId);
    }
}
