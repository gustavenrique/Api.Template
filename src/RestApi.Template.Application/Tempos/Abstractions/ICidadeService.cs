using RestApi.Common.Types.Output;
using RestApi.Template.Domain.Tempos.Dtos;

namespace RestApi.Template.Application.Tempos.Abstractions;

public interface ICidadeService
{
    Task<Result<CidadeDto?>> BuscarPorNome(string cidade);
}