using System;
using Azure.Messaging.ServiceBus;
namespace ServiceBusImp
{
    public class ServiceBusSample1
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly string _queueName;
        public ServiceBusSample1(string connString, string queueName)
        {
            _serviceBusClient = new ServiceBusClient(connString);
            _queueName = queueName;
        }

        public async Task SendServiceBusMessage()
        {
           var sender = _serviceBusClient.CreateSender(_queueName);

            ServiceBusMessage message = new("Hello world!");

            // send the message
            await sender.SendMessageAsync(message);

            // create a receiver that we can use to receive the message
            ServiceBusReceiver receiver = _serviceBusClient.CreateReceiver(_queueName);

            // the received message is a different type as it contains some service set properties
            ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

            // get the message body as a string
            string body = receivedMessage.Body.ToString();
            Console.WriteLine(body);

        }

    }
}

