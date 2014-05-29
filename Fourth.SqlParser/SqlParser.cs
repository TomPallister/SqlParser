using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.SqlParser.Common;
using Microsoft.SqlServer.Management.SqlParser.Parser;

namespace Fourth.SqlParser
{
    public class SqlParser
    {
        public List<TestResult> Parse(DatabaseConnection connection)
        {
            var testResults = new List<TestResult>();
            var sourceConnection = new ServerConnection(connection.Host, connection.UserName, connection.Password);
            var sourceServer = new Server(sourceConnection);
            Database sourceDatabase = sourceServer.Databases[connection.DatabaseName];
            StoredProcedureCollection sps = sourceDatabase.StoredProcedures;
            UserDefinedFunctionCollection udfs = sourceDatabase.UserDefinedFunctions;
            ViewCollection views = sourceDatabase.Views;
            foreach (StoredProcedure storedProcedure in sps.Cast<StoredProcedure>())
            {
                try
                {
                    string header = "";
                    string body = "";
                    try
                    {
                        header = storedProcedure.TextHeader;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    try
                    {
                        body = storedProcedure.TextBody;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    string sprocText = string.Format("{0}{1}", header, body);
                    var options = new ParseOptions {CompatibilityLevel = DatabaseCompatibilityLevel.Version110};
                    ParseResult result = Parser.Parse(sprocText, options);
                    List<string> errors = result.Errors.Select(error => error.Message).ToList();
                    var testResultError = new TestResultError(errors);
                    var testResult = new TestResult
                    {
                        Database = connection.DatabaseName,
                        Errors = testResultError,
                        Server = connection.Host,
                        ObjectName = storedProcedure.Name
                    };
                    testResults.Add(testResult);
                }
                catch (Exception exception)
                {
                    var testResult = new TestResult
                    {
                        Database = connection.DatabaseName,
                        Errors = null,
                        Server = connection.Host,
                        ObjectName = storedProcedure.Name,
                        Exception = exception.ToString()
                    };
                    testResults.Add(testResult);
                }
            }

            foreach (UserDefinedFunction userDefinedFunction in udfs.Cast<UserDefinedFunction>())
            {
                try
                {
                    string header = "";
                    string body = "";
                    try
                    {
                        header = userDefinedFunction.TextHeader;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    try
                    {
                        body = userDefinedFunction.TextBody;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    string funcText = string.Format("{0}{1}", header, body);
                    var options = new ParseOptions {CompatibilityLevel = DatabaseCompatibilityLevel.Version110};
                    ParseResult result = Parser.Parse(funcText, options);
                    List<string> errors = result.Errors.Select(error => error.Message).ToList();
                    var testResultError = new TestResultError(errors);
                    var testResult = new TestResult
                    {
                        Database = connection.DatabaseName,
                        Errors = testResultError,
                        Server = connection.Host,
                        ObjectName = userDefinedFunction.Name
                    };
                    testResults.Add(testResult);
                }
                catch (Exception exception)
                {
                    var testResult = new TestResult
                    {
                        Database = connection.DatabaseName,
                        Errors = null,
                        Server = connection.Host,
                        ObjectName = userDefinedFunction.Name,
                        Exception = exception.ToString()
                    };
                    testResults.Add(testResult);
                }
            }

            foreach (View view in views.Cast<View>())
            {
                try
                {
                    string header = "";
                    string body = "";
                    try
                    {
                        header = view.TextHeader;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    try
                    {
                        body = view.TextBody;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    string viewText = string.Format("{0}{1}", header, body);
                    var options = new ParseOptions {CompatibilityLevel = DatabaseCompatibilityLevel.Version110};
                    ParseResult result = Parser.Parse(viewText, options);
                    List<string> errors = result.Errors.Select(error => error.Message).ToList();
                    var testResultError = new TestResultError(errors);
                    var testResult = new TestResult
                    {
                        Database = connection.DatabaseName,
                        Errors = testResultError,
                        Server = connection.Host,
                        ObjectName = view.Name
                    };
                    testResults.Add(testResult);
                }
                catch (Exception exception)
                {
                    var testResult = new TestResult
                    {
                        Database = connection.DatabaseName,
                        Errors = null,
                        Server = connection.Host,
                        ObjectName = view.Name,
                        Exception = exception.ToString()
                    };
                    testResults.Add(testResult);
                }
            }

            return testResults;
        }
    }
}