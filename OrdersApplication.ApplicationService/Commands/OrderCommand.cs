using MediatR;

namespace OrdersApplication.ApplicationService.Commands
{
    public class OrderCommand : BaseCommand
    {
        public string OrderText { get; set; }
    }
}
