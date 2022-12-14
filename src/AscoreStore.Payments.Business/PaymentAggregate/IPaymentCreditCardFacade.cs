namespace AscoreStore.Payments.Business.PaymentAggregate
{
    public interface IPaymentCreditCardFacade
    {
        Transaction MakePayment(Order order, Payment payment);
    }
}