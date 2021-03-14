using System;

namespace OrdersApplication.ApplicationService.Commands
{
    public class ConfirmationCommand
    {
        public int OrderId { get; set; }
        public Guid AgentId { get; set; }
        public string OrderStatus { get; set; }
    }
}
