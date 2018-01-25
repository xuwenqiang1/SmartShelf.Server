using SmartShelf.Common;
using SmartShelf.Model;

namespace SmartShelf.DataAccess
{
    public class UserDataAccess: DataAccessBase<User>
    {
        public static SqlStrings OperationSql = new SqlStrings
        {
            TableName = "User",
            Add = @"INSERT INTO User (Id,Name) " +
                  "VALUES (@Id, @Name)",
            Update = @"UPDATE User SET " +
                     "Name = @Name" +
                     "WHERE Id = @Id",
            Delete = @"delete from User WHERE Id = @Id",
            QueryAll = @"SELECT * FROM User",
            QueryOne = @"SELECT * FROM User WHERE Id = @Id"
        };

    }
}
