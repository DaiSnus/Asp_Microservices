using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saga.Saga;

namespace Saga;

public class SagaDbContext : MassTransit.EntityFrameworkCoreIntegration.SagaDbContext
{
    public SagaDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<OrderState> OrderStates { get; set; }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new OrderStateMap();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddTransactionalOutboxEntities();
        
        base.OnModelCreating(modelBuilder);
    }
}

public class OrderStateMap : SagaClassMap<OrderState>
{
    protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder builder)
    {
        entity.ToTable("order_state_saga");
        entity.HasKey(e => e.CorrelationId);
        entity.HasIndex(e => e.OrderId);
        entity.Property(e => e.CurrentState).HasMaxLength(64);
        entity.HasIndex(e => e.CurrentState);
    }
}