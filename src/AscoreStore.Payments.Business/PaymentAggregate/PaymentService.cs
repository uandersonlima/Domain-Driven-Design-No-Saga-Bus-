using AscoreStore.Core.Communication.Mediator;
using AscoreStore.Core.DomainObjects.DTO;
using AscoreStore.Core.Messages.Common.IntegrationEvents;
using AscoreStore.Core.Messages.Common.Notifications;

namespace AscoreStore.Payments.Business.PaymentAggregate
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentCreditCardFacade _paymentCreditCardFacade;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PaymentService(IPaymentCreditCardFacade paymentCreditCardFacade, IPaymentRepository paymentRepository, IMediatorHandler mediatorHandler)
        {
            _paymentCreditCardFacade = paymentCreditCardFacade;
            _paymentRepository = paymentRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transaction> MakePaymentOrder(OrderPayment oP)
        {
            Order order = new(oP.OrderId, oP.Total);

            Payment payment = new(oP.OrderId, oP.Total, oP.CardName, oP.CardNumber, oP.CardExpiration, oP.CardCvv);

            Transaction transaction = _paymentCreditCardFacade.MakePayment(order, payment);


            if (transaction.Status == TransactionStatus.PaidOut)
            {
                payment.AddEvent(new PaymentMadeEvent(order.Id, oP.CustomerId, transaction.PaymentId, transaction.Id, order.Valor));

                _paymentRepository.Add(payment);
                _paymentRepository.AddTransaction(transaction);

                await _paymentRepository.UnitOfWork.Commit();
                return transaction;
            }

            await _mediatorHandler.PublishNotificationAsync(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
            await _mediatorHandler.PublishEventAsync(new PaymentDeclinedEvent(order.Id, oP.CustomerId, transaction.PaymentId, transaction.Id, order.Valor));

            return transaction;
        }
    }
}