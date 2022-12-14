using AscoreStore.Core.DomainObjects.DTO;

namespace AscoreStore.Core.Messages.Common.IntegrationEvents
{
    public class ConfirmedStockOrderEvent : IntegrationEvent
    {
        public ConfirmedStockOrderEvent(Guid orderId, Guid customerId, OrderProductList orderProductList, decimal total, string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            OrderId = orderId;
            CustomerId = customerId;
            OrderProductList = orderProductList;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }

        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderProductList OrderProductList { get; private set; }
        public decimal Total { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardCvv { get; private set; }
    }
}