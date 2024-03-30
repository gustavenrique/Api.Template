using System.Collections;
using static Brand.Common.Tests.Unit.Abstractions.Utilities.Fakes.Entity;

namespace Brand.Common.Tests.Unit.Abstractions.Utilities;

internal static partial class Utils
{
    internal static class Entity
    {
        private static Usuario GerarUsuario(string? guid = null) => Usuario.Criar(
            new Guid(guid ?? Constants.Entity.Id.Guid),
            Constants.Entity.Nome,
            Constants.Entity.Email()
        ).Value!;

        private static Lider GerarLider(int somaId = 0) => Lider.Criar(
            123232 + somaId,
            Constants.Entity.Email()
        ).Value!;

        private static Exemplo GerarExemplo(string complementoId = "") => new Exemplo(
            Constants.Entity.Id.String + complementoId,
            Constants.Entity.Descricao
        );

        /// <summary>
        /// Lista de parãmetros com entities iguais
        /// </summary>
        public sealed class Iguais : IEnumerable<object[]>
        {
            static readonly List<object[]> s_data = [
                [GerarUsuario(), GerarUsuario()],
                [GerarLider(), GerarLider()],
                [GerarExemplo(), GerarExemplo()],
            ];

            public IEnumerator<object[]> GetEnumerator() => s_data.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// Lista de parãmetros com entities diferentes
        /// </summary>
        public sealed class Diferentes : IEnumerable<object[]>
        {
            static readonly List<object[]> s_data = [
                [GerarUsuario(), GerarUsuario("6521a18e-e3e8-418a-9fae-caba9b2227e8")],
                [GerarLider(), GerarLider(1232)],
                [GerarExemplo(), GerarExemplo("id")],
            ];

            public IEnumerator<object[]> GetEnumerator() => s_data.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}