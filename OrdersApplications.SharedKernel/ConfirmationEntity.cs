using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace OrdersApplications.SharedKernel.Broker
{
    public class ConfirmationEntity : TableEntity
    {
        public ConfirmationEntity(int orderId, Guid agentId, string orderStatus)
        {
            this.OrderId = orderId;
            this.AgentId = agentId;
            OrderStatus = orderStatus;
            this.PartitionKey = orderId.ToString(); this.RowKey = agentId.ToString();
        }

        public int OrderId { get; set; }
        public Guid AgentId { get; set; }
        public string OrderStatus { get; set; }
    }
}
