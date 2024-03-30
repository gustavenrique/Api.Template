using RestApi.Common.Types.Output;
using MediatR;

namespace RestApi.Common.Abstractions;

// Créditos: Milan Jovanovîc (https://www.youtube.com/@MilanJovanovicTech)

/// <summary>
/// Representa um comando (mudança no state da aplicação)
/// </summary>
public interface ICommand : IRequest<Result>
{
    public string CorrelationId { get; }
}

/// <summary>
/// Representa um comando que retorna algo
/// </summary>
public interface ICommand<TOutput> : IRequest<Result<TOutput>>, ICommand;