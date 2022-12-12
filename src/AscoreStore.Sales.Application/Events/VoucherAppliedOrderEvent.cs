using AscoreStore.Core.Messages;
namespace AscoreStore.Sales.Application.Events
{
    public class VoucherAppliedOrderEvent : Event
    {
        public VoucherAppliedOrderEvent(Guid customerId, Guid orderId, Guid voucherId)
        {
            
            CustomerId = customerId;
            OrderId = orderId;
            VoucherId = voucherId;
        }

        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid VoucherId { get; private set; }
    }
}