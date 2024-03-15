using Brand.Template.Domain.Tempos.Models;

namespace Brand.Template.Domain.Tempos.Abstractions;

public interface ICidadeRepository
{
    Task<Cidade?> BuscarPorNome(string cidade);
}