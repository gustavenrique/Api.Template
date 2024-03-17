using FluentAssertions;

using SharedKernel.Abstractions.ResultType;
using SharedKernel.ResultType;

namespace SharedKernel.UnitTests.Abstractions.ResultType;
public sealed class ResultTTests
{
    [Theory(DisplayName = "Ok - Contém ResultReason correto e armazena parâmetros de modo íntegro")]
    [MemberData(nameof(ParametrosSuccess))]
    internal void SuccessMethod_QuandoChamado_ArmazenaParametrosCorretamente(object value, IEnumerable<string>? mensagens)
    {
        // Act
        Result<object> result = Result<object>.Ok(value, mensagens);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);
        result.Reason.Should().Be(ResultReason.Ok);
        result.Value.Should().Be(value);

        result.HasValue.Should().BeTrue();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Theory(DisplayName = "Created - Contém ResultReason correto e armazena parâmetros de modo íntegro")]
    [MemberData(nameof(ParametrosSuccess))]
    internal void CreatedMethod_QuandoChamado_ArmazenaParametrosCorretamente(object value, IEnumerable<string>? mensagens)
    {
        // Act
        Result<object> result = Result<object>.Created(value, mensagens);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);
        result.Reason.Should().Be(ResultReason.Created);
        result.Value.Should().Be(value);

        result.HasValue.Should().BeTrue();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact(DisplayName = "Empty - Retorna ResultReason correto")]
    internal void EmptyField_QuandoAcessado_RetornaReasonCorreto()
    {
        // Act
        Result<object> result = Result<object>.Empty;

        // Assert
        result.Messages.Should().BeEmpty();
        result.Value.Should().BeNull();
        result.Reason.Should().Be(ResultReason.Empty);

        result.HasValue.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Theory(DisplayName = "UnexpectedError - Contém ResultReason correto e armazena parâmetros de modo íntegro")]
    [MemberData(nameof(ParametrosFailure))]
    internal void UnexpectedErrorMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string>? mensagens)
    {
        // Act
        Result<object> result = Result<object>.UnexpectedError(mensagens!);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);
        result.Reason.Should().Be(ResultReason.UnexpectedError);
        result.Value.Should().BeNull();

        result.HasValue.Should().BeFalse();
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    [Theory(DisplayName = "InvalidInput - Contém ResultReason correto e armazena parâmetros de modo íntegro")]
    [MemberData(nameof(ParametrosFailure))]
    internal void InvalidInputMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string>? mensagens)
    {
        // Act
        Result<object> result = Result<object>.InvalidInput(mensagens!);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);
        result.Reason.Should().Be(ResultReason.InvalidInput);
        result.Value.Should().BeNull();

        result.HasValue.Should().BeFalse();
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    [Theory(DisplayName = "UnexistentId - Contém ResultReason correto e armazena parâmetros de modo íntegro")]
    [MemberData(nameof(ParametrosFailure))]
    internal void UnexistentIdMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string>? mensagens)
    {
        // Act
        Result<object> result = Result<object>.UnexistentId(mensagens!);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);
        result.Reason.Should().Be(ResultReason.UnexistentId);
        result.Value.Should().BeNull();

        result.HasValue.Should().BeFalse();
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    [Theory(DisplayName = "BusinessLogicViolation - Contém ResultReason correto e armazena parâmetros de modo íntegro")]
    [MemberData(nameof(ParametrosFailure))]
    internal void BusinessLogicViolationMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string>? mensagens)
    {
        // Act
        Result<object> result = Result<object>.BusinessLogicViolation(mensagens!);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);
        result.Reason.Should().Be(ResultReason.BusinessLogicViolation);
        result.Value.Should().BeNull();

        result.HasValue.Should().BeFalse();
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    #region Utils
    public static IEnumerable<object[]> ParametrosSuccess()
    {
        yield return new object[]
        {
            "dado", new string[] { "mensagem" }
        };

        yield return new object[]
        {
            1, new string[] { "mensagem", "teste" },
        };

        yield return new object[]
        {
            new int[] { 1, 2, 3, 4 },
            new string[] { "teste", "exemplo", "mensagem" },
        };
    }

    public static IEnumerable<object[]> ParametrosFailure()
    {
        yield return new object[] { new string[] { "mensagem" } };
        yield return new object[] { new string[] { "mensagem", "teste" } };
        yield return new object[] { new string[] { "teste", "exemplo", "mensagem" } };
    }
    #endregion
}