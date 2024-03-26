using Brand.Template.Domain.Tempos.Models.ValueObjects;
using Brand.SharedKernel.Abstractions;

namespace Brand.Template.Domain.Tempos.Models;

public sealed class Cidade(CidadeId id, string nome) : Entity<CidadeId>(id)
{
    public string Nome { get; private set; } = nome;
}