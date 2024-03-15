using Brand.Template.Domain.Tempos.Models.ValueObjects;
using SharedKernel.Abstractions;

namespace Brand.Template.Domain.Tempos.Models;

public sealed class Cidade(CidadeId Id, string Nome) : Entity<CidadeId>(Id)
{
    public string Nome { get; private set; } = Nome;
}