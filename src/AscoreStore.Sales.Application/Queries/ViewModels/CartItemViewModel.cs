namespace AscoreStore.Sales.Application.Queries.ViewModels
{
    public class CartItemViewModel
    {
        public CartItemViewModel(Guid productId, string productName, int quantity, decimal unitaryValue, decimal totalValue)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitaryValue = unitaryValue;
            TotalValue = totalValue;
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitaryValue { get; set; }
        public decimal TotalValue { get; set; }
    }
}