using ServiceBusConsumer;
using Common;
using Common.Events;

ConsumerMethods.ConsumeQueue<OrderCreatedEvent>(Constants.OrderCreatedQueueName, i =>
{
    Console.WriteLine($"OrderCreatedEvent ReceivedMessage with id: {i.Id}, Name: {i.ProductName}");
}).Wait();

ConsumerMethods.ConsumeQueue<OrderDeletedEvent>(Constants.OrderDeletedQueueName, i =>
{
    Console.WriteLine($"OrderDeletedEvent ReceivedMessage with id: {i.Id}");
}).Wait();
 
Console.ReadKey();
