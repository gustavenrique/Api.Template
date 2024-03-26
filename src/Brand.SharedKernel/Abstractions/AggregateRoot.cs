namespace Brand.SharedKernel.Abstractions;

/// <summary>
/// Representa um Aggregate Root e abstrai lógicas de comparação relacionadas
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <param name="id"></param>
public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id)
    where TId : notnull
{
}