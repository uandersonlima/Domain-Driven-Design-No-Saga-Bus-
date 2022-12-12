using AscoreStore.Core.Messages;
namespace AscoreStore.Sales.Application.Events
{
    public class OrderProductRemovedEvent : Event
    {
        public OrderProductRemovedEvent(Guid customerId, Guid orderId, Guid productId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ProductId = productId;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }


        
    }
}