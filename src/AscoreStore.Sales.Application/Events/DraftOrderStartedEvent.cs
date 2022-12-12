using AscoreStore.Core.Messages;

namespace AscoreStore.Sales.Application.Events
{
    public class DraftOrderStartedEvent : Event
    {
        public DraftOrderStartedEvent(Guid customerId, Guid orderId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }

    }
}