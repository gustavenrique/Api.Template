using Brand.Template.Application.Tempos.Abstractions;
using Brand.Template.Application.Tempos.Errors;
using Brand.Template.Domain.Tempos.Abstractions;
using Brand.Template.Domain.Tempos.Dtos;
using MapsterMapper;
using SharedKernel.ResultType;

namespace Brand.Template.Application.Tempos.Services;

internal sealed class CidadeService(
    ICidadeRepository repository,
    IMapper mapper
) : ICidadeService
{
    private ICidadeRepository _repository => repository;
    private IMapper _mapper => mapper;

    public async Task<Result<CidadeDto?>> BuscarPorNome(string cidade)
    {
        if (string.IsNullOrEmpty(cidade))
            return CidadeErrors.CidadeInvalida;

        var entity = await _repository.BuscarPorNome(cidade);

        if (entity is null) return CidadeErrors.CidadeNaoEncontrada;

        var dto = _mapper.Map<CidadeDto>(entity);

        return Result<CidadeDto?>.Ok(dto);
    }
}