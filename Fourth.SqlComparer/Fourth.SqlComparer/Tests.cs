using System;
using System.Data.SqlClient;
using System.Linq;
using Fourth.SqlParser.Infrastructure.Infrastructure.Logging;
using RedGate.Shared.ComparisonInterfaces;
using RedGate.SQLCompare.Engine;
using Xunit;

namespace Fourth.SqlComparer
{
    public class Tests
    {
        [Fact]
        public void Test()
        {
            var comparisonConfiguration = new ComparisonConfiguration
            {
                SourceServerName = "SomeServerNameOrIp",
                SoureDatabaseName = "SomeDatabaseName",
                SourcePassword = "SomePassword",
                SourceUserName = "SomeUserName",
                TargetServerName = "SomeServerNameOrIp",
                TargetDatabasName = "SomeDatabaseName",
                TargetPassword = "SomePassword",
                TargetUserName = "SomeUserName"
            };


            using (Database stagingDb = new Database(), productionDb = new Database())
            {
                var sourceConnectionProperties =
                    new ConnectionProperties(comparisonConfiguration.SourceServerName,
                        comparisonConfiguration.SoureDatabaseName, comparisonConfiguration.SourceUserName,
                        comparisonConfiguration.SourcePassword);

                var targetConnectionProperties = new ConnectionProperties(
                    comparisonConfiguration.TargetServerName, comparisonConfiguration.TargetDatabasName,
                    comparisonConfiguration.TargetUserName, comparisonConfiguration.TargetPassword);

                // Connect to the two databases and read the schema
                try
                {
                    Console.WriteLine("Registering database " + sourceConnectionProperties.DatabaseName);
                    stagingDb.Register(sourceConnectionProperties, Options.Default);
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(@"
Cannot connect to database '{0}' on server '{1}'. The most common causes of this error are:
        o The sample databases are not installed
        o ServerName not set to the location of the target database
        o For sql server authentication, username and password incorrect or not supplied in ConnectionProperties constructor
        o Remote connections not enabled", sourceConnectionProperties.DatabaseName,
                        sourceConnectionProperties.ServerName);
                    return;
                }
                try
                {
                    Console.WriteLine("Registering database " + targetConnectionProperties.DatabaseName);
                    productionDb.Register(targetConnectionProperties, Options.Default);
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(@"
Cannot connect to database '{0}' on server '{1}'. The most common causes of this error are:
        o The sample databases are not installed
        o ServerName not set to the location of the target database
        o For sql server authentication, username and password incorrect or not supplied in ConnectionProperties constructor
        o Remote connections not enabled", targetConnectionProperties.DatabaseName,
                        targetConnectionProperties.ServerName);
                    return;
                }

                // Compare WidgetStaging against WidgetProduction
                Differences stagingVsProduction = stagingDb.CompareWith(productionDb, Options.Default);
                Log4NetLogger.LogEntry(GetType(), "SqlCompareResult",
                    string.Format("Total Differences = {0}", stagingVsProduction.Count(x => x.Type != DifferenceType.Equal)), LoggerLevel.Info);
                // Display the results on the console
                foreach (Difference difference in stagingVsProduction)
                {
                    if (difference.Type != DifferenceType.Equal)
                    {
                        Log4NetLogger.LogEntry(GetType(), "SqlCompareResult",
                            string.Format("{0} {1} {2}", difference.Type, difference.DatabaseObjectType, difference.Name),
                            LoggerLevel.Info);
                    }
                }
                Log4NetLogger.LogEntry(GetType(), "SqlCompareResult", string.Format("Finished writing differences"),
                    LoggerLevel.Info);
            }
        }
    }
}