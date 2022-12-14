namespace AscoreStore.Core.DomainObjects.DTO
{
    public class OrderPayment
    {
        public OrderPayment(Guid orderId, Guid customerId, decimal total, string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }

        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvv { get; set; }
    }
}