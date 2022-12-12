namespace AscoreStore.Sales.Application.Queries.ViewModels
{
    public class CartPaymentViewModel
    {
        public CartPaymentViewModel(string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvv { get; set; }
    }
}