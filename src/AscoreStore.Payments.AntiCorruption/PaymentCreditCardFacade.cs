using AscoreStore.Payments.Business.PaymentAggregate;

namespace AscoreStore.Payments.AntiCorruption
{
    public class PaymentCreditCardFacade : IPaymentCreditCardFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configManager;

        public PaymentCreditCardFacade(IPayPalGateway payPalGateway, IConfigurationManager configManager)
        {
            _payPalGateway = payPalGateway;
            _configManager = configManager;
        }

        public Transaction MakePayment(Order order, Payment payment)
        {
            var apiKey = _configManager.GetValue("apiKey");
            var encriptionKey = _configManager.GetValue("encriptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.CardNumber);

            var paymentResult = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Value);

            // TODO: O gateway de pagamentos que deve retornar o objeto transação
            Transaction transaction = new
            (
                order.Id,
                payment.Id,
                order.Valor
            );

            if (paymentResult)
            {
                transaction.Status = TransactionStatus.PaidOut;
                return transaction;
            }

            transaction.Status = TransactionStatus.Declined;
            return transaction;
        }
    }
}