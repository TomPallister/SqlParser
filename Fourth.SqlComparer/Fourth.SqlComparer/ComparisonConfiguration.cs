namespace Fourth.SqlComparer
{
    public class ComparisonConfiguration
    {
        public string SoureDatabaseName { get; set; }
        public string SourceServerName { get; set; }
        public string SourceUserName { get; set; }
        public string SourcePassword { get; set; }
        public string TargetDatabasName { get; set; }
        public string TargetServerName { get; set; }
        public string TargetUserName { get; set; }
        public string TargetPassword { get; set; }
    }
}