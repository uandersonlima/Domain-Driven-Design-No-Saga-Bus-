using AscoreStore.Core.DomainObjects.DTO;

namespace AscoreStore.Payments.Business.PaymentAggregate
{
    public interface IPaymentService
    {
        Task<Transaction> MakePaymentOrder(OrderPayment orderPayment);
    }
}