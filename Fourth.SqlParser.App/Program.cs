using System.Collections.Generic;
using System.Linq;

namespace Fourth.SqlParser.App
{
    internal class Program
    {
        private static void Main(string[] args)
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