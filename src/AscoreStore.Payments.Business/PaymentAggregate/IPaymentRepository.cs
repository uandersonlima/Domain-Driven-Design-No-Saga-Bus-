using AscoreStore.Core.Data;

namespace AscoreStore.Payments.Business.PaymentAggregate
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Add(Payment payment);

        void AddTransaction(Transaction transaction);
    }
}