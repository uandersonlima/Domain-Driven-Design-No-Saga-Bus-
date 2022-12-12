namespace AscoreStore.Core.DomainObjects.DTO
{
    public class OrderPayment
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvv { get; set; }
    }
}