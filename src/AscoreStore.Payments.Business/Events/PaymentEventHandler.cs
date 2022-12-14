using AscoreStore.Core.DomainObjects.DTO;
using AscoreStore.Core.Messages.Common.IntegrationEvents;
using AscoreStore.Payments.Business.PaymentAggregate;
using MediatR;

namespace AscoreStore.Payments.Business.Events
{
    public class PaymentEventHandler : INotificationHandler<ConfirmedStockOrderEvent>
    {
        private readonly IPaymentService _paymentService;

        public PaymentEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(ConfirmedStockOrderEvent notification, CancellationToken cancellationToken)
        {
            OrderPayment orderPayment = new
            (
                notification.OrderId,
                notification.CustomerId,
                notification.Total,
                notification.CardName,
                notification.CardNumber,
                notification.CardExpiration,
                notification.CardCvv
            );

            await _paymentService.MakePaymentOrder(orderPayment);
        }
    }
}