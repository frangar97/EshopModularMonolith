using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Products.EventHandlers
{
    public class ProductPriceChangedEventHandler(IBus bus,ILogger<ProductCreatedEventHandler> logger) : INotificationHandler<ProductPriceChangedEvent>
    {
        public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new ProductPriceChangedIntegrationEvent
            {
                ProductId = notification.product.Id,
                Name = notification.product.Name,
                Category = notification.product.Category,
                Description = notification.product.Description,
                ImageFile = notification.product.ImageFile,
                Price = notification.product.Price //set updated product price
            };

            await bus.Publish(integrationEvent, cancellationToken);
        }
    }
}
