using AscoreStore.Core.Messages;
using AscoreStore.Sales.Application.Validations;

namespace AscoreStore.Sales.Application.Commands
{
    public class AddOrderItemCommand : Command
    {
        public AddOrderItemCommand(Guid customerId, Guid productId, string name, int quantity, decimal unitaryValue)
        {
            CustomerId = customerId;
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            UnitaryValue = unitaryValue;
        }

        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitaryValue { get; private set; }


        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}