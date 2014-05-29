using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Fourth.SqlParser.Infrastructure.Infrastructure;
using Fourth.SqlParser.Infrastructure.Infrastructure.Enums;
using Fourth.SqlParser.Infrastructure.Infrastructure.Extensions;


namespace Fourth.SqlParser
{
    public class SqlParserDal
    {
        public void Store(TestResult testResult)
        {
            using (
                var storedProcedure = new StoredProcedure(DataBase.Default, "sp_InsertResults",
                    SetUpResultParameters(testResult)))
            {
                storedProcedure.Execute();
            }
        }

        public List<DatabaseConnection> Get()
        {
            var connections = new List<DatabaseConnection>();
            using (
                var storedProcedure = new StoredProcedure(DataBase.Default, "sp_getConnections"))
            {
                SqlDataReader reader = storedProcedure.GetDataReader();
                while (reader.Read())
                {
                    DatabaseConnection connection = SetUpDatabaseConnection(reader);
                    connections.Add(connection);
                }
            }

            return connections;
        }

        private DatabaseConnection SetUpDatabaseConnection(SqlDataReader reader)
        {
            var connection = new DatabaseConnection
            {
                Id = reader.SafeValue("Id", 0),
                DatabaseName = reader.SafeValue("DatabaseName", ""),
                Host = reader.SafeValue("Host", ""),
                Password = reader.SafeValue("Password", ""),
                UserName = reader.SafeValue("UserName", "")
            };

            return connection;
        }

        private List<SqlParameter> SetUpResultParameters(TestResult testResult)
        {
            if (string.IsNullOrWhiteSpace(testResult.Exception))
            {
                testResult.Exception = "N/A";
            }

            if (testResult.Errors != null)
            {
                var stringBuilder = new StringBuilder();
                foreach (string error in testResult.Errors.Errors)
                {
                    stringBuilder.Append(string.Format("| {0} |", error));
                }

                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@Database", testResult.Database),
                    new SqlParameter("@Server", testResult.Server),
                    new SqlParameter("@ObjectName", testResult.ObjectName),
                    new SqlParameter("@Errors", stringBuilder.ToString()),
                    new SqlParameter("@Exception", testResult.Exception)
                };

                return sqlParameters;
            }
            else
            {
                var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@Database", testResult.Database),
                    new SqlParameter("@Server", testResult.Server),
                    new SqlParameter("@ObjectName", testResult.ObjectName),
                    new SqlParameter("@Errors", ""),
                    new SqlParameter("@Exception", testResult.Exception)
                };

                return sqlParameters;
            }
        }
    }
}