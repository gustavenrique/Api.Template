using SharedKernel.Abstractions.ResultType;

namespace SharedKernel.ResultType;

public partial record Result<TValue> : Result
{
    /// <summary>
    /// Valor do resultado da(s) operação(ões)
    /// </summary>
    public TValue? Value { get; init; }

    public bool HasValue => IsSuccess && Value is not null;

    private Result(TValue? value, ResultReason reason, IEnumerable<string> messages)
        : base(reason, messages) => Value = value;

    /// <summary>
    /// Constructor útil para mapeamento de um Result para outro
    /// </summary>
    public Result(Result other) : base(other.Reason, other.Messages) { }

    #region Success
    public static Result<TValue> Ok(TValue value, IEnumerable<string>? messages = null) =>
        new(value, ResultReason.Ok, messages ?? []);

    public static Result<TValue> Created(TValue value, IEnumerable<string>? messages = null) =>
        new(value, ResultReason.Created, messages ?? []);

    public new static Result<TValue> Empty() => new(default, ResultReason.Empty, []);
    #endregion

    #region Failure
    public static Result<TValue> InvalidInput(IEnumerable<string> messages, TValue? value = default) =>
        new(value, ResultReason.InvalidInput, messages);

    public static Result<TValue> UnexistentId(IEnumerable<string> messages, TValue? value = default) =>
        new(value, ResultReason.UnexistentId, messages);

    public static Result<TValue> BusinessLogicViolation(IEnumerable<string> messages, TValue? value = default) =>
        new(value, ResultReason.BusinessLogicViolation, messages);

    public static Result<TValue> UnexpectedError(IEnumerable<string> messages, TValue? value = default) =>
        new(value, ResultReason.UnexpectedError, messages);
    #endregion
}