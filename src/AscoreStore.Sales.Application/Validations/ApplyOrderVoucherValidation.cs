using AscoreStore.Sales.Application.Commands;
using FluentValidation;

namespace AscoreStore.Sales.Application.Validations
{
    public class ApplyOrderVoucherValidation : AbstractValidator<ApplyOrderVoucherCommand>
    {
        public ApplyOrderVoucherValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.VoucherCode)
                .NotEmpty()
                .WithMessage("O código do voucher não pode ser vazio");
        }
    }
}