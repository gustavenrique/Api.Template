using System.Text.Json.Serialization;

namespace RestApi.Template.Api.Filter.ResponseMapping;

internal sealed record Response<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; init; }

    [JsonPropertyName("messages")]
    public string[] Messages { get; init; } = [];

    public Response(T data, string[] messages)
    {
        Data = data;
        Messages = messages;
    }
}