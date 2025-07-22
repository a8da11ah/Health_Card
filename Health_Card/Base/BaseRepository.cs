using System.Data;
using Dapper;

namespace Health_Card.Base
{
    public abstract class BaseRepository
    {
        protected readonly IDbConnection _connection;

        protected BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // Existing methods...
        protected async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null, CommandType? commandType = null)
        {
            return await _connection.QuerySingleOrDefaultAsync<T>(sql, parameters, commandType: commandType);
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<T>(sql, parameters, commandType: commandType);
        }

        protected async Task<int> ExecuteAsync(string sql, object parameters = null, CommandType? commandType = null)
        {
            return await _connection.ExecuteAsync(sql, parameters, commandType: commandType);
        }

        protected async Task<int> ExecuteScalarAsync(string sql, object parameters = null, CommandType? commandType = null)
        {
            return await _connection.ExecuteScalarAsync<int>(sql, parameters, commandType: commandType);
        }

        // Optional: Dedicated method for stored procedures
        protected async Task<T> ExecuteStoredProcedureSingleAsync<T>(string storedProcedureName, object parameters = null)
        {
            return await _connection.QuerySingleOrDefaultAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        }

        protected async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, object parameters = null)
        {
            return await _connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}