using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace SmartShelf.Common
{
    public class SqlDatabaseProxy
    {
        private readonly SqlConnectionFactory _dbConnectionFactory;

        public SqlDatabaseProxy()
        {
            _dbConnectionFactory = new SqlConnectionFactory();
        }

        public T QueryOne<T>(string sql)
        {
            using (var connection = CreateConnection())
            {
                var query = connection.QueryFirstOrDefault<T>(sql);
                return query;
            }
        }

        public T QueryOne<T>(string sql, object param)
        {
            using (var connection = CreateConnection())
            {
                var query = connection.QueryFirstOrDefault<T>(sql, param);
                return query;
            }
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            using (var connection = CreateConnection())
            {
                var query = connection.Query<T>(sql);
                return query.ToList();
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param)
        {
            using (var connection = CreateConnection())
            {
                return connection.Query<T>(sql, param);
            }
        }

        public int Execute<T>(string sql, params T[] items)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, items);
                return result;
            }
        }

        public int Delete<T>(string sql, int id, params T[] items)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, items);
                return result;
            }
        }

        public int Delete<T>(string sql, string modelType, params T[] items)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, items);
                return result;
            }
        }

        public int Delete(string sql, int id)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, new { Id = id });
                return result;
            }
        }

        public int Delete<T>(string sql, T item)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, item);
                return result;
            }
        }

        public int Update<T>(string sql, T item)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, item);
                return result;
            }
        }

        public int Execute(string sql)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql);
                return result;
            }
        }

        public int Insert<T>(string sql, T item)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, item);
                return result;
            }
        }
        public T QueryMulti<T>(string sql, object param, Func<SqlMapper.GridReader, T> fill)
        {
            using (var connection = CreateConnection())
            {
                var muti = connection.QueryMultiple(sql, param);
                var t = fill(muti);
                return t;
            }
        }

        public IList<dynamic> Query(string sql)
        {
            using (var connection = CreateConnection())
            {
                var query = connection.Query(sql);
                return query.ToList();
            }
        }

        public int InsertBatch<T>(string sql, List<T> items)
        {
            using (var connection = CreateConnection())
            {
                var result = connection.Execute(sql, items);
                return result;
            }
        }

        public IDbConnection CreateConnection()
        {
            try
            {
                var connection = _dbConnectionFactory.CreateConnection();
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                throw new Exception("Failed to create connection");
            }
        }
    }
}