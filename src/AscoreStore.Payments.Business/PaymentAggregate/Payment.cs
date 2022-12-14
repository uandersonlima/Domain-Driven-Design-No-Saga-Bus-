using AscoreStore.Core.DomainObjects;

namespace AscoreStore.Payments.Business.PaymentAggregate
{
    public class Payment : Entity, IAggregateRoot
    {
        public Payment(Guid orderId, decimal value, string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            OrderId = orderId;
            Value = value;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }

        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public decimal Value { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvv { get; set; }

        // EF. Rel.
        public Transaction Transaction { get; set; }
    }
}