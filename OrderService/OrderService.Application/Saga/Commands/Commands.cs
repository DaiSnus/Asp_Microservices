namespace OrderService.Application.Saga.Commands.Orchestration;

public record ReserveWarehouseCommand
(
    Guid OrderId,
    Guid ProductId,
    int Quantity
);

public record ProcessPaymentCommand
(
    Guid OrderId,
    Guid BuyerId,
    decimal Amount    
);