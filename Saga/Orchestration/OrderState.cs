using MassTransit;

namespace Saga.Saga;

public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    
    public string CurrentState { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Guid BuyerId { get; set; }
    
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Amount { get; set; }
    
    public string TransactionId { get; set; }
    
    public DateTime Created { get; set; }
}