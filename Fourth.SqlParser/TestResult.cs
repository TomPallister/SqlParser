namespace Fourth.SqlParser
{
    public class TestResult
    {
        public string Database { get; set; }
        public string Server { get; set; }
        public string ObjectName { get; set; }
        public TestResultError Errors { get; set; }
        public string Exception { get; set; }
    }
}