using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersApplication.ApplicationService.Commands;
using OrdersApplication.ApplicationService.Interfaces;
using System.Threading.Tasks;

namespace OrdersApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupervisorController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IOrderSupervisorAppService _orderSupervisorAppService;


        public SupervisorController(IOrderSupervisorAppService orderSupervisorAppService, ILogger<SupervisorController> logger)
        {
            _logger = logger;
            _orderSupervisorAppService = orderSupervisorAppService;

        }

        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> Order(OrderCommand cmd)
        {
            await _orderSupervisorAppService.CreateOrderAsync(cmd);
            return Ok();
        }

        [HttpPost]
        [Route("Confirmation")]
        public IActionResult Confirmation(ConfirmationCommand cmd)
        {
            _orderSupervisorAppService.ReceiveConfirmation(cmd);
            return Ok();
        }
    }
}
