using AscoreStore.Core.DomainObjects;

namespace AscoreStore.Payments.Business.PaymentAggregate
{
    public class Transaction : Entity
    {
        public Transaction(Guid orderId, Guid paymentId, decimal total)
        {
            OrderId = orderId;
            PaymentId = paymentId;
            Total = total;
        }

        public void SetStatus(TransactionStatus status)
        {
            Status = status;
        }


        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Total { get; set; }
        public TransactionStatus Status { get; set; }

        // EF. Rel.
        public Payment Payment { get; set; }
    }
}