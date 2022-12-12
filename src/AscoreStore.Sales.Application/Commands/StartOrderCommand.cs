using AscoreStore.Core.Messages;
using AscoreStore.Sales.Application.Validations;

namespace AscoreStore.Sales.Application.Commands
{
    public class StartOrderCommand : Command
    {
        public StartOrderCommand(Guid orderId, Guid customerId, decimal total, string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }

        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardCvv { get; private set; }


        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }



}