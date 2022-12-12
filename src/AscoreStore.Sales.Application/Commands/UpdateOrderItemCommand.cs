using AscoreStore.Core.Messages;
using AscoreStore.Sales.Application.Validations;

namespace AscoreStore.Sales.Application.Commands
{
    public class UpdateOrderItemCommand : Command
    {
        public UpdateOrderItemCommand(Guid customerId, Guid productId, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }


        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }


}