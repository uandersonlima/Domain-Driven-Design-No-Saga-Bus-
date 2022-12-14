using AscoreStore.Core.Helpers.Enumeration;

namespace AscoreStore.Payments.Business.PaymentAggregate
{
    public class TransactionStatus : Enumeration
    {
        public static TransactionStatus PaidOut = new(1, nameof(PaidOut));
        public static TransactionStatus Declined = new(2, nameof(Declined));
       
        public TransactionStatus(int id, string name) : base(id, name)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PaidOut;
            yield return Declined;
        }
    }
}