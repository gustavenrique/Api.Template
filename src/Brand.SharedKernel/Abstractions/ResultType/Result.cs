using SharedKernel.Abstractions.ResultType;

namespace SharedKernel.ResultType;

public partial record Result
{
    public ResultReason Reason { get; init; }

    /// <summary>
    /// Mensagens de successo ou falha
    /// </summary>
    public IEnumerable<string> Messages { get; protected set; }

    public bool IsSuccess => Reason
        is ResultReason.Ok
        or ResultReason.Created
        or ResultReason.Empty;

    public bool IsFailure => !IsSuccess;

    public Result(ResultReason reason, IEnumerable<string> messages)
    {
        Reason = reason;
        Messages = messages;
    }

    public Result(Result result)
    {
        Reason = result.Reason;
        Messages = result.Messages;
    }

    #region Success
    public static Result Ok(IEnumerable<string>? messages = null) =>
        new(ResultReason.Ok, messages ?? []);

    public static Result Created(IEnumerable<string>? messages = null) =>
        new(ResultReason.Created, messages ?? []);

    public readonly static Result Empty = new(ResultReason.Empty, []);
    #endregion

    #region Failure
    public static Result UnexpectedError(IEnumerable<string> messages) =>
        new(ResultReason.UnexpectedError, messages);

    public static Result BusinessLogicViolation(IEnumerable<string> messages) =>
        new(ResultReason.BusinessLogicViolation, messages);

    public static Result UnexistentId(IEnumerable<string> messages) =>
        new(ResultReason.UnexistentId, messages);

    public static Result InvalidInput(IEnumerable<string> messages) =>
        new(ResultReason.InvalidInput, messages);
    #endregion
}