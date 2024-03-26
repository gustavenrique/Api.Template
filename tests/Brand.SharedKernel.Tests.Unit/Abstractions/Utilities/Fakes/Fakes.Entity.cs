using Brand.SharedKernel.Abstractions;
using Brand.SharedKernel.Abstractions.Events;
using Brand.SharedKernel.Types.Output;
using static Brand.SharedKernel.UnitTests.Abstractions.Utilities.Fakes.ValueObject;

namespace Brand.SharedKernel.UnitTests.Abstractions.Utilities;
internal static partial class Fakes
{
    /// <summary>
    /// Implementações criadas apenas para fins de testes
    /// </summary>
    internal static class Entity
    {
        internal sealed class Usuario : Entity<Guid>
        {
            public string Username { get; private set; }
            public Email Email { get; private set; }

            private Usuario(Guid id, string username, Email email) : base(id)
            {
                Username = username;
                Email = email;
            }

            public static Result<Usuario?> Criar(string username, Email email) =>
                Criar(Guid.NewGuid(), username, email);

            public static Result<Usuario?> Criar(Guid id, string username, Email email)
            {
                List<string> erros = [];

                if (string.IsNullOrEmpty(username))
                {
                    erros.Add("Nome não preenchido");
                }

                if (string.IsNullOrEmpty(email.Value))
                {
                    erros.Add("Email não preenchido");
                }

                if (erros.Count > 0)
                {
                    return Result<Usuario?>.InvalidInput(erros);
                }

                return Result<Usuario?>.Created(new Usuario(
                    id,
                    username,
                    email
                ));
            }

            public Result AlterarEmail(Email novoEmail)
            {
                Email emailAntigo = Email;
                Email = novoEmail;

                Raise(new EmailAlteradoEvent(Id, emailAntigo, Email));

                return Result.Empty;
            }
        }

        internal sealed record EmailAlteradoEvent(
            Guid UsuarioId,
            Email EmailAntigo,
            Email EmailNovo
        ) : IDomainEvent;

        internal sealed class Lider : AggregateRoot<int>
        {
            readonly List<Usuario> _liderados = [];

            public Email Email { get; private set; }
            public IReadOnlyList<Usuario> Liderados => _liderados.AsReadOnly();

            private Lider(int id, Email email) : base(id) =>
                Email = email;

            public static Result<Lider?> Criar(Email email) => Criar(new Random().Next(), email);

            public static Result<Lider?> Criar(int id, Email email)
            {
                if (string.IsNullOrEmpty(email.Value))
                    return Result<Lider?>.InvalidInput(["Email não preenchido"]);

                return Result<Lider?>.Created(new Lider(id, email));
            }
        }

        internal sealed class Exemplo : Entity<string>
        {
            public string Descricao { get; private set; }

            public Exemplo(string id, string descricao) : base(id) =>
                Descricao = descricao;
        }
    }
}