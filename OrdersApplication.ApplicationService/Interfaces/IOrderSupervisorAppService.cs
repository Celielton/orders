using OrdersApplication.ApplicationService.Commands;
using System.Threading.Tasks;

namespace OrdersApplication.ApplicationService.Interfaces
{
    public interface IOrderSupervisorAppService
    {
        Task CreateOrderAsync(OrderCommand cmd);
        void ReceiveConfirmation(ConfirmationCommand cmd);
    }
}
