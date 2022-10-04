

namespace DDDAccountBalance.Logic.Persistance
{
    using Events;
    using EventStore.Client;
    using Newtonsoft.Json;
    using System.Text;
    public class EventStoreService : IEventStoreService
    {
        EventStoreClientSettings _settings { get; }

        EventStoreClient _client { get; }
        public EventStoreService()
        {
            _settings = EventStoreClientSettings.Create("esdb://127.0.0.1:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000");
            _client = new EventStoreClient(_settings);
        }
        public async Task AddEventAsync(string streamId, object @event)
        {
            var eventData = new EventData(Uuid.NewUuid(), @event.GetType().Name,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)));
            var result = _client.AppendToStreamAsync(
                streamId,
                StreamState.Any,
                new List<EventData>
                {
                    eventData
                });
        }
        public async Task<List<IEvent>> GetEvents(string streamId)
        {
            var result = _client.ReadStreamAsync(Direction.Forwards, streamId, StreamPosition.Start);
            if (await result.ReadState == ReadState.StreamNotFound)
            {
                return new List<IEvent>(); 
            }
            var events = await result.ToListAsync();
            var DeserializedEvents = DeserializeEvents(events);
            return DeserializedEvents;
        }
        public async Task<List<IEvent>> GetEvents()
        {
            var result = _client.ReadAllAsync(Direction.Forwards, Position.Start);
            var events = (await result.ToListAsync()).Where(@event => @event.OriginalStreamId.StartsWith("account")).ToList();
            return DeserializeEvents(events);
        }

        private List<IEvent> DeserializeEvents(List<ResolvedEvent> list)
        {
            var @events = new List<IEvent>();
            foreach (var @event in list)
            {
                switch (@event.Event.EventType)
                {
                    case "AccountCreated":
                        var dataAccountCreated =
                            JsonConvert.DeserializeObject<AccountCreated>(
                                Encoding.UTF8.GetString(@event.Event.Data.ToArray()));
                        @events.Add(dataAccountCreated);
                        break;
                    case "FundsTransfered":
                        var dataFundsSent = JsonConvert.DeserializeObject<FundsTransfered>(
                            Encoding.UTF8.GetString(@event.Event.Data.ToArray()));
                        @events.Add(dataFundsSent);
                        break;
                    case "FundsDeposited":
                        var fundsDeposited = JsonConvert.DeserializeObject<FundsDeposited>(
                            Encoding.UTF8.GetString(@event.Event.Data.ToArray()));
                        @events.Add(fundsDeposited);
                        break;
                    case "FundsWithdrawn":
                        var fundsWithdrawn = JsonConvert.DeserializeObject<FundsWithdrawn>(
                            Encoding.UTF8.GetString(@event.Event.Data.ToArray()));
                        @events.Add(fundsWithdrawn);
                        break;
                    case "AccountBlocked":
                        var accountBlocked =
                            JsonConvert.DeserializeObject<AccountBlocked>(
                                Encoding.UTF8.GetString((@event.Event.Data.ToArray())));
                        @events.Add(accountBlocked);
                        break;
                    case "AccountUnblocked":
                        var accountUnblocked = JsonConvert.DeserializeObject<AccountUnblocked>(
                            Encoding.UTF8.GetString((@event.Event.Data.ToArray())));
                        @events.Add(accountUnblocked);
                        break;
                    case "ChequeDeposited":
                        var chequeDeposited = JsonConvert.DeserializeObject<ChequeDeposited>(
                            Encoding.UTF8.GetString((@event.Event.Data.ToArray())));
                        @events.Add(chequeDeposited);
                        break;
                    case "OverDraftLimitUpdated":
                        var overdraftLimitUpdated = JsonConvert.DeserializeObject<OverDraftLimitUpdated>(
                            Encoding.UTF8.GetString((@event.Event.Data.ToArray())));
                        @events.Add(overdraftLimitUpdated);
                        break;
                    case "DailyTransferLimitUpdated":
                        var dailyTransferLimitUpdated = JsonConvert.DeserializeObject<DailyTransferLimitUpdated>(
                            Encoding.UTF8.GetString((@event.Event.Data.ToArray())));
                        @events.Add(dailyTransferLimitUpdated);
                        break;
                }

            }
            return @events;
        }
    }
}