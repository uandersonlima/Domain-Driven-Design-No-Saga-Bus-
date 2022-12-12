using AscoreStore.Core.DomainObjects;

namespace AscoreStore.Sales.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitaryValue { get; private set; }

        // EF Rel.
        public Order Order { get; set; }


        public OrderItem(Guid orderId, string productName, int quantity, decimal unitaryValue)
        {
            OrderId = orderId;
            ProductName = productName;
            Quantity = quantity;
            UnitaryValue = unitaryValue;
        }

        protected OrderItem() { }

        internal void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculateValue()
        {
            return Quantity * UnitaryValue;
        }

        internal void AddUnits(int units)
        {
            Quantity += units;
        }

        internal void ChangeUnits(int units)
        {
            Quantity = units;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}