namespace Fourth.SqlParser
{
    public static class Logger
    {
        public static void Log(TestResult testResult)
        {
            var sqlParserDal = new SqlParserDal();
            sqlParserDal.Store(testResult);
        }
    }
}