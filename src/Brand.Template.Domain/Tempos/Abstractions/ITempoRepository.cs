using Brand.Template.Domain.Tempos.Models;

namespace Brand.Template.Domain.Tempos.Abstractions;

public interface ITempoRepository
{
    /// <summary>
    /// Busca o tempo corrente na cidade especificada
    /// </summary>
    /// <param name="cidade"></param>
    /// <returns></returns>
    Task<Tempo?> BuscarTempoAtual(Cidade cidade);
}