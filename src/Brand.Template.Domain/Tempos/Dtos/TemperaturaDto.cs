namespace Brand.Template.Domain.Tempos.Dtos;

public readonly record struct TemperaturaDto(
    decimal Celsius,
    decimal Fahrenheit,
    decimal Kelvin
);