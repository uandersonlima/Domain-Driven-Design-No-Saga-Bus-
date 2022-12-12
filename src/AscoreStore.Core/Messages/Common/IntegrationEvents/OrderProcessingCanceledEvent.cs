using AscoreStore.Core.DomainObjects.DTO;

namespace AscoreStore.Core.Messages.Common.IntegrationEvents
{
    public class OrderProcessingCanceledEvent : IntegrationEvent
    {
        public OrderProcessingCanceledEvent(Guid orderId, Guid customerId, OrderProductList orderProductList)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
            OrderProductList = orderProductList;
        }

        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderProductList OrderProductList { get; private set; }
    }
}