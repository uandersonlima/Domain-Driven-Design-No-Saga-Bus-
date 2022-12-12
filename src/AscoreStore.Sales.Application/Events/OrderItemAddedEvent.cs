using AscoreStore.Core.Messages;
namespace AscoreStore.Sales.Application.Events
{
    public class OrderItemAddedEvent : Event
    {
        public OrderItemAddedEvent(Guid customerId, Guid orderId, Guid productId, string productName, decimal unitaryValue, int quantity)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            UnitaryValue = unitaryValue;
            Quantity = quantity;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitaryValue { get; private set; }
        public int Quantity { get; private set; }
    }
}