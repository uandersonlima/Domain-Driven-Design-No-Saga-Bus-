using AscoreStore.Core.Data;

namespace AscoreStore.Sales.Domain.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetListByCustomerIdAsync(Guid customerId);
        Task<Order> GetDraftOrderByCustomerIdAsync(Guid customerId);
        void Add(Order order);
        void Update(Order order);

        Task<OrderItem> GetItemByIdAsync(Guid id);
        Task<OrderItem> GetItemByOrderAsync(Guid orderId, Guid productId);
        void AddItem(OrderItem orderItem);
        void UpdateItem(OrderItem orderItem);
        void RemoveItem(OrderItem orderItem);

        Task<Voucher> GetVoucherByCodeAsync(string code);
    }
}