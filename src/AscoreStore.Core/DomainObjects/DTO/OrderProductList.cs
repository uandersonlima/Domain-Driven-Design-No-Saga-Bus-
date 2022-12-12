namespace AscoreStore.Core.DomainObjects.DTO
{
    public class OrderProductList
    {
        public Guid OrderId { get; set; }
        public ICollection<Item> Items { get; set; }
    }
    
    public class Item
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}