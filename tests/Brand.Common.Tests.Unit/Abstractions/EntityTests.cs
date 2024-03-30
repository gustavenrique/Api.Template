using FluentAssertions;
using Brand.Common.Abstractions.Domain;
using Brand.Common.Abstractions.Domain.Events;
using Brand.Common.Tests.Unit.Abstractions.Utilities;
using static Brand.Common.Tests.Unit.Abstractions.Utilities.Fakes.Entity;
using static Brand.Common.Tests.Unit.Abstractions.Utilities.Fakes.ValueObject;

namespace Brand.Common.Tests.Unit.Abstractions;

public sealed class EntityTests
{
    [Theory(DisplayName = "Entity - Comparação: Se ID for igual, retorna true")]
    [ClassData(typeof(Utils.Entity.Iguais))]
    internal void Entity_SeEntidadesPossuemMesmoId_ComparacaoRetornaTrue(dynamic entity, dynamic entity1)
    {
        Assert.True(entity == entity1);
        Assert.True(entity.GetHashCode() == entity1.GetHashCode());
        Assert.True(entity.Equals(entity1));
        Assert.True(Equals(entity, entity1));
    }

    [Theory(DisplayName = "Entity - Comparação: Se ID for diferente, retorna false")]
    [ClassData(typeof(Utils.Entity.Diferentes))]
    internal void Entity_SeEntidadesPossuemIdDiferente_ComparacaoRetornaFalse(dynamic entity, dynamic entity1)
    {
        Assert.False(entity == entity1);
        Assert.False(entity.GetHashCode() == entity1.GetHashCode());
        Assert.False(entity.Equals(entity1));
        Assert.False(Equals(entity, entity1));
    }

    [Fact(DisplayName = "Entity - Registra domain events corretamente")]
    internal void Entity_RegistraDomainEventsCorretamente()
    {
        // Arrange
        Usuario usuario = Usuario.Criar(
            Constants.Entity.Nome,
            Constants.Entity.Email()
        ).Value!;

        Email novoEmail = "novo.email@clear.sale"!;
        Email outroNovoEmail = "outro.novo.email@clear.sale"!;

        // Act
        usuario.AlterarEmail(novoEmail);
        usuario.AlterarEmail(outroNovoEmail);

        // Assert
        IReadOnlyCollection<IDomainEvent> events = usuario.PopEvents();

        events.Should().HaveCount(2);
        usuario.PopEvents().Should().BeEmpty();

        (events.ElementAt(0) as EmailAlteradoEvent)!
            .EmailNovo
            .Should()
            .Be(novoEmail);

        (events.ElementAt(1) as EmailAlteradoEvent)!
            .EmailNovo
            .Should()
            .Be(outroNovoEmail);
    }

    [Fact(DisplayName = "AggregateRoot - Deve herdar de Entity")]
    internal void AggregateRoot_DeveHerdarDeEntity()
    {
        // Arrange
        var aggregateRoot = Lider.Criar(Constants.Entity.Email()).Value!;

        // Assert
        (aggregateRoot is Entity<int>).Should().BeTrue();
    }
}