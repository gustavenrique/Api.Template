using System.Text.Json.Serialization;

namespace Brand.Template.Api.Filter;

internal sealed record Response<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; init; }

    [JsonPropertyName("messages")]
#pragma warning disable CA1819
    public string[] Messages { get; init; } = [];
#pragma warning restore CA1819

    public Response(T data, string[] messages)
    {
        Data = data;
        Messages = messages;
    }
}