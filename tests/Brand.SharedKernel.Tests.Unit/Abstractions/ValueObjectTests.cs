using FluentAssertions;

using Brand.SharedKernel.UnitTests.Abstractions.Utilities;

namespace Brand.SharedKernel.UnitTests.Abstractions;

public sealed class ValueObjectTests
{
    [Theory(DisplayName = "Comparação - Se propriedades iguais, comparação deve retornar true")]
    [ClassData(typeof(Utils.ValueObject.Iguais))]
    internal void Comparacao_SePropsIguais_RetornaTrue(Fakes.ValueObject.Produto one, Fakes.ValueObject.Produto other)
    {
        one.Equals(other).Should().BeTrue();

        (one == other).Should().BeTrue();

        Equals(one, other).Should().BeTrue();

        one.GetHashCode().Should().Be(other.GetHashCode());
    }

    [Theory(DisplayName = "Comparação - Se propriedades diferentes, comparação deve retornar false")]
    [ClassData(typeof(Utils.ValueObject.Diferentes))]
    internal void Comparacao_SePropsDiferentes_RetornaFalse(Fakes.ValueObject.Produto one, Fakes.ValueObject.Produto other)
    {
        one.Equals(other).Should().BeFalse();

        (one == other).Should().BeFalse();

        Equals(one, other).Should().BeFalse();

        one.GetHashCode().Should().NotBe(other.GetHashCode());
    }

    [Fact(DisplayName = "GetHashCode - Hash code gerado deve ser único")]
    internal void GetHashCode_DeveGerarHashUnico()
    {
        int[] hashes =
        [
            Utils.ValueObject.Criar(DateTime.Now).GetHashCode(),
            Utils.ValueObject.Criar().GetHashCode(),
            Utils.ValueObject.Criar(tag: "Tag 1").GetHashCode(),
            Utils.ValueObject.Criar(tag: "Tag  ").GetHashCode(),
            Utils.ValueObject.Criar(categorias: [1, 2,]).GetHashCode(),
            Utils.ValueObject.Criar(categorias: [1, 3,]).GetHashCode(),
            Utils.ValueObject.Criar(DateTime.Now).GetHashCode(), // deve ter hash diferente, pois será gerado milisegundos depois do primeiro
        ];

        var hashesDistinct = hashes.Distinct();

        hashes.Should().BeEquivalentTo(hashesDistinct);
    }
}