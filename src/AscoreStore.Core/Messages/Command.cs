using FluentValidation.Results;
using MediatR;

namespace AscoreStore.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }


        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}