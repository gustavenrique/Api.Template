namespace RestApi.Common.Abstractions.Domain;

// referência: https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/master/CSharpFunctionalExtensions/ValueObject/ValueObject.cs

/// <summary>
/// Marker interface para Value Objects; Pode ser útil para records simples,
/// que não precisem implementar o <see cref="ValueObject" />
/// </summary>
public interface IValueObject;

/// <summary>
/// Representa um Value Object e abstrai lógicas de comparação relacionadas
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>, IValueObject
{
    int? _cachedHashCode;

    public abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other) => Equals((object?)other);

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;

        var otherValueObject = (ValueObject)obj;

        return GetEqualityComponents()
            .SequenceEqual(otherValueObject.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (left is null && right is null) return true;

        return Equals(left, right);
    }

    public static bool operator !=(ValueObject left, ValueObject right) =>
        !(left == right);

    public override int GetHashCode()
    {
        if (!_cachedHashCode.HasValue)
            _cachedHashCode = GetEqualityComponents()
                .Aggregate(1, (total, value) =>
                    unchecked(total * 23 + (value?.GetHashCode() ?? 0))
                );

        return _cachedHashCode.Value;
    }
}