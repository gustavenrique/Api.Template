using RestApi.Template.Application.Tempos.Dtos;
using RestApi.Template.Domain.Tempos.Dtos;
using RestApi.Common.Types.Output;

namespace RestApi.Template.Application.Tempos.Abstractions;

public interface ITempoService
{
    /// <summary>
    /// Busca o tempo de uma cidade, pelo seu nome
    /// </summary>
    /// <param name="cidade">Nome da cidade, em qualquer idioma</param>
    /// <returns>Eventual tempo</returns>
    Task<Result<TempoDto?>> BuscarPorCidade(string cidade);

    /// <summary>
    /// Diminui a temperatura de uma cidade, de acordo com a quantidade especificada em Celsius
    /// </summary>
    Task<Result> DiminuirTemperatura(DiminuirTemperaturaDto input);
}