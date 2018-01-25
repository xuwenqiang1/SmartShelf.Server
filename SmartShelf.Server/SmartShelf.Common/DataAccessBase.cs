using System;
using System.Collections.Generic;

namespace SmartShelf.Common
{
    public class DataAccessBase<T> : EntityBaseRepository<T> where T : class
    {
        protected override SqlStrings Sql { get; set; }

        protected override Func<string, Dictionary<string, object>, SqlBuilder.Template> QueryByPageSql => GenerateQueryByPageSql;

        protected virtual SqlBuilder.Template GenerateQueryByPageSql(string queryByPageTemplate, Dictionary<string, object> querys)
        {
            //You have to override this, otherwise it will throw exception
            throw new NotImplementedException();
        }
    }
}