using MediatR;
using AscoreStore.Catalog.Application.Services;
using AscoreStore.Catalog.Application.Services.Interfaces;
using AscoreStore.Catalog.Data;
using AscoreStore.Catalog.Data.Repositories;
using AscoreStore.Catalog.Domain;
using AscoreStore.Catalog.Domain.Events;
using AscoreStore.Catalog.Domain.Interfaces;
using AscoreStore.Core.Communication.Mediator;
using AscoreStore.Core.Filter;
using AscoreStore.Core.Filter.Interpreters;

namespace AscoreStore.WebApp.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Filter
            services.AddSingleton<IFilterInterpreterFactory, FilterInterpreterFactory>();
            services.AddSingleton<IDynamicFilter, DynamicFilter>();

            // Domain communication (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();


            services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
        }
    }
}