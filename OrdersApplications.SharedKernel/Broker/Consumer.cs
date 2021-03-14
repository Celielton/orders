using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using OrdersApplications.SharedKernel.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrdersApplications.SharedKernel.Broker
{
    public abstract class Consumer<T> 
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
            _table = client.GetTableReference(Constants.CONFIRMATION_TABLE_NAME);
            _table.CreateIfNotExistsAsync().ConfigureAwait(false).GetAwaiter();
        }

        public async Task SabeToTable(TableEntity entity)
        {
            TableOperation insertOperation = TableOperation.InsertOrReplace(entity);
            await _table.ExecuteAsync(insertOperation);
        }
    }
}
