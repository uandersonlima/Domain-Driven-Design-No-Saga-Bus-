namespace AscoreStore.Core.Messages.Common.IntegrationEvents
{
    public class RejectedStockOrderEvent : IntegrationEvent
    {
        public RejectedStockOrderEvent(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;

        }

        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        
    }
}