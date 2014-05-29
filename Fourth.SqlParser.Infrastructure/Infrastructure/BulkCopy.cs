using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Fourth.SqlParser.Infrastructure.Infrastructure.Enums;

namespace Fourth.SqlParser.Infrastructure.Infrastructure
{
    public class BulkCopy
    {
        public BulkCopy(DataBase database)
        {
            switch (database)
            {
                case DataBase.Default:
                    DbConnection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    break;
            }
        }

        private SqlConnection DbConnection { get; set; }

        public void Upload(DataTable data, string tableName)
        {
            using (DbConnection)
            {
                var bulkCopy = new SqlBulkCopy(DbConnection, SqlBulkCopyOptions.TableLock, null)
                {
                    DestinationTableName = tableName
                };
                DbConnection.Open();

                bulkCopy.WriteToServer(data);
            }
        }
    }
}