using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersApplication.ApplicationService.Commands;
using System.Net;
using System.Threading.Tasks;

namespace OrdersApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupervisorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SupervisorController> _logger;
        public SupervisorController( ILogger<SupervisorController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> Order(OrderCommand cmd)
        {
            await _mediator.Send(cmd);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost]
        [Route("Confirmation")]
        public async Task<IActionResult> Confirmation(ConfirmationCommand cmd)
        {
            await _mediator.Publish(cmd);
            return Ok();
        }
    }
}
