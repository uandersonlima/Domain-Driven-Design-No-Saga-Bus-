namespace AscoreStore.Payments.Business.PaymentAggregate
{
    public class Order
    {
        public Order(Guid id, decimal valor)
        {
            Id = id;
            Valor = valor;
        }

        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public List<Product> Products { get; set; }
    }
}