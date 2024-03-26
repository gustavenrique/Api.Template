using Brand.SharedKernel.Abstractions;

namespace Brand.Template.Domain.Tempos.Models.ValueObjects;

public sealed class CidadeId(
    decimal latitude,
    decimal longitude
) : ValueObject
{
    public decimal Latitude => latitude;
    public decimal Longitude => longitude;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}