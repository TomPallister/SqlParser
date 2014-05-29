Notes
=====

All private information removed from solution. This means you need to do your own connections strings.
RedGate stuff in this project needs a licensed RedGate SDK installed to work optimally.

SqlParser
=========

Contains code using SQL Management libs that can parse SQL objects and check they will compile in a different comparability mode. Also contains an example of how to use RedGate compare to look at differences between SQL databases. 


Fourth.SqlParser.App

====================

This is a console application in the Fourth.SqlParser solution. When it runs it looks at a database and finds a list of connections then loops through them checking each one before writing the results to the same database (another table).

This application is backed by its main library (Fourth.SqlParser) an infrastructure library (Fourth.SqlParser.Infrastructure) and an MS database project (SqlParserDb).


Requirements

============
Visual Studio 2012+
.NET 4.5
MS SQL Server Express 2008 R2+
Microsoft SQL Management Libraries
XUnit Test Runner for Visual Studio or ReSharper and the XUnit plugin for ReSharper
RedGate SDK (for the SqlComparer anyway)

Tests
=====

Integration tests can be found in ParseSqlTests.cs however they only work when a database is set up and they are pointed at it.

How To
======

In order to use the application.

1. Deploy the DB somewhere.
2. Enter connections of databases you want to check into the DatabaseConnections table.
3. Run the application.
4. Get the results from the TestResults table.


Going forward
=============

Feel free to change this app however you want. Might be interesting to do a version where you pass parameters into the console application so you can do a list of dbs from a table and a specific one off if you just need to do that.


Fourth.SqlComparer
==================

Contains code that uses RedGate comparison SDK to compare two database schema. It consists of one project.

This is pretty simple code ripped from RedGates own example and I have just added a sort of test container object so we aint just passing strings around....well at least they are part of an object!

How To
======

Run the test in the project.

Going forward
=============

This needs to be a console application where you pass in the target and source databse info and it compares the two for you.

In order for this app to be useful it would probably need to change the way it logs differences and possibly get it to script the differences out somewhere. Loads of stuff is possible with RedGate SDK. If you not sure come and find Tom Pallister. 


The End
=======


