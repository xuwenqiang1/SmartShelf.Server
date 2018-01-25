using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SmartShelf.Common
{
    public abstract class EntityBaseRepository<T> where T : class
    {
        protected SqlDatabaseProxy DatabaseProxy { get; set; }

        protected abstract SqlStrings Sql { get; set; }
        protected readonly DateTime MinDate;

        protected EntityBaseRepository()
        {
            DatabaseProxy = new SqlDatabaseProxy();
            MinDate = DateTime.ParseExact("1753-01-01", "yyyy-MM-dd", CultureInfo.CurrentCulture);
        }

        public int Add(T t)
        {
            var count = DatabaseProxy.Execute(Sql.Add, t);
            return count;
        }

        public int Update(T t)
        {
            var count = DatabaseProxy.Update(Sql.Update, t);
            return count;
        }

        public int Delete(object id)
        {
            var count = DatabaseProxy.Delete(Sql.Delete, new { Id = id });
            return count;
        }

        public IEnumerable<T> GetAll()
        {
            return DatabaseProxy.Query<T>(Sql.QueryAll);
        }

        public IEnumerable<T> QueryByPage(int startNumber, int endNumber, out int totleRecords, Dictionary<string, object> querys)
        {
            var items = new List<T>();
            var number = 0;
            var sqlBuild = QueryByPageSql.Invoke(Sql.QueryByPage, querys);
            var parameters = sqlBuild.Parameters;
            parameters.AddDynamicParams(new { StartNumber = startNumber, EndNumber = endNumber });
            DatabaseProxy.QueryMulti(sqlBuild.RawSql, parameters, reader =>
            {
                items = reader.Read<T>().ToList();
                number = reader.Read<int>().Single();
                return true;
            });
            totleRecords = number;
            return items;
        }

        public T Get(object id)
        {
            return DatabaseProxy.Query<T>(Sql.QueryOne, new { Id = id }).First();
        }

        public int AddBatch(List<T> items)
        {
            var count = DatabaseProxy.InsertBatch(Sql.Add, items);
            return count;
        }

        protected abstract Func<string, Dictionary<string, object>, SqlBuilder.Template> QueryByPageSql { get; }
    }
}