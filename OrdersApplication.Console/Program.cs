using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersApplications.SharedKernel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersApplication.ConsoleApp
{
    class Program
    {
        private static ILog _log;
        static async Task Main(string[] args)
        {
            var configuration = Configure();

            CancellationTokenSource tokenSource = new CancellationTokenSource();

            _log = LoggerConfiguration.CreateLogger();
            var serviceCollection = new ServiceCollection();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddSingleton(_log);

            _log.Info("Starting Application");

            Thread[] threads = new Thread[5];
            for (var i = 0; i < 5; i++)
            {
                threads[i] = new Thread(new ThreadStart(() => new OrderConsumer(_log, configuration).ExecuteAsync(tokenSource).ConfigureAwait(false)));
                threads[i].Start();
            }

            Console.ReadKey();
        }


        private static IConfiguration Configure()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);

            return builder.Build();
        }

    }
}
