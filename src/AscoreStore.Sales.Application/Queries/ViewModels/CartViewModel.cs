namespace AscoreStore.Sales.Application.Queries.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel(Guid orderId, Guid customerId, decimal subTotal, decimal totalValue, decimal discountValue, string voucherCode)
        {
            OrderId = orderId;
            CustomerId = customerId;
            SubTotal = subTotal;
            TotalValue = totalValue;
            DiscountValue = discountValue;
            VoucherCode = voucherCode;
        }

        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalValue { get; set; }
        public decimal DiscountValue { get; set; }
        public string VoucherCode { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public CartPaymentViewModel Payment { get; set; }
    }
}