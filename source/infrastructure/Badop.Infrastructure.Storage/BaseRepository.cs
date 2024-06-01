using Dapper;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace Badop.Infrastructure.Storage;

public abstract class BaseRepository
    {
        protected abstract string ConnectionString { get; }
        protected readonly bool LogQueryTimes;
        protected Action<string, string>? LogHandler;

        protected BaseRepository(Action<string, string>? logHandler, bool logQueryTimes)
        {
            this.LogQueryTimes = logQueryTimes;
            this.LogHandler = logHandler;
        }

        protected int GetRowsAffected(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            return GetRowsAffectedAsync(commandText, parameters, opts).Result;
        }

        protected async Task<int> GetRowsAffectedAsync(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            return await GetResultAsync<int>(commandText, parameters, opts);
        }

        [Obsolete("Use Execute(commandText,parameters,opts) method instead")]
        protected bool ExecuteNonQuery(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            return !(GetResults<int>(commandText, parameters, opts) ?? new int[] { }).Any();
        }

        /// <summary>
        /// Execute SQL text / stored procedures without expecting any results.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="opts"></param>
        protected void Execute(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            opts ??= new QueryOptions();

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;
            var timer = new Stopwatch();

            try
            {
                PreExecute(out connection, out transaction, timer, opts);

                if (connection != null)
                {
                    connection.Execute(commandText, parameters, transaction, opts.ConnectionTimeout, opts.CommandType);

                    PostExecuteSuccess(commandText, connection, transaction, timer, opts);
                }
            }
            catch (Exception ex)
            {
                PostExecuteFailure(commandText, connection, transaction, opts, ex);
            }
            finally
            {
                CleanConnection(connection);
            }
        }

        protected int ExecuteReturnInt(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            var result = 0;
            opts ??= new QueryOptions();

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;
            var timer = new Stopwatch();

            try
            {
                PreExecute(out connection, out transaction, timer, opts);

                if (connection != null)
                {
                    result = connection.ExecuteScalar<int>(commandText, parameters, transaction, opts.ConnectionTimeout,
                        opts.CommandType);

                    PostExecuteSuccess(commandText, connection, transaction, timer, opts);
                }
            }
            catch (Exception ex)
            {
                PostExecuteFailure(commandText, connection, transaction, opts, ex);
            }
            finally
            {
                CleanConnection(connection);
            }
            return result;
        }

        protected async Task<bool> ExecuteNonQueryAsync(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            return !(await GetResultsAsync<int>(commandText, parameters, opts)).Any();
        }

        protected bool GetBooleanResult(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            int result = ExecuteReturnInt(commandText, parameters, opts);
            if (result == 1)
                return true;
            return false;
        }

        protected T? GetResult<T>(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            IEnumerable<T?>? result = Query<T>(commandText, parameters, opts);
            return opts is { SingleResultCanBeNull: true } ? result.SingleOrDefault() : result.Single();
        }

        protected async Task<T?> GetResultAsync<T>(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            IEnumerable<T?> result = await QueryAsync<T>(commandText, parameters, opts) ?? Array.Empty<T>();
            return opts is { SingleResultCanBeNull: true } ? result.SingleOrDefault() : result.Single();
        }

        protected IEnumerable<T>? GetResults<T>(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            return Query<T>(commandText, parameters, opts);
        }

        protected async Task<IEnumerable<T>> GetResultsAsync<T>(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            return await QueryAsync<T>(commandText, parameters, opts);
        }

       private async Task<IEnumerable<T>> QueryAsync<T>(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            if (opts == null)
                opts = new QueryOptions();

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;
            Stopwatch timer = new Stopwatch();
            IEnumerable<T> result = new List<T>();

            try
            {
                PreExecute(out connection, out transaction, timer, opts);

                if (connection != null)
                {
                    result = await connection.QueryAsync<T>(commandText, parameters, transaction,
                        opts.ConnectionTimeout, opts.CommandType);

                    PostExecuteSuccess(commandText, connection, transaction, timer, opts);
                }
            }
            catch (Exception ex)
            {
                PostExecuteFailure(commandText, connection, transaction, opts, ex);
            }
            finally
            {
                CleanConnection(connection);
            }

            return result;
        }

        //TODO: refactor tuple call in the future
        /*private async Task<Tuple<IEnumerable<T>,Y>> QueryAsync<T,Y>(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            if (opts == null)
                opts = new QueryOptions();

            SqlConnection connection = null;
            SqlTransaction transaction = null;
            Stopwatch timer = new Stopwatch();
            Tuple<IEnumerable<T>, Y> result = null;
            try
            {
                PreExecute(out connection, out transaction, timer, opts);

                result = await connection.QueryTupleAsync<T,Y>(commandText, parameters, transaction, opts.ConnectionTimeout, opts.CommandType);

                PostExecuteSuccess(commandText, connection, transaction, timer, opts);
            }
            catch (Exception ex)
            {
                PostExecuteFailure(commandText, connection, transaction, opts, ex);
            }
            finally
            {
                CleanConnection(connection, opts);
            }
            
            return new Tuple<IEnumerable<T>, Y>(result.Item1.ToList(),result.Item2);
        }*/

        private IEnumerable<T>? Query<T>(string commandText, object? parameters = null,  QueryOptions? opts = null)
        {
            if (opts == null)
                opts = new QueryOptions();

            SqlConnection? connection = null;
            SqlTransaction? transaction = null;
            Stopwatch timer = new Stopwatch();
            IEnumerable<T>? result = null;

            try
            {
                PreExecute(out connection, out transaction, timer, opts);

                if (connection != null)
                {
                    result = connection.Query<T>(commandText, parameters, transaction, true, opts.ConnectionTimeout,
                        opts.CommandType);

                    PostExecuteSuccess(commandText, connection, transaction, timer, opts);
                }
            }
            catch (Exception ex)
            {
                PostExecuteFailure(commandText, connection, transaction, opts, ex);
            }
            finally
            {
                CleanConnection(connection);
            }

            return result;
        }

        private void PreExecute(out SqlConnection? connection, out SqlTransaction? transaction, Stopwatch timer, QueryOptions? opts)
        {

            connection = new SqlConnection(ConnectionString);
            connection.Open();

            timer?.Start();

            if (opts != null)
            {
                transaction = opts.IsTransactional ? connection.BeginTransaction(opts.IsolationLevel) : null;
            }
            else
            {
                transaction = null;
            }
        }

        private void PostExecuteSuccess(string commandText, SqlConnection? connection, SqlTransaction? transaction, Stopwatch timer, QueryOptions? opts)
        {
            if (opts != null)
            {
                if (opts.IsTransactional)
                {
                    transaction?.Commit();
                }

                timer?.Stop();

                if (LogQueryTimes)
                {
                    LogHandler?.Invoke(this.GetType().Name, $"Time taken to run command took {timer?.ElapsedMilliseconds}ms : {commandText} ");
                }
            }
        }

        private void PostExecuteFailure(string commandText, SqlConnection? connection, SqlTransaction? transaction, QueryOptions? opts, Exception ex)
        {

            if (opts != null)
            {
                if (opts.IsTransactional)
                {
                    transaction?.Rollback();
                }

                if (ex.GetType() == typeof(TimeoutException))
                {
                    throw new TimeoutException($"{GetType().FullName} experienced a SQL timeout when running '{commandText}'\n{ex.Message}", ex);
                }
            }
            throw new Exception($"{GetType().FullName} experienced an exception when running '{commandText}'\n{ex.Message}", ex);
        }

        private void CleanConnection(SqlConnection? connection)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }

        protected void AddCommandParametersFromObject(SqlCommand command, object? parameters)
        {
            if (parameters == null)
                return;

            var result = new List<SqlParameter>();
            Type myType = parameters.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object? propValue = prop.GetValue(parameters, null);

                command.Parameters.AddWithValue("@" + prop.Name, propValue);
            }
        }

        protected static QueryOptions CanBeNullOption()
        {
            return new QueryOptions { SingleResultCanBeNull = true };
        }
    }