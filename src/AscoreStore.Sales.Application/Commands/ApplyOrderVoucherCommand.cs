using AscoreStore.Core.Messages;
using AscoreStore.Sales.Application.Validations;

namespace AscoreStore.Sales.Application.Commands
{
    public class ApplyOrderVoucherCommand : Command
    {
        public ApplyOrderVoucherCommand(Guid customerId, string voucherCode)
        {
            CustomerId = customerId;
            VoucherCode = voucherCode;
        }

        public Guid CustomerId { get; private set; }
        public string VoucherCode { get; private set; }


        public override bool IsValid()
        {
            ValidationResult = new ApplyOrderVoucherValidation().Validate(this);
            return ValidationResult.IsValid;
        }


    }

}