namespace AscoreStore.Sales.Application.Queries.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(int code, decimal totalValue, DateTime createdDate, int status)
        {
            Code = code;
            TotalValue = totalValue;
            CreatedDate = createdDate;
            Status = status;
        }

        public int Code { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
    }
}