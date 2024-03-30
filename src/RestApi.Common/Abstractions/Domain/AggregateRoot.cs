namespace RestApi.Common.Abstractions.Domain;

/// <summary>
/// Representa um Aggregate Root e abstrai lógicas de comparação relacionadas
/// </summary>
public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id)
    where TId : notnull;