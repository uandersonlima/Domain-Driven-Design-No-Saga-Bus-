using AscoreStore.Core.Messages;

namespace AscoreStore.Sales.Application.Commands
{
    public class FinalizeOrderCommand : Command
    {
        public FinalizeOrderCommand(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }

        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
    }
}