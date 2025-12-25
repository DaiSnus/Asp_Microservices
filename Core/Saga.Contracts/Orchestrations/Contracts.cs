namespace Core.Saga.Contracts.Orchestrations;

public record StartOrderCreation
(
    Guid OrderId,
    Guid BuyerId,
    Guid ProductId,
    int Quantity,
    decimal Amount
);

public record OrderCompleted
(
    Guid OrderId
);

public record OrderCancelled
(
    Guid OrderId,
    string Reason
);

public record ReverseWarehouse
(
    Guid OrderId,
    Guid ProductId,
    int Quantity
);
    
public record WarehouseReserved
(
    Guid OrderId,
    Guid ProductId,
    int Quantity
);
    
public record WarehouseReservationFailed
(
    Guid OrderId,
    Guid ProductId
);
    
public record ReleaseStock
(
    Guid OrderId,
    Guid ProductId,
    int Quantity
);    

public record ProcessPayment
(
    Guid OrderId,
    Guid BuyerId,
    decimal Amount
);

public record PaymentAccepted
(
    Guid OrderId,
    Guid BuyerId,
    string TransactionId
);

public record PaymentRejected
(
    Guid OrderId,
    string Reason
);