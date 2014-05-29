namespace Fourth.SqlParser
{
    public class DatabaseConnection
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
    }
}