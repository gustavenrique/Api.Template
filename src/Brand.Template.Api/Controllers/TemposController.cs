using Asp.Versioning;
using Brand.SharedKernel.Types.Output;
using Brand.Template.Api.Filter.ResponseMapping;
using Brand.Template.Application.Tempos.Abstractions;
using Brand.Template.Application.Tempos.Dtos;
using Brand.Template.Domain.Tempos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Brand.Template.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/tempos")]
public sealed class TemposController(ITempoService service) : ControllerBase
{
    readonly ITempoService _service = service;

    /// <summary>
    /// Busca o tempo atual da cidade
    /// </summary>
    [HttpGet("{cidade}")]
    [ProducesResponseType(typeof(Response<TempoDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status500InternalServerError)]
    public Task<Result<TempoDto?>> BuscarPorCidade([FromRoute] string cidade) =>
        _service.BuscarPorCidade(cidade);

    /// <summary>
    /// Diminui a temperatura de uma cidade, na quantidade especificada em Celsius (sqn)
    /// </summary>
    [HttpPatch("diminuir-temperatura")]
    [ProducesResponseType(typeof(Response<TempoDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status500InternalServerError)]
    public Task<Result> DiminuirTemperatura([FromBody] DiminuirTemperaturaDto request) =>
        _service.DiminuirTemperatura(request);
}