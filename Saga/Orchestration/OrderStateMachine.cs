using MassTransit;
using Saga.Contracts.Orchestrations;

namespace Saga.Saga;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public State AwaitingReserve { get; private set; } = null!;
    public State PaymentPending { get; private set; } = null!;
    public State Completed { get; private set; } = null!;
    public State Failed { get; private set; } = null!;
    
    public Event<CreateOrder> CreationOrderStarted { get; private set; } = null!;
    public Event<WarehouseReserved> WarehouseReserved { get; private set; } = null!;
    public Event<WarehouseReservationFailed> WarehouseReservationFailed { get; private set; } = null!;
    public Event<PaymentAccepted> PaymentAccepted { get; private set; } = null!;
    public Event<PaymentRejected> PaymentRejected { get; private set; } = null!;
    
    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CreationOrderStarted, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => WarehouseReserved, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => WarehouseReservationFailed, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => PaymentAccepted, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => PaymentRejected, x => x.CorrelateById(m => m.Message.OrderId));
        
        State(() => AwaitingReserve);
        State(() => PaymentPending);
        State(() => Completed);
        State(() => Failed);

        Initially(
            When(CreationOrderStarted)
                .Then(context =>
                {
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.BuyerId = context.Message.BuyerId;
                    context.Saga.ProductId = context.Message.ProductId;
                    context.Saga.Quantity = context.Message.Quantity;
                    context.Saga.Amount = context.Message.Amount;
                    context.Saga.Created = DateTime.UtcNow;
                })
                .Publish(context => new ReverseWarehouse
                (
                    context.Saga.OrderId,
                    context.Saga.ProductId,
                    context.Saga.Quantity
                ))
                .TransitionTo(AwaitingReserve)
        );
        
        During(AwaitingReserve, 
            When(WarehouseReserved)
                .Publish(context => new ProcessPayment
                    (
                        context.Saga.OrderId,
                        context.Saga.BuyerId,
                        context.Saga.Amount
                    ))
                .TransitionTo(PaymentPending),
            
            When(WarehouseReservationFailed)
                .Publish(context => new OrderCancelled(context.Saga.OrderId, "The quantity of the product is less than the requested value"))
                .TransitionTo(Failed)
        );
        
        During(PaymentPending,
            When(PaymentAccepted)
                .Then(context =>
                {
                    context.Saga.TransactionId = context.Message.TransactionId;
                })
                .Publish(context => new OrderCompleted
                    (
                        context.Saga.OrderId
                    ))
                .TransitionTo(Completed),
            
            When(PaymentRejected)
                .Publish(context => new ReleaseWarehouse
                    (
                        context.Saga.OrderId,
                        context.Saga.ProductId,
                        context.Saga.Quantity
                    ))
                .Publish(context => new OrderCancelled
                    (
                        context.Saga.OrderId,
                        "Payment rejected"
                    ))
                .TransitionTo(Failed)
        );
        
        During(Completed,
                Ignore(CreationOrderStarted),
                Ignore(WarehouseReserved),
                Ignore(PaymentAccepted)
        );
        
        During(Failed,
            Ignore(CreationOrderStarted),
            Ignore(WarehouseReserved),
            Ignore(PaymentAccepted)
        );
        
        SetCompletedWhenFinalized();
    }
}