using Brand.SharedKernel.Types.Output;
using Brand.SharedKernel.Abstractions;

namespace Brand.Template.Domain.Tempos.Models.ValueObjects;

public sealed class Temperatura : ValueObject
{
    private const decimal ZeroAbsoluto = -273.15m;

    public decimal Celsius { get; init; }
    public decimal Fahrenheit => 32 + (int)(Celsius / 0.5556m);
    public decimal Kelvin => Celsius + 273.15m;

    private Temperatura(decimal celsius) => Celsius = celsius;

    public static Result<Temperatura?> Criar(decimal celsius)
    {
        if (celsius < ZeroAbsoluto)
            return Result<Temperatura?>.InvalidInput(["Não é possível existir temperatura menor que o zero absoluto"]);

        return Result<Temperatura?>.Created(new Temperatura(celsius));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Celsius;
        yield return Fahrenheit;
        yield return Kelvin;
    }
}