using System.Data;
using Dapper;
using RestApi.Common.Configuration;
using Microsoft.Data.SqlClient;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using static Dapper.SqlMapper;

namespace RestApi.Common.Abstractions.Infra;

public abstract class DapperRepository(SqlConfig config)
{
    readonly SqlConfig _config = config;

    readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<SqlException>(SqlServerTransientExceptionDetector.ShouldRetryOn)
        .Or<TimeoutException>()
        .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(
            TimeSpan.FromSeconds(config.Resilience.MedianFirstRetryDelay),
            config.Resilience.RetryCount
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
        using SqlConnection conn = new(_config.ConnectionString);

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
        using SqlConnection conn = new(_config.ConnectionString);

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
        using SqlConnection conn = new(_config.ConnectionString);

        await conn.OpenAsync(cancellationToken ?? CancellationToken.None);

        IEnumerable<T> collection = await _retryPolicy.ExecuteAsync(async () =>
            await conn.QueryAsync<T>(query, param, commandType: commandType)
        );

        return collection.AsList();
    }

    protected async Task<IEnumerable<T2>> Query<T, T1, T2>(
        string query,
        Func<T, T1, T2> map,
        object param,
        CommandType? commandType = null,
        string splitOn = "Id",
        CancellationToken? cancellationToken = null
    )
    {
        using SqlConnection conn = new(_config.ConnectionString);

        await conn.OpenAsync(cancellationToken ?? CancellationToken.None);

        return await conn.QueryAsync(query, map, param: param, commandType: commandType, splitOn: splitOn);
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
        using SqlConnection conn = new(_config.ConnectionString);

        await conn.OpenAsync(cancellationToken ?? CancellationToken.None);

        using GridReader retorno = await _retryPolicy.ExecuteAsync(async () =>
            await conn.QueryMultipleAsync(query, param, commandType: commandType)
        );

        return await mappingCallback(retorno);
    }
}