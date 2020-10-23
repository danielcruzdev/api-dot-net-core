using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Helpers
{
    public class BaseDapper : IDisposable
    {
        protected readonly SettingsApplication _configuration;

        public BaseDapper(SettingsApplication configuration)
        {
            _configuration = configuration;
        }

        public void ExecuteSP(string procedureName, DynamicParameters parameters = null)
        {
            using TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required);
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.ConnectionString))
            {
                sqlConnection.Open();

                if (parameters == null)
                    sqlConnection.Execute(procedureName, commandType: CommandType.StoredProcedure);
                else
                    sqlConnection.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            transactionScope.Complete();
        }

        public List<T> ExecuteSP<T>(string procedureName, DynamicParameters parameters = null)
        {
            List<T> ret;

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (var sqlConnection = new SqlConnection(_configuration.ConnectionString))
                {
                    sqlConnection.Open();

                    if (parameters == null)
                        ret = sqlConnection.Query<T>(procedureName).ToList<T>();
                    else
                        ret = sqlConnection.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure).ToList<T>();
                }

                transactionScope.Complete();
            }

            return ret;
        }

        public async Task ExecuteSPAsync(string procedureName, DynamicParameters parameters = null)
        {
            using var tran = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
            using var connection = new SqlConnection(_configuration.ConnectionString);
            await connection.OpenAsync();

            if (parameters == null)
                _ = await connection.ExecuteAsync(procedureName, commandType: CommandType.StoredProcedure);
            else
                _ = await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);

            tran.Complete();
        }

        public async Task<List<T>> QueryAsync<T>(string procedureName, DynamicParameters parameters = null)
        {
            List<T> ret;

            using var tran = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                await connection.OpenAsync();

                if (parameters == null)
                {
                    var queryResult = await connection.QueryAsync<T>(procedureName);
                    ret = queryResult.ToList();
                }
                else
                {
                    var queryResult = await connection.QueryAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    ret = queryResult.ToList();
                }

                tran.Complete();
            }

            return ret;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public DataTable ExecuteSPtoDT(string procedureName, DynamicParameters parameters = null)
        {
            DataTable table = new DataTable();

            using (var sqlConnection = new SqlConnection(_configuration.ConnectionString))
            {
                sqlConnection.Open();
                table.Load(sqlConnection.ExecuteReader(procedureName, parameters, commandType: CommandType.StoredProcedure));
            }

            return table;
        }
    }
}
