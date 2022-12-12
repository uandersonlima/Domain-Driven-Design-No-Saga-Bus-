using AscoreStore.Sales.Application.Commands;
using FluentValidation;

namespace AscoreStore.Sales.Application.Validations
{
    public class UpdateOrderItemValidation : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");
        }
    }
}