using Brand.Template.Domain.Tempos.Dtos;
using SharedKernel.ResultType;

namespace Brand.Template.Application.Tempos.Abstractions;

public interface ICidadeService
{
    Task<Result<CidadeDto?>> BuscarPorNome(string cidade);
}