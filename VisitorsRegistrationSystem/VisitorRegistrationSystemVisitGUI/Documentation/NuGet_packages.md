# NuGet packages

The following NuGet packages were used:
 - DotNetEnv

 ## DotNetEnv
 A .NET library to load environment variables from .env files.
 This package is used to store the connection string of the database in a variable inside a .env file.
 This file is local and does not get pushed to the GitHub repository.
 The connection string gets stored in the following format:
`CONNECTION_STRING_DB=connection_string`
Where `CONNECTION_STRING_DB` is the environment variable and `connection_string` is the actual connection string.

This allows for easier management of the connection string. That way, the connection string does not need to be constantly changed to your specific connection string. Every developer can have his own connection string defined in that file.