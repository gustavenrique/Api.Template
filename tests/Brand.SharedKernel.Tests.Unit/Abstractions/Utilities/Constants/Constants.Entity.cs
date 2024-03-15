using static SharedKernel.UnitTests.Abstractions.Utilities.Fakes.ValueObject;

namespace SharedKernel.UnitTests.Abstractions.Utilities;

internal static partial class Constants
{
    internal static class Entity
    {
        public const string Nome = "Brand";
        public const string Descricao = "Exemplo de descrição";
        public static Email Email() => "team@brand.com"!;

        internal static class Id
        {
            public const string Guid = "6521a18e-e3e8-418a-9fae-caba9b64b7e8";
            public const string String = "id-string";
            public const int Int = 12413;
        }
    }
}