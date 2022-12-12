using AscoreStore.Core.Messages;

namespace AscoreStore.Sales.Application.Events
{
    public class FinishedOrderEvent : Event
    {
        public FinishedOrderEvent(Guid orderId)
        {
            AggregateId = orderId;
            OrderId = orderId;
        }

        public Guid OrderId { get; private set; }


        
    }
}