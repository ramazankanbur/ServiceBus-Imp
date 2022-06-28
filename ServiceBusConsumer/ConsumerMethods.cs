using Azure.Messaging.ServiceBus;
using Common;

namespace ServiceBusConsumer
{
	public static class ConsumerMethods
	{
		public static async Task ConsumeQueue<T>(string queueOrTopicName, Action<T> receivedAction)
		{
			ServiceBusClient serviceBusClient = new(Constants.SERVICEBUSCONNSTRING);
			var processor = serviceBusClient.CreateProcessor(queueOrTopicName, new ServiceBusProcessorOptions());

            processor.ProcessMessageAsync += async (ProcessMessageEventArgs arg) =>
            {
                var body = arg.Message.Body.ToObjectFromJson<T>();

                receivedAction(body);

                await arg.CompleteMessageAsync(arg.Message);
            };
            processor.ProcessErrorAsync += ErrorHandler;
 
            await processor.StartProcessingAsync();

			Console.WriteLine($"{typeof(T).Name} is listening");
          
            static Task ErrorHandler(ProcessErrorEventArgs args)
            {
                Console.WriteLine(args.Exception.ToString());
                return Task.CompletedTask;
            }
        }
    }
}

