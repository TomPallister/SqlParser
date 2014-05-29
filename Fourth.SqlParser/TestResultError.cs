using System.Collections.Generic;

namespace Fourth.SqlParser
{
    public class TestResultError
    {
        private readonly List<string> _errors = new List<string>();

        public TestResultError(List<string> errors)
        {
            _errors = errors;
        }

        public List<string> Errors
        {
            get { return _errors; }
        }
    }
}