using AscoreStore.Sales.Application.Queries.ViewModels;

namespace AscoreStore.Sales.Application.Queries
{
    public interface IOrderQueries
    {
        Task<CartViewModel> GetCartByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<OrderViewModel>> GetOrdersByCustomerIdAsync(Guid customerId);
    }
}