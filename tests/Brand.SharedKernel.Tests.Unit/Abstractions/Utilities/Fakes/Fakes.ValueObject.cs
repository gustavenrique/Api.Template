﻿using System.Text.RegularExpressions;

using SharedKernel.Abstractions;
using SharedKernel.ResultType;

using VO = SharedKernel.Abstractions.ValueObject;

namespace SharedKernel.UnitTests.Abstractions.Utilities;
internal static partial class Fakes
{
    /// <summary>
    /// Implementãções criadas apenas para fins de testes
    /// </summary>
    internal static class ValueObject
    {
        internal sealed class Produto(string nome, double preco, decimal desconto, DateTime data, uint[] categorias, Tag[] tags) : VO
        {
            string Nome => nome;
            double Preco => preco;
            decimal Desconto => desconto;
            DateTime Data => data;
            uint[] Categorias => categorias;
            Tag[] Tags => tags;

            public override IEnumerable<object> GetEqualityComponents()
            {
                yield return Nome;
                yield return Preco;
                yield return Desconto;
                yield return Data.GetHashCode();

                foreach (uint c in Categorias) yield return c;

                foreach (Tag tag in Tags) yield return tag;
            }
        }

        internal sealed record Tag(string Nome) : IValueObject;

        internal sealed record Email : IValueObject
        {
            private const string _pattern = @"^.+@.+\..+$";
            private const byte _maxLength = 100;
            private const byte _minLength = 4;

            public string Value { get; init; }

            public static implicit operator Email?(string value) =>
                Criar(value)?.Value;

            private Email(string value) => Value = value;

            public static Result<Email?> Criar(string value)
            {
                string erro = value switch
                {
                    var v when string.IsNullOrEmpty(v) => "Email não preenchido",
                    var v when v.Length < _minLength => $"Email deve ter ao menos {_minLength} caracteres",
                    var v when v.Length > _maxLength => $"Email não pode ultrapassar {_maxLength} caracteres",
                    var v when !Regex.IsMatch(v, _pattern) => "Email deve seguir o padrão *@*.*",
                    _ => string.Empty
                };

                if (!string.IsNullOrEmpty(erro))
                    return Result<Email?>.InvalidInput([erro]);

                return Result<Email?>.Created(new Email(value));
            }
        }
    }
}