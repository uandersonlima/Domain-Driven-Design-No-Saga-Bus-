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
using AscoreStore.Core.Messages.Common.Notifications;
using AscoreStore.Sales.Application.Queries;
using AscoreStore.Sales.Data.Repository;
using AscoreStore.Sales.Domain.OrderAggregate;
using AscoreStore.Sales.Data;
using AscoreStore.Sales.Application.Commands;
using AscoreStore.Sales.Application.Handlers;
using AscoreStore.Sales.Application.Events;
using AscoreStore.Core.Messages.Common.IntegrationEvents;
using AscoreStore.Payments.Business.PaymentAggregate;
using AscoreStore.Payments.Data.Repository;
using AscoreStore.Payments.AntiCorruption;
using ConfigurationManager = AscoreStore.Payments.AntiCorruption.ConfigurationManager;
using AscoreStore.Payments.Data;
using AscoreStore.Payments.Business.Events;

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

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<OrderStartedEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<OrderProcessingCanceledEvent>, ProductEventHandler>();


            //Sales  
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyOrderVoucherCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<StartOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizeOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelOrderProcessingCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelOrderProcessingReverseStockCommand, bool>, OrderCommandHandler>();

            services.AddScoped<INotificationHandler<DraftOrderStartedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<RejectedStockOrderEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<PaymentMadeEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<PaymentDeclinedEvent>, OrderEventHandler>();


            //Payment

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentCreditCardFacade, PaymentCreditCardFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<PaymentContext>();

            services.AddScoped<INotificationHandler<ConfirmedStockOrderEvent>, PaymentEventHandler>();



        }
    }
}