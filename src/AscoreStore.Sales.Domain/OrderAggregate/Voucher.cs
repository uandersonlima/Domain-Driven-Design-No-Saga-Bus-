using AscoreStore.Core.DomainObjects;
using FluentValidation;
using FluentValidation.Results;

namespace AscoreStore.Sales.Domain.OrderAggregate
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public VoucherDiscountType VoucherDiscountType { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? DateOfUse { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool IsActivated { get; private set; }
        public bool Used { get; private set; }


        // EF Rel.
        public ICollection<Order> Orders { get; set; }


        internal ValidationResult ValidateIfApplicable()
        {
            return new VoucherApplicableValidation().Validate(this);
        }
    }
    public class VoucherApplicableValidation : AbstractValidator<Voucher>
    {

        public VoucherApplicableValidation()
        {
            RuleFor(c => c.ExpirationDate)
                .Must(ExpirationDateHigherThanCurrent)
                .WithMessage("Este voucher está expirado.");

            RuleFor(c => c.IsActivated)
                .Equal(true)
                .WithMessage("Este voucher não é mais válido.");

            RuleFor(c => c.Used)
                .Equal(false)
                .WithMessage("Este voucher já foi utilizado.");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Este voucher não está mais disponível");
        }

        protected static bool ExpirationDateHigherThanCurrent(DateTime expirationDate) => expirationDate >= DateTime.Now;
    }
}