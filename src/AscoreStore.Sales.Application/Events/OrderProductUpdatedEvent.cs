using AscoreStore.Core.Messages;
namespace AscoreStore.Sales.Application.Events
{
    public class OrderProductUpdatedEvent : Event
    {
        public OrderProductUpdatedEvent(Guid customerId, Guid orderId, Guid productId, int quantity)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        
    }
}