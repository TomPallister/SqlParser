using System.Collections.Generic;
using System.Linq;

namespace Fourth.SqlParser
{
    public class ErrorChecker
    {
        private static readonly string[] ReservedWords =
        {
            "EXTERNAL", "PIVOT", "UNPIVOT", "REVERT", "TABLESAMPLE",
            "CUBE", "MERGE", "ROLLUP", "TRY_CONVERT", "SEMANTICKEYPHRASETABLE",
            "SEMANTICSIMILARITYDETAILSTABLE", "SEMANTICSIMILARITYTABLE"
        };

        public static List<Errors> CheckError(string sqlToCheck)
        {
            var errors = new List<Errors>();
            if (sqlToCheck.Contains("*="))
            {
                errors.Add(Errors.StarEquals);
            }
            if (sqlToCheck.Contains("=*"))
            {
                errors.Add(Errors.EqualsStar);
            }
            if (ReservedWords.Any(sqlToCheck.Contains))
            {
                errors.Add(Errors.ReservedWord);
            }
            return errors;
        }
    }

    public enum Errors
    {
        StarEquals = 0,
        EqualsStar = 1,
        ReservedWord = 3
    }
}