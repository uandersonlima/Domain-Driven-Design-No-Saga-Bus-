using AscoreStore.Sales.Application.Commands;
using FluentValidation;

namespace AscoreStore.Sales.Application.Validations
{
    public class RemoveOrderItemValidation : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");
        }
    }
}