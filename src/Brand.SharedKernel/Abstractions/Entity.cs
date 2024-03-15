using SharedKernel.Abstractions.Events;

namespace SharedKernel.Abstractions;

/*
    Referências:
    - https://github.com/amantinband/clean-architecture/blob/main/src/CleanArchitecture.Domain/Common/Entity.cs
    - https://www.youtube.com/watch?v=weGLBPky43U
*/

/// <summary>
/// Abstrai lógicas comuns de entities
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <param name="id"></param>
public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>>
    where TId : notnull
{
    private Queue<IEvent> _events = [];

    public TId Id => id;

    /// <summary>
    /// Registra o acontecimento de um domain event
    /// </summary>
    /// <param name="domainEvent"></param>
    protected void Raise(IEvent domainEvent) =>
        _events.Enqueue(domainEvent);

    /// <summary>
    /// Retorna e limpa os registros de events do domain object
    /// </summary>
    /// <returns></returns>
    public IReadOnlyCollection<IEvent> PopEvents()
    {
        var events = _events;
        _events = [];
        return events;
    }

    public bool Equals(Entity<TId>? other) => Equals((object?)other);

    public override bool Equals(object? obj) =>
        obj is Entity<TId> other && other.Id.Equals(Id);

    public static bool operator ==(Entity<TId> one, Entity<TId> another) =>
        Equals(one, another);

    public static bool operator !=(Entity<TId> one, Entity<TId> another) =>
        !Equals(one, another);

    public override int GetHashCode() => Id.GetHashCode();
}