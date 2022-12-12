namespace AscoreStore.Sales.Domain.OrderAggregate
{
    public enum OrderStatus
    {
        Draft = 0,
        Started = 1,
        PaidOut = 4,
        Delivered = 5,
        Canceled = 6

    }
}