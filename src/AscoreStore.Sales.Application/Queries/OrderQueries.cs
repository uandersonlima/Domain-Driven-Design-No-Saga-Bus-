using AscoreStore.Core.Extensions;
using AscoreStore.Sales.Application.Queries.ViewModels;
using AscoreStore.Sales.Domain.OrderAggregate;

namespace AscoreStore.Sales.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CartViewModel> GetCartByCustomerIdAsync(Guid customerId)
        {
            var order = await _orderRepository.GetDraftOrderByCustomerIdAsync(customerId);
            if (order is null) return null;


            var voucher = order.VoucherId is not null ? order.Voucher.Code : String.Empty;
            var cart = new CartViewModel(order.Id, order.CustomerId, (order.Discount + order.TotalValue), order.TotalValue, order.Discount, voucher);

            order.OrderItems.ForEach(item => cart.Items.Add(new CartItemViewModel(item.ProductId, item.ProductName, item.Quantity, item.UnitaryValue, item.CalculateValue())));

            return cart;
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            var orders = await _orderRepository.GetListByCustomerIdAsync(customerId);

            orders = orders.Where(p => p.Status == OrderStatus.PaidOut || p.Status == OrderStatus.Canceled)
                .OrderByDescending(p => p.Code);

            if (!orders.Any()) return null;

            var ordersView = new List<OrderViewModel>();

            orders.ForEach(order => ordersView.Add(new OrderViewModel(order.Code, order.TotalValue, order.CreatedDate, (int)order.Status)));

            return ordersView;
        }
    }
}