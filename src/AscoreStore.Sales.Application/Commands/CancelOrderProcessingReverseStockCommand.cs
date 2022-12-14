using AscoreStore.Core.Messages;

namespace AscoreStore.Sales.Application.Commands
{
    public class CancelOrderProcessingReverseStockCommand : Command
    {
        public CancelOrderProcessingReverseStockCommand(Guid orderId, Guid customerId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            AggregateId = orderId;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
    }
}