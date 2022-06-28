using System;
using Azure.Messaging.ServiceBus.Administration;

namespace ServiceBusProducer.Services
{
	public class AzureServiceBusAdministration
	{
        private readonly ServiceBusAdministrationClient _administartionClient;

        public AzureServiceBusAdministration(ServiceBusAdministrationClient administartionClient)
        {
            _administartionClient = administartionClient;
        }

        public async Task CreateQueueIfNotExists(string queueName)
        {
            if (!await _administartionClient.QueueExistsAsync(queueName))
            {
                await _administartionClient.CreateQueueAsync(queueName);
            }
        }
    }
}

