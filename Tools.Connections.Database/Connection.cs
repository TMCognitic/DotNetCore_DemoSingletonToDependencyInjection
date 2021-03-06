using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Tools.Connections.Database
{
    public class Connection : IConnection
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _factory;

        public Connection(DbProviderFactory factory, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("'connectionString' isn't valid!!");

            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            _connectionString = connectionString;
            _factory = factory;

            //Test de connexion
            using (DbConnection DbConnection = CreateConnection())
            {
                try
                {
                    DbConnection.Open();
                }
                catch (DbException)
                {
                    throw new InvalidOperationException("'connectionString' isn't valid or the server is not started!!");
                }
            }
        }

        public int ExecuteNonQuery(Command command)
        {
            using (DbConnection DbConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, DbConnection))
                {
                    DbConnection.Open();
                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(Command command)
        {
            using (DbConnection DbConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, DbConnection))
                {
                    DbConnection.Open();
                    object o = sqlCommand.ExecuteScalar();
                    return (o is DBNull) ? null : o;
                }
            }
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector)
        {
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            using (DbConnection DbConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, DbConnection))
                {
                    DbConnection.Open();
                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            yield return selector(dataReader);
                        }
                    }
                }
            }
        }

        public DataTable GetDataTable(Command command)
        {
            using (DbConnection DbConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, DbConnection))
                {
                    using (DbDataAdapter sqlDataAdapter = _factory.CreateDataAdapter())
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);

                        return dataTable;
                    }
                }
            }
        }

        public DataSet GetDataSet(Command command)
        {
            using (DbConnection DbConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, DbConnection))
                {
                    using (DbDataAdapter sqlDataAdapter = _factory.CreateDataAdapter())
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        DataSet dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        return dataSet;
                    }
                }
            }
        }

        private DbConnection CreateConnection()
        {
            DbConnection DbConnection = _factory.CreateConnection();
            DbConnection.ConnectionString = _connectionString;

            return DbConnection;
        }

        private static DbCommand CreateCommand(Command command, DbConnection DbConnection)
        {
            DbCommand sqlCommand = DbConnection.CreateCommand();
            sqlCommand.CommandText = command.Query;

            if (command.IsStoredProcedure)
                sqlCommand.CommandType = CommandType.StoredProcedure;

            foreach (KeyValuePair<string, object> kvp in command.Parameters)
            {
                DbParameter sqlParameter = sqlCommand.CreateParameter();
                sqlParameter.ParameterName = kvp.Key;
                sqlParameter.Value = kvp.Value;

                sqlCommand.Parameters.Add(sqlParameter);
            }

            return sqlCommand;
        }
    }
}
