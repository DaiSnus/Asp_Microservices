namespace Saga.Contracts.Choreography;

public record CreatingOrderStarted
(
    Guid OrderId,
    Guid BuyerId,
    Guid ProductId,
    int Quantity,
    decimal Amount
);

public record WarehouseReserved(Guid OrderId);

public record PaymentAccepted
(
    Guid OrderId,
    string TransactionId
);