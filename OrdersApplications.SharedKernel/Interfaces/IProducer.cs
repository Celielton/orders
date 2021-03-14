using Azure.Storage.Queues.Models;
using System.Threading.Tasks;

namespace OrdersApplications.SharedKernel.Interfaces
{
    public interface IProducer<T> where T : class
    {
        Task PublishMessageAsync(T message);
        Task<T> ReceiveMessageAsync();
        Task DeleteMessageAsync(QueueMessage message);
    }
}
