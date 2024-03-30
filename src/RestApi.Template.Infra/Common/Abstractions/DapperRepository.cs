using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using static Dapper.SqlMapper;

using SqlOptions = RestApi.Template.Infra.Options.Sql;

namespace RestApi.Template.Infra.Common.Abstractions;

internal abstract class DapperRepository(SqlOptions options)
{
    readonly SqlOptions _options = options;

    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<SqlException>(SqlServerTransientExceptionDetector.ShouldRetryOn)
        .Or<TimeoutException>()
        .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(
            TimeSpan.FromSeconds(options.Resilience.MedianFirstRetryDelay),
            options.Resilience.RetryCount
        ));

    /// <summary>
    /// Para comandos DML (create, update, delete)
    /// </summary>
    protected async Task<int> Execute(
        string query,
        object? param = null,
        CommandType? commandType = null,
        CancellationToken? cancellationToken = null
    )
    {
        using SqlConnection conn = new(_options.ConnectionString);

        await conn.OpenAsync(cancellationToken ?? CancellationToken.None);

        return await _retryPolicy.ExecuteAsync(async () =>
            await conn.ExecuteAsync(query, param, commandType: commandType)
        );
    }

    /// <summary>
    /// Tenta buscar o registro
    /// </summary>
    protected async Task<T?> QueryFirstOrDefault<T>(
        string query,
        object? param = null,
        CommandType? commandType = null,
        CancellationToken? cancellationToken = null
    )
    {
        using SqlConnection conn = new(_options.ConnectionString);

        await conn.OpenAsync(cancellationToken ?? CancellationToken.None);

        return await _retryPolicy.ExecuteAsync(async () =>
            await conn.QueryFirstOrDefaultAsync<T>(query, param, commandType: commandType)
        );
    }

    /// <summary>
    /// Busca uma lista de registros
    /// </summary>
    protected async Task<List<T>> Query<T>(
        string query,
        object? param = null,
        CommandType? commandType = null,
        CancellationToken? cancellationToken = null
    )
    {
        using SqlConnection conn = new(_options.ConnectionString);

        await conn.OpenAsync(cancellationToken ?? CancellationToken.None);

        IEnumerable<T> collection = await _retryPolicy.ExecuteAsync(async () =>
            await conn.QueryAsync<T>(query, param, commandType: commandType)
        );

        return collection.AsList();
    }

    /// <summary>
    /// Para SELECTs concomitantes
    /// </summary>
    protected async Task<T> MultipleQuery<T>(
        string query,
        Func<GridReader, Task<T>> mappingCallback,
        object? param = null,
        CommandType? commandType = null,
        CancellationToken? cancellationToken = null
    )
    {
        using SqlConnection conn = new(_options.ConnectionString);

        await conn.OpenAsync(cancellationToken ?? CancellationToken.None);

        using GridReader retorno = await _retryPolicy.ExecuteAsync(async () =>
            await conn.QueryMultipleAsync(query, param, commandType: commandType)
        );

        return await mappingCallback(retorno);
    }
}