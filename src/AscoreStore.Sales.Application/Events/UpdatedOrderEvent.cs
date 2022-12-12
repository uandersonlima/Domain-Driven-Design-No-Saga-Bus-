using AscoreStore.Core.Messages;
namespace AscoreStore.Sales.Application.Events
{
    public class UpdatedOrderEvent : Event
    {
        public UpdatedOrderEvent(Guid customerId, Guid orderId, decimal totalValue)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            TotalValue = totalValue;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal TotalValue { get; private set; }
    }
}