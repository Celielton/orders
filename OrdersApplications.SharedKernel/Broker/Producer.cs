using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrdersApplications.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace OrdersApplications.SharedKernel.Broker
{
    public class Producer<T> : IProducer<T> where T : class
    {
        private readonly QueueClient _queueClient;

        public Producer(IConfiguration configuration)
        {
            _queueClient = new QueueClient(configuration.GetSection(Constants.QUEUE_CONNECTION).Value, typeof(T).Name.ToLower());
            _queueClient.CreateIfNotExists();
        }

        public async Task PublishMessageAsync(T message)
        {
            await _queueClient.SendMessageAsync(JsonConvert.SerializeObject(message));
        }

        public async Task<T> ReceiveMessageAsync()
        {
            var response = await _queueClient.ReceiveMessageAsync();
            if (response.Value != null){
                await DeleteMessageAsync(response.Value);
                return JsonConvert.DeserializeObject<T>(response.Value.Body.ToString());
            }
             
            return null;
        }

        public async Task DeleteMessageAsync(QueueMessage message)
        {
            await _queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
        }

    }
}
