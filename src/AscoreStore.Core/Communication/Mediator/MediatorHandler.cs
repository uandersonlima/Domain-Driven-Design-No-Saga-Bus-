using AscoreStore.Core.Messages;
using MediatR;

namespace AscoreStore.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEventAsync<T>(T ev) where T : Event
        {
            await _mediator.Publish(ev);
        }
    }
}