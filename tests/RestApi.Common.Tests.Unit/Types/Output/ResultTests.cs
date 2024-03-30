using FluentAssertions;
using RestApi.Common.Types.Output;

namespace RestApi.Common.Tests.Unit.Types.Output;

public sealed class ResultTests
{
    [Theory(DisplayName = "Ok - Cont�m ResultReason correto e armazena Messages de modo �ntegro")]
    [MemberData(nameof(ParametrosDoFactoryMethod))]
    internal void SuccessMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string> mensagens)
    {
        // Act
        Result result = Result.Ok(mensagens);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);

        result.Reason.Should().Be(ResultReason.Ok);

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Theory(DisplayName = "Created - Cont�m ResultReason correto e armazena Messages de modo �ntegro")]
    [MemberData(nameof(ParametrosDoFactoryMethod))]
    internal void CreatedMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string> mensagens)
    {
        // Act
        Result result = Result.Created(mensagens);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);

        result.Reason.Should().Be(ResultReason.Created);

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact(DisplayName = "Empty - Cont�m ResultReason correto")]
    internal void EmptyMethod_QuandoChamado_ArmazenaReasonCorreto()
    {
        // Act
        Result result = Result.Empty;

        // Assert
        result.Messages.Should().BeEmpty();

        result.Reason.Should().Be(ResultReason.Empty);

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Theory(DisplayName = "UnexpectedError - Cont�m ResultReason correto e armazena Messages de modo �ntegro")]
    [MemberData(nameof(ParametrosDoFactoryMethod))]
    internal void UnexpectedErrorMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string> mensagens)
    {
        // Act
        Result result = Result.UnexpectedError(mensagens!);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);

        result.Reason.Should().Be(ResultReason.UnexpectedError);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    [Theory(DisplayName = "BusinessLogicViolation - Cont�m ResultReason correto e armazena Messages de modo �ntegro")]
    [MemberData(nameof(ParametrosDoFactoryMethod))]
    internal void BusinessLogicViolationMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string> mensagens)
    {
        // Act
        Result result = Result.BusinessLogicViolation(mensagens!);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);

        result.Reason.Should().Be(ResultReason.BusinessLogicViolation);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    [Theory(DisplayName = "UnexistentId - Cont�m ResultReason correto e armazena Messages de modo �ntegro")]
    [MemberData(nameof(ParametrosDoFactoryMethod))]
    internal void UnexistentIdMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string> mensagens)
    {
        // Act
        Result result = Result.UnexistentId(mensagens!);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);

        result.Reason.Should().Be(ResultReason.UnexistentId);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    [Theory(DisplayName = "InvalidInput - Cont�m ResultReason correto e armazena Messages de modo �ntegro")]
    [MemberData(nameof(ParametrosDoFactoryMethod))]
    internal void InvalidInputMethod_QuandoChamado_ArmazenaParametrosCorretamente(IEnumerable<string> mensagens)
    {
        // Act
        Result result = Result.InvalidInput(mensagens!);

        // Assert
        result.Messages.Should().BeEquivalentTo(mensagens);

        result.Reason.Should().Be(ResultReason.InvalidInput);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    #region Utils
    public static IEnumerable<object[]> ParametrosDoFactoryMethod()
    {
        yield return new object[] { new string[] { "mensagem" } };
        yield return new object[] { new string[] { "teste", "mensagem" } };
        yield return new object[] { new string[] { "exemplo", "mensagem", "teste" } };
    }
    #endregion
}