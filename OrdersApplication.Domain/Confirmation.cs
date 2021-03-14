using System;

namespace OrdersApplication.Domain
{
    public class Confirmation
    {
        public Confirmation(int orderId, Guid agentId, string orderStatus)
        {
            OrderId = orderId;
            AgentId = agentId;
            OrderStatus = orderStatus;
        }

        public int OrderId { get; set; }
        public Guid AgentId { get; private set; }
        public string OrderStatus { get; private set; }
    }
}
