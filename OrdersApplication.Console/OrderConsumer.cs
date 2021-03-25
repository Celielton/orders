using Azure.Storage.Queues.Models;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrdersApplication.Domain;
using OrdersApplications.SharedKernel;
using OrdersApplications.SharedKernel.Broker;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersApplication.ConsoleApp
{
    public class OrderConsumer : Consumer<Order>
    {
        private readonly ILog _log;
        private readonly HttpClient _client;
        public OrderConsumer(ILog log, IConfiguration configuration) : base(configuration)
        {
            _log = log;
            _log.Info(string.Format(Constants.AGENT_APP_MESSAGE, AgentId, MagicNumber));
            _client = new HttpClient();
        }

        public async Task ExecuteAsync(CancellationTokenSource tokenSource)
        {

            while (!tokenSource.Token.IsCancellationRequested)
            {
                var message = await _queueClient.ReceiveMessageAsync();

                if (message.Value != null)
                {
                    var order = JsonConvert.DeserializeObject<Order>(message.Value.Body.ToString());

                    if (order.Random == MagicNumber)
                    {
                        _log.Warn(Constants.MAGIC_NUMBER_FOUND_MESSAGE);
                        tokenSource.Cancel();
                        _log.Info("Press any key to exit");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        //what about here If for any reason my application stop and I miss any step?
                        _log.Info(string.Format(Constants.ORDER_RECEIVED, order.Id, order.Text));
                        await DeleteMessageAsync(message);
                        await SabeToTable(new ConfirmationEntity(order.Id, AgentId, Constants.MESSAGE_PROCESSED));
                        await SendConfimation(new Confirmation(order.Id, AgentId, Constants.MESSAGE_PROCESSED));
                    }
                }
                await Task.Delay(5, tokenSource.Token);
            }
        }

        public async Task SendConfimation(Confirmation confirmation)
        {
            var json = new StringContent(JsonConvert.SerializeObject(confirmation), Encoding.UTF8, "application/json");
            await _client.PostAsync($"{ Constants.API_URL}/supervisor/confirmation", json);
        }

        protected async Task DeleteMessageAsync(QueueMessage message)
        {
            await _queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
        }
    }

}
