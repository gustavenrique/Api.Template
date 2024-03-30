using System.Collections;
using Common.UnitTests.Abstractions.Utilities;

namespace Brand.Common.Tests.Unit.Abstractions.Utilities;

internal static partial class Utils
{
    internal static class ValueObject
    {
        internal static Fakes.ValueObject.Produto Criar(
            DateTime? date = null,
            string tag = "Testando",
            uint[]? categorias = null
        ) =>
            new Fakes.ValueObject.Produto(
                nome: Constants.ValueObject.Nome,
                preco: Constants.ValueObject.Preco,
                desconto: Constants.ValueObject.Desconto,
                categorias: categorias ?? Constants.ValueObject.Categorias,
                tags: [
                    new Fakes.ValueObject.Tag(Constants.ValueObject.Nome),
                    new Fakes.ValueObject.Tag(tag),
                ],
                data: date ?? Constants.ValueObject.Data
            );

        /// <summary>
        /// Lista de parãmetros com value objects iguais
        /// </summary>
        public sealed class Iguais : IEnumerable<object[]>
        {
            static readonly List<object[]> s_data = [
                [Criar(), Criar()],
                [Criar(DateTime.MinValue), Criar(DateTime.MinValue)],
                [Criar(tag: "Tag 1"), Criar(tag: "Tag 1"),],
            ];

            public IEnumerator<object[]> GetEnumerator() => s_data.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// Lista de parãmetros com value objects diferentes
        /// </summary>
        public sealed class Diferentes : IEnumerable<object[]>
        {
            static readonly List<object[]> s_data = [
                [Criar(DateTime.MinValue), Criar()],
                [Criar(tag: "Tag 1"), Criar(tag: "Tag 2"),],
                [Criar(categorias: []), Criar(categorias: [9]),]
            ];

            public IEnumerator<object[]> GetEnumerator() => s_data.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}