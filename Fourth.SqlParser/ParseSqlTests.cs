using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fourth.SqlParser
{
    /// <summary>
    /// These tests need a database setting up to work and it needs to be 
    /// added as default connection in the projects app config
    /// </summary>
    public class ParseSqlTests
    {
        /// <summary>
        /// This test needs connection info adding to the dC object if it is doing to work.
        /// </summary>
        [Fact]
        public void can_parse_database()
        {
            var dC = new DatabaseConnection
            {
                DatabaseName = "SomeDatabaseName",
                Host = "SomeServerNameOrIp",
                Password = "SomePassword",
                UserName = "SomeUserName"
            };
            var sqlParser = new SqlParser();
            List<TestResult> results = sqlParser.Parse(dC);
            foreach (
                TestResult result in results.Where(result => result.Errors == null || result.Errors.Errors.Count > 0))
            {
                Logger.Log(result);
            }
        }

        [Fact]
        public void can_store_good_test_result()
        {
            var testResult = new TestResult
            {
                Database = "somedb",
                Errors = new TestResultError(new List<string>
                {
                    "some error",
                    "some other error"
                })
                ,
                ObjectName = "someobject",
                Server = "someserver"
            };

            Logger.Log(testResult);
        }

        [Fact]
        public void can_store_bad_test_result()
        {
            var testResult = new TestResult
            {
                Database = "somedb",
                Errors = null,
                ObjectName = "someobject",
                Server = "someserver",
                Exception = "some exception"
            };

            Logger.Log(testResult);
        }

        [Fact]
        public void can_get_connections()
        {
            var sqlParserDal = new SqlParserDal();
            List<DatabaseConnection> connections = sqlParserDal.Get();
            Assert.True(connections != null);
        }


        [Fact]
        public void parse_all_sql_2012_databses()
        {
            var sqlParserDal = new SqlParserDal();
            var connections = sqlParserDal.Get();
            foreach (TestResult result in from databaseConnection in connections let sqlParser = new SqlParser() select sqlParser.Parse(databaseConnection) into results from result in results.Where(result => result.Errors == null || result.Errors.Errors.Count > 0) select result)
            {
                Logger.Log(result);
            }
        }
    }
}