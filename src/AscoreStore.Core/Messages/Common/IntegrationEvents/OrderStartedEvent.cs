using AscoreStore.Core.DomainObjects.DTO;

namespace AscoreStore.Core.Messages.Common.IntegrationEvents
{
    public class OrderStartedEvent : IntegrationEvent
    {
        public OrderStartedEvent(Guid orderId, Guid customerId, decimal total, OrderProductList orderProductList, string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
            Total = total;
            OrderProductList = orderProductList;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }

        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public OrderProductList OrderProductList { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardCvv { get; private set; }
    }
}