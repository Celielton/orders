using Microsoft.Extensions.DependencyInjection;
using OrdersApplication.ApplicationService.Interfaces;
using OrdersApplication.ApplicationService.Services;
using OrdersApplication.ApplicationService.ViewModel;
using OrdersApplication.Domain;
using OrdersApplications.SharedKernel.Broker;
using OrdersApplications.SharedKernel.Interfaces;

namespace OrdersApplication.ApplicationService.DependencyInjection
{
    public static class ServicesConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderSupervisorAppService, OrderSupervisorAppService>();
            services.AddScoped<IProducer<Order>, Producer<Order>>();
            services.AddScoped<IProducer<Confirmation>, Producer<Confirmation>>();
            services.AddScoped<IProducer<OrderSupervisorViewModel>, Producer<OrderSupervisorViewModel>>();
        }
    }
}
