using AscoreStore.Core.DomainObjects;
using FluentValidation.Results;

namespace AscoreStore.Sales.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public OrderStatus Status { get; private set; }

        private readonly List<OrderItem> _orderItem;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItem;

        // EF Rel.
        public Voucher Voucher { get; private set; }



        public Order(Guid customerId, bool voucherUsed, decimal discount, decimal totalValue)
        {
            CustomerId = customerId;
            VoucherUsed = voucherUsed;
            Discount = discount;
            TotalValue = totalValue;
            _orderItem = new List<OrderItem>();
        }

        protected Order()
        {
            _orderItem = new List<OrderItem>();
        }

        public ValidationResult ApplyVoucher(Voucher voucher)
        {
            var validationResult = voucher.ValidateIfApplicable();
            if (!validationResult.IsValid) return validationResult;

            Voucher = voucher;
            VoucherUsed = true;
            CalculateOrderValue();

            return validationResult;
        }

        public void CalculateOrderValue()
        {
            TotalValue = OrderItems.Sum(p => p.CalculateValue());
            CalculateDiscount();
        }

        public void CalculateDiscount()
        {
            if (!VoucherUsed) return;

            decimal discount = 0;
            var value = TotalValue;

            if (Voucher.VoucherDiscountType == VoucherDiscountType.Percentage)
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = (value * Voucher.Percentage.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            TotalValue = discount < 0 ? 0 : value;
            Discount = discount;
        }

        public bool ExistingOrderItem(OrderItem item)
        {
            return _orderItem.Any(p => p.ProductId == item.ProductId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);

            if (ExistingOrderItem(item))
            {
                var existingItem = _orderItem.FirstOrDefault(p => p.ProductId == item.ProductId);
                existingItem.AddUnits(item.Quantity);
                item = existingItem;

                _orderItem.Remove(existingItem);
            }

            item.CalculateValue();
            _orderItem.Add(item);

            CalculateOrderValue();
        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            var existingItem = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existingItem is null) throw new DomainException("O item não pertence ao pedido");
            _orderItem.Remove(existingItem);

            CalculateOrderValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var existingItem = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existingItem is null) throw new DomainException("O item não pertence ao pedido");

            _orderItem.Remove(existingItem);
            _orderItem.Add(item);

            CalculateOrderValue();
        }

        public void UpdateUnits(OrderItem item, int units)
        {
            item.ChangeUnits(units);
            UpdateItem(item);
        }

        public void MakeDraft()
        {
            Status = OrderStatus.Draft;
        }

        public void StartOrder()
        {
            Status = OrderStatus.Started;
        }

        public void FinalizeOrder()
        {
            Status = OrderStatus.PaidOut;
        }

        public void CancelOrder()
        {
            Status = OrderStatus.Canceled;
        }

        public static class OrderFactory
        {
            public static Order NewDraftOrder(Guid customerId)
            {
                var order = new Order
                {
                    CustomerId = customerId,
                };

                order.MakeDraft();
                return order;
            }
        }


    }
}