using Brand.Template.Domain.Tempos.Models.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.ResultType;

namespace Brand.Template.Domain.Tempos.Models;

public sealed class Tempo(
    int id,
    string descricao,
    Temperatura temperatura,
    Temperatura sensacaoTermica,
    int humidade,
    Cidade cidade
) : AggregateRoot<int>(id)
{
    /// <summary>
    /// Descrição do tempo. e.g. Chuvoso, Nublado, etc
    /// </summary>
    public string Descricao { get; private set; } = descricao;

    public Temperatura Temperatura { get; private set; } = temperatura;
    public Temperatura SensacaoTermica { get; private set; } = sensacaoTermica;

    /// <summary>
    /// Humidade em termos percentuais. e.g. Um 50 representaria 50%
    /// </summary>
    public int Humidade { get; private set; } = humidade;
    public Cidade Cidade { get; private set; } = cidade;

    public Result DiminuirTemperatura(decimal celsiusDiminuidos)
    {
        var novaTemperatura = Temperatura
            .Criar(Temperatura.Celsius - celsiusDiminuidos);

        if (!novaTemperatura.HasValue) return new Result(novaTemperatura);

        decimal temperaturaAntiga = Temperatura.Celsius;

        Temperatura = novaTemperatura.Value!;

        Raise(new TemperaturaDiminuida(
            Id,
            temperaturaAntiga,
            Temperatura.Celsius
        ));

        return Result.Empty();
    }
}