using Brand.SharedKernel.Types.Output;
using Brand.Template.Domain.Tempos.Dtos;

namespace Brand.Template.Application.Tempos.Abstractions;

public interface ICidadeService
{
    Task<Result<CidadeDto?>> BuscarPorNome(string cidade);
}