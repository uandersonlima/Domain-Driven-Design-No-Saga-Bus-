namespace AscoreStore.Core.Messages.Common.IntegrationEvents
{
    public class PaymentMadeEvent : IntegrationEvent
    {
        public PaymentMadeEvent(Guid orderId, Guid customerId, Guid paymentId, Guid transactionId, decimal total)
        {
            AggregateId = paymentId;
            OrderId = orderId;
            CustomerId = customerId;
            PaymentId = paymentId;
            TransactionId = transactionId;
            Total = total;
        }

        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid PaymentId { get; private set; }
        public Guid TransactionId { get; private set; }
        public decimal Total { get; private set; }
    }
}