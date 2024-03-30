using RestApi.Template.Domain.Tempos.Models;

namespace RestApi.Template.Domain.Tempos.Abstractions;

public interface ICidadeRepository
{
    Task<Cidade?> BuscarPorNome(string cidade);
}