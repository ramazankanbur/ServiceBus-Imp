using System.Text;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace ServiceBusProducer.Services
{
    public class AzureServiceBusMessageManegement
    {
        private readonly ServiceBusClient _serviceBusClient;
        public AzureServiceBusMessageManegement(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }
         
        public async Task SendMessageToQueueOrTopic(string queueOrTopicName, object message)
        {
            var byteArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var messageSender =_serviceBusClient.CreateSender(queueOrTopicName);
            var queueMessage = new ServiceBusMessage(byteArr);
            await messageSender.SendMessageAsync(queueMessage);
        }     
    }
}

