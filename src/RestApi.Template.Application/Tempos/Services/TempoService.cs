using MapsterMapper;
using Microsoft.Extensions.Logging;
using RestApi.Common.Abstractions.Domain.Events;
using RestApi.Common.Types.Output;
using RestApi.Template.Application.Tempos.Abstractions;
using RestApi.Template.Application.Tempos.Dtos;
using RestApi.Template.Application.Tempos.Errors;
using RestApi.Template.Domain.Tempos.Abstractions;
using RestApi.Template.Domain.Tempos.Dtos;
using RestApi.Template.Domain.Tempos.Models;

namespace RestApi.Template.Application.Tempos.Services;

internal sealed class TempoService(
    ITempoRepository tempoRepository,
    ICidadeService cidadeService,
    ILogger<TempoService> logger,
    IMapper mapper,
    IDomainEventDispatcher eventDispatcher
) : ITempoService
{
    readonly ITempoRepository _tempoRepository = tempoRepository;
    readonly ICidadeService _cidadeService = cidadeService;
    readonly ILogger<TempoService> _logger = logger;
    readonly IMapper _mapper = mapper;
    readonly IDomainEventDispatcher _eventDispatcher = eventDispatcher;

    public async Task<Result<TempoDto?>> BuscarPorCidade(string cidade)
    {
        if (string.IsNullOrEmpty(cidade))
        {
            return TempoErrors.CidadeInvalida;
        }

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
        Result<TempoDto?> tempoDto = await BuscarPorCidade(input.Cidade);

        if (!tempoDto.HasValue)
        {
            return new(tempoDto);
        }

        Tempo tempo = tempoDto.Value!.ParaDomain();

        Result result = tempo.DiminuirTemperatura(input.CelsiusDiminuidos);

        if (result.IsFailure)
        {
            _logger.LogError("Ocorreu um erro ao tentar diminuir a temperatura - Result: {@Result}", result);

            return new(result);
        }

        _eventDispatcher.Dispatch(tempo.PopEvents());

        return Result.Empty;
    }
}