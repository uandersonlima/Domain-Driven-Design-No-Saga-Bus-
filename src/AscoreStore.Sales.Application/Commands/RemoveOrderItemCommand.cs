using AscoreStore.Core.Messages;
using FluentValidation;

namespace AscoreStore.Sales.Application.Commands
{
    public class RemoveOrderItemCommand : Command
    {
        public RemoveOrderItemCommand(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }

        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }


        public override bool IsValid()
        {
            return base.IsValid();
        }
    }


}