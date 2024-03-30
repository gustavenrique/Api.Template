using Brand.Template.Domain.Tempos.Dtos;
using Brand.Common.Types.Output;

namespace Brand.Template.Application.Tempos.Abstractions;

public interface ICidadeService
{
    Task<Result<CidadeDto?>> BuscarPorNome(string cidade);
}