using System.Threading;
using System.Threading.Tasks;

namespace OrdersApplications.SharedKernel.Interfaces
{
    public interface IConsumer<T> where T : class
    {
        Task<T> ConsumeAsync(CancellationTokenSource toke);
    }
}
