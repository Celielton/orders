using MediatR;
using System;
using System.Text.Json.Serialization;

namespace OrdersApplication.ApplicationService.Commands
{
    public abstract class BaseCommand : IRequest<bool>
    {
        [JsonIgnore]
        public TimeSpan TimeStamp { get; private set; }
        public BaseCommand()
        {
            TimeStamp = DateTime.Now.TimeOfDay;
        }
    }
}
