using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrdersApplication.ApplicationService.Commands;
using OrdersApplication.ApplicationService.Interfaces;
using OrdersApplication.ApplicationService.Services;
using OrdersApplication.ApplicationService.ViewModel;
using OrdersApplication.Domain;
using OrdersApplications.SharedKernel.Broker;
using OrdersApplications.SharedKernel.Interfaces;
using System.Reflection;

namespace OrdersApplication.ApplicationService.DependencyInjection
{
    public static class ServicesConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IProducer<Order>, Producer<Order>>();
            services.AddScoped<IProducer<Confirmation>, Producer<Confirmation>>();
            services.AddScoped<IProducer<OrderSupervisorViewModel>, Producer<OrderSupervisorViewModel>>();
            services.AddScoped<IRequestHandler<OrderCommand, bool>, OrderSupervisorAppService>();
            services.AddScoped<INotificationHandler<ConfirmationCommand>, OrderSupervisorAppService>();
        }
    }

}