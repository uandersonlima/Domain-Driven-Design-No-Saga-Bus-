using AscoreStore.Core.Communication.Mediator;
using AscoreStore.Core.DomainObjects.DTO;
using AscoreStore.Core.Extensions;
using AscoreStore.Core.Messages;
using AscoreStore.Core.Messages.Common.IntegrationEvents;
using AscoreStore.Core.Messages.Common.Notifications;
using AscoreStore.Sales.Application.Commands;
using AscoreStore.Sales.Application.Events;
using AscoreStore.Sales.Domain.OrderAggregate;
using MediatR;

namespace AscoreStore.Sales.Application.Handlers
{
    public class OrderCommandHandler :
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<ApplyOrderVoucherCommand, bool>,
        IRequestHandler<CancelOrderProcessingCommand, bool>,
        IRequestHandler<CancelOrderProcessingReverseStockCommand, bool>,
        IRequestHandler<FinalizeOrderCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>,
        IRequestHandler<StartOrderCommand, bool>,
        IRequestHandler<UpdateOrderItemCommand, bool>

    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddOrderItemCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerIdAsync(command.CustomerId);
            var orderItem = new OrderItem(command.ProductId, command.Name, command.Quantity, command.UnitaryValue);

            if (order is null)
            {
                order = Order.OrderFactory.NewDraftOrder(command.CustomerId);
                order.AddItem(orderItem);

                _orderRepository.Add(order);
                order.AddEvent(new DraftOrderStartedEvent(command.CustomerId, command.ProductId));
            }
            else
            {
                var existingOrderItem = order.ExistingOrderItem(orderItem);
                order.AddItem(orderItem);

                if (existingOrderItem)
                {
                    _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(orderItem);
                }

                order.AddEvent(new UpdatedOrderEvent(order.CustomerId, order.Id, order.TotalValue));
            }

            order.AddEvent(new OrderItemAddedEvent(order.CustomerId, order.Id, command.ProductId, command.Name, command.UnitaryValue, command.Quantity));

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ApplyOrderVoucherCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerIdAsync(command.CustomerId);

            if (order is null)
            {
                await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Pedido não encontrado!"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCodeAsync(command.VoucherCode);

            if (voucher is null)
            {
                await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Voucher não encontrado!"));
                return false;
            }

            var voucherApplicationValidation = order.ApplyVoucher(voucher);

            if (!voucherApplicationValidation.IsValid)
            {
                foreach (var error in voucherApplicationValidation.Errors)
                {
                    await _mediatorHandler.PublishNotificationAsync(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            order.AddEvent(new UpdatedOrderEvent(order.CustomerId, order.Id, order.TotalValue));
            order.AddEvent(new VoucherAppliedOrderEvent(command.CustomerId, order.Id, voucher.Id));

            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelOrderProcessingCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            if (order is null)
            {
                await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Pedido não encontrado!"));
                return false;
            }

            order.MakeDraft();

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelOrderProcessingReverseStockCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            if (order is null)
            {
                await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Pedido não encontrado!"));
                return false;
            }

            var itemsList = new List<Item>();
            order.OrderItems.ForEach(i => itemsList.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));
            var orderProductList = new OrderProductList { OrderId = order.Id, Items = itemsList };

            order.AddEvent(new OrderProcessingCanceledEvent(order.Id, order.CustomerId, orderProductList));
            order.MakeDraft();

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(FinalizeOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);

            if (order is null)
            {
                await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Pedido não encontrado!"));
                return false;
            }

            order.FinalizeOrder();

            order.AddEvent(new FinishedOrderEvent(command.OrderId));
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveOrderItemCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerIdAsync(command.CustomerId);

            if (order is null)
            {
                await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Pedido não encontrado!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrderAsync(order.Id, command.ProductId);

            if (orderItem is null || !order.ExistingOrderItem(orderItem))
            {
                await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Item do order não encontrado!"));
                return false;
            }

            order.RemoveItem(orderItem);
            order.AddEvent(new UpdatedOrderEvent(order.CustomerId, order.Id, order.TotalValue));
            order.AddEvent(new OrderProductRemovedEvent(command.CustomerId, order.Id, command.ProductId));

            _orderRepository.RemoveItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(StartOrderCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerIdAsync(command.CustomerId);
            order.StartOrder();

            var itemsList = new List<Item>();
            order.OrderItems.ForEach(i => itemsList.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));
            var orderProductList = new OrderProductList { OrderId = order.Id, Items = itemsList };

            order.AddEvent(new OrderStartedEvent(order.Id, order.CustomerId, order.TotalValue, orderProductList, command.CardName, command.CardNumber, command.CardExpiration, command.CardCvv));

            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerIdAsync(command.CustomerId);

            if (order is null)
            {
                await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Pedido não encontrado!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrderAsync(order.Id, command.ProductId);

            if (!order.ExistingOrderItem(orderItem))
            {
                await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Item do order não encontrado!"));
                return false;
            }

            order.UpdateUnits(orderItem, command.Quantity);

            order.AddEvent(new UpdatedOrderEvent(order.CustomerId, order.Id, order.TotalValue));
            order.AddEvent(new OrderProductUpdatedEvent(command.CustomerId, order.Id, command.ProductId, command.Quantity));

            _orderRepository.UpdateItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }


        private bool ValidateCommand(Command command)
        {
            if (command.IsValid()) return true;

            foreach (var error in command.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotificationAsync(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}