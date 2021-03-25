using MediatR;
using Microsoft.Extensions.Logging;
using OrdersApplication.ApplicationService.Commands;
using OrdersApplication.ApplicationService.Interfaces;
using OrdersApplication.ApplicationService.ViewModel;
using OrdersApplication.Domain;
using OrdersApplications.SharedKernel;
using OrdersApplications.SharedKernel.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersApplication.ApplicationService.Services
{
    public class OrderSupervisorAppService : INotificationHandler<ConfirmationCommand>, IRequestHandler<OrderCommand, bool>
    {

        private readonly ILogger _logger;
        private readonly IProducer<Order> _orderBroker;
        private readonly IProducer<OrderSupervisorViewModel> _orderSupervisorBroker;
        public OrderSupervisorAppService(ILogger<OrderSupervisorAppService> logger,
            IProducer<Order> orderBroker,
            IProducer<OrderSupervisorViewModel> orderSupervisorBroker)
        {
            _logger = logger;
            _orderBroker = orderBroker;
            _orderSupervisorBroker = orderSupervisorBroker;
        }

        public async Task Handle(ConfirmationCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(string.Format(Constants.CONFIRMATION_RECEIVED, notification.OrderId, notification.AgentId));
            await Task.CompletedTask;
        }

        public async Task<bool> Handle(OrderCommand request, CancellationToken cancellationToken)
        {
            var orderViewModel = await _orderSupervisorBroker.ReceiveMessageAsync() ?? new OrderSupervisorViewModel() { OrderId = 1 };
            var order = new Order(orderViewModel.OrderId, request.OrderText);
            _logger.LogInformation(string.Format(Constants.SEND_ORDER_MESSAGE, order.Id, order.Random));
            await _orderBroker.PublishMessageAsync(order);
            //orderViewModel.IncrementOrderId();
            //await _orderSupervisorBroker.PublishMessageAsync(orderViewModel);
            return await Task.FromResult(true);
        }
    }
}
