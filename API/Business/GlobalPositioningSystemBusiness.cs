using Ford.Tracker.Api.Messaging.Constants;
using Ford.Tracker.Api.Messaging.Payload;
using Rebus.Bus;
using System;
using System.Threading.Tasks;

namespace Ford.Tracker.Api.Business
{
    public class GlobalPositioningSystemBusiness : IGlobalPositioningSystemBusiness
    {
        private IBus _bus;

        public GlobalPositioningSystemBusiness(IBus bus)
        {
            _bus = bus;
        }       

        public Task MapAndSendToMessengerAsync(string gpsInformation)
        {
            var task = new Task(() =>
            {
                _bus.Advanced.Routing.Send(QueueName.GlobalPositioningSystemPersistanceQueueName, new GlobalPositioningSystemMessage { test = gpsInformation });
            });

            task.Start();

            return task;
        }
    }
}
