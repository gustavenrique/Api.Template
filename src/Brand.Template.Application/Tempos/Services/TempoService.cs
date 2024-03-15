using Brand.Template.Application.Tempos.Abstractions;
using Brand.Template.Application.Tempos.Errors;
using Brand.Template.Domain.Tempos.Abstractions;
using Brand.Template.Domain.Tempos.Dtos;
using Brand.Template.Domain.Tempos.Models;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using SharedKernel.Abstractions.Events;
using SharedKernel.ResultType;

namespace Brand.Template.Application.Tempos.Services;

internal sealed class TempoService(
    ITempoRepository tempoRepository,
    ICidadeService cidadeService,
    ILogger<TempoService> logger,
    IMapper mapper,
    IEventDispatcher eventDispatcher
) : ITempoService
{
    private ITempoRepository _tempoRepository => tempoRepository;
    private ICidadeService _cidadeService => cidadeService;
    private ILogger<TempoService> _logger => logger;
    private IMapper _mapper => mapper;
    private IEventDispatcher _eventDispatcher => eventDispatcher;

    public async Task<Result<TempoDto?>> BuscarPorCidade(string cidade)
    {
        if (string.IsNullOrEmpty(cidade)) return TempoErrors.CidadeInvalida;

        Result<CidadeDto?> cidadeResult = await _cidadeService.BuscarPorNome(cidade);

        if (!cidadeResult.HasValue)
        {
            return new(cidadeResult);
        }

        var tempo = await _tempoRepository.BuscarTempoAtual(
            _mapper.Map<Cidade>(cidadeResult.Value!)
        );

        if (tempo is null)
        {
            _logger.LogError("[BuscarPorCidade] Tempos nulo - Cidade: {Cidade}", cidade);

            return TempoErrors.TempoNaoEncontrado;
        }

        var dto = _mapper.Map<TempoDto?>(tempo);

        return Result<TempoDto?>.Ok(dto);
    }

    public async Task<Result> DiminuirTemperatura(DiminuirTemperaturaDto input)
    {
        Result<TempoDto?> tempoResult = await BuscarPorCidade(input.Cidade);

        if (!tempoResult.HasValue)
        {
            return new(tempoResult);
        }

        Tempo tempo = tempoResult.Value!.ParaDomain();

        Result result = tempo.DiminuirTemperatura(input.CelsiusDiminuidos);

        if (result.IsFailure)
        {
            return new(result);
        }

        _eventDispatcher.Dispatch(tempo.PopEvents());

        return Result.Empty();
    }
}