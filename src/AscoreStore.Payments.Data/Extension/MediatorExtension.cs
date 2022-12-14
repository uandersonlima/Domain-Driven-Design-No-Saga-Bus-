using AscoreStore.Core.Communication.Mediator;
using AscoreStore.Core.DomainObjects;

namespace AscoreStore.Payments.Data.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishEventAsync(this IMediatorHandler mediator, PaymentContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications is not null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEventAsync(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}