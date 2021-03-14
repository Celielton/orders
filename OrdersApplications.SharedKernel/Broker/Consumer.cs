using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;

namespace OrdersApplications.SharedKernel.Broker
{
    //: IConsumer<T>
    public abstract class Consumer<T> where T : class
    {
        protected readonly QueueClient _queueClient;
        protected readonly Random _random;
        protected readonly CloudTable _table;

        public int MagicNumber { get; private set; }
        public Guid AgentId { get; private set; }
        public Consumer(IConfiguration configuration)
        {
            _random = new Random();
            MagicNumber = _random.Next(1, 10);
            AgentId = Guid.NewGuid();
            _queueClient = new QueueClient(configuration.GetSection(Constants.QUEUE_CONNECTION).Value, typeof(T).Name.ToLower());
            _queueClient.CreateIfNotExists();
            var account = CloudStorageAccount.Parse(configuration.GetSection(Constants.QUEUE_CONNECTION).Value);
            var client = account.CreateCloudTableClient();
            _table = client.GetTableReference("confirmations");
            _table.CreateIfNotExistsAsync().ConfigureAwait(false).GetAwaiter();
        }

        public async Task SabeToTable(TableEntity entity)
        {
            TableOperation insertOperation = TableOperation.InsertOrReplace(entity);
            await _table.ExecuteAsync(insertOperation);
        }
    }

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
