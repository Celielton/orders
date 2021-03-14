using Azure.Storage.Queues;
using log4net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrdersApplication.ApplicationService.Commands;
using OrdersApplication.ApplicationService.Interfaces;
using OrdersApplication.ApplicationService.ViewModel;
using OrdersApplication.Domain;
using OrdersApplications.SharedKernel;
using OrdersApplications.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace OrdersApplication.ApplicationService.Services
{
    public class OrderSupervisorAppService : IOrderSupervisorAppService
    {

        private readonly ILogger _logger;
        private readonly IProducer<Order> _orderBroker;
        private readonly IProducer<Confirmation> _confirmationBroker;
        private readonly IProducer<OrderSupervisorViewModel> _orderSupervisorBroker;
        public OrderSupervisorAppService(ILogger<OrderSupervisorAppService> logger, IProducer<Order> orderBroker, IProducer<Confirmation> confirmationBroker,
        IProducer<OrderSupervisorViewModel> orderSupervisorBroker)
        {
            _logger = logger;
            _orderBroker = orderBroker;
            _confirmationBroker = confirmationBroker;
            _orderSupervisorBroker = orderSupervisorBroker;
        }

        public async Task CreateOrderAsync(OrderCommand cmd)
        {
            var orderViewModel = await _orderSupervisorBroker.ReceiveMessageAsync() ?? new OrderSupervisorViewModel() { OrderId = 1 };
            var order = new Order(orderViewModel.OrderId, cmd.OrderText);
            _logger.LogInformation(string.Format(Constants.SEND_ORDER_MESSAGE, order.Id, order.Random));
            await _orderBroker.PublishMessageAsync(order);
            orderViewModel.IncrementOrderId();
            await _orderSupervisorBroker.PublishMessageAsync(orderViewModel);
        }

        public void ReceiveConfirmation(ConfirmationCommand cmd)
        {
            _logger.LogInformation(string.Format(Constants.CONFIRMATION_RECEIVED, cmd.OrderId, cmd.AgentId));
        }
    }
}
