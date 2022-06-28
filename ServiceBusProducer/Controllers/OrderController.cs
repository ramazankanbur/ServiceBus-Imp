using Microsoft.AspNetCore.Mvc;
using Common.dto;
using ServiceBusProducer.Services;
using Common.Events;
using Common;


namespace ServiceBusProducer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly AzureServiceBusMessageManegement _azureService;
        private readonly AzureServiceBusAdministration _azureServiceBusAdministration;
        public OrderController(AzureServiceBusMessageManegement azureService, AzureServiceBusAdministration azureServiceBusAdministration)
        {
            _azureServiceBusAdministration = azureServiceBusAdministration;
            _azureService = azureService;
        }

        [HttpPost]
        public async Task CreateOrder(OrderDto orderDto)
        {
            var orderCreatedEvent = new OrderCreatedEvent() { CreatedOn = DateTime.Now, Id = orderDto.Id, ProductName = orderDto.OrderName };
            await _azureServiceBusAdministration.CreateQueueIfNotExists(Constants.OrderCreatedQueueName);
            await _azureService.SendMessageToQueueOrTopic(Constants.OrderCreatedQueueName, orderCreatedEvent);

        }

        [HttpDelete]
        public async Task DeleteOrder(int id)
        {
            var orderCreatedEvent = new OrderDeletedEvent() { CreatedOn = DateTime.Now, Id =id};
            await _azureServiceBusAdministration.CreateQueueIfNotExists(Constants.OrderDeletedQueueName);
            await _azureService.SendMessageToQueueOrTopic(Constants.OrderDeletedQueueName, orderCreatedEvent);

        }
    }
}

