using Core.Contracts.Orchestrations;
using MassTransit;
using WarehouseReservationFailed = Core.Contracts.Choreography.WarehouseReservationFailed;
using WarehouseReserved = Core.Contracts.Choreography.WarehouseReserved;

namespace ProductService.Api.Consumers.Choreography;

public class ReserveWarehouseConsumer : IConsumer<ReverseWarehouse>
{
    private readonly IPublishEndpoint _publisher;
    
    public ReserveWarehouseConsumer(IPublishEndpoint publisher)
    {
        _publisher = publisher;
    }

    public async Task Consume(ConsumeContext<ReverseWarehouse> context)
    {
        var message = context.Message;

        if (message.Quantity <= 2)
            await _publisher.Publish(new WarehouseReserved(message.OrderId, message.ProductId, message.Quantity));

        await _publisher.Publish(new WarehouseReservationFailed(message.OrderId, message.ProductId));
    }
}