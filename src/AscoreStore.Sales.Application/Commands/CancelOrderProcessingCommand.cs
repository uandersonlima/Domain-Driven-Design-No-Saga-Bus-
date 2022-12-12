using AscoreStore.Core.Messages;
using FluentValidation;

namespace AscoreStore.Sales.Application.Commands
{
    public class CancelOrderProcessingCommand : Command
    {
        public CancelOrderProcessingCommand(Guid customerId, Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            AggregateId = orderId;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
    }
}