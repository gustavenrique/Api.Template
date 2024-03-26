namespace Brand.SharedKernel.Types.Output;

public enum ResultReason
{
    #region Success
    /// <summary>
    /// SUCCESS - Representa um 'happy path' genérico
    /// </summary>
    Ok,

    /// <summary>
    /// SUCCESS - Dados persistidos
    /// </summary>
    Created,

    /// <summary>
    /// SUCCESS - Resultado vazio de uma 'query'
    /// </summary>
    Empty,
    #endregion

    #region Failure
    /// <summary>
    /// FAILURE - Input incorreto e/ou incompleto
    /// </summary>
    InvalidInput,

    /// <summary>
    /// FAILURE - Identificador inexistente
    /// </summary>
    UnexistentId,

    /// <summary>
    /// FAILURE - Algo não está compatível com todas as regras de negócio
    /// </summary>
    BusinessLogicViolation,

    /// <summary>
    /// FAILURE - Erro interno inesperado
    /// </summary>
    UnexpectedError,
    #endregion
}