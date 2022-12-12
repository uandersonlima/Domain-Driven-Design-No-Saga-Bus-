using AscoreStore.Core.Data;
using AscoreStore.Sales.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace AscoreStore.Sales.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesContext _context;

        public OrderRepository(SalesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetListByCustomerIdAsync(Guid customerId)
        {
            return await _context.Orders.AsNoTracking().Where(p => p.CustomerId == customerId).ToListAsync();
        }

        public async Task<Order> GetDraftOrderByCustomerIdAsync(Guid customerId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(p => p.CustomerId == customerId && p.Status == OrderStatus.Draft);
            if (order is null) return null;

            await _context.Entry(order)
                .Collection(o => o.OrderItems).LoadAsync();

            if (order.VoucherId is not null)
            {
                await _context.Entry(order)
                    .Reference(o => o.Voucher).LoadAsync();
            }

            return order;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }


        public async Task<OrderItem> GetItemByIdAsync(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrderAsync(Guid orderId, Guid produtoId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(p => p.ProductId == produtoId && p.OrderId == orderId);
        }

        public void AddItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
        }

        public void UpdateItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
        }

        public void RemoveItem(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
        }

        public async Task<Voucher> GetVoucherByCodeAsync(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        
    }
}