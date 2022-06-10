# Web API

## Required extensions and packets
To edit this API on vscode it is necessary to install the following extensions:
- C#
- Insert GUID
- NuGet Gallery

The NuGet is used to install the following packets:
- Microsoft.EntityFrameworkCore.SqlServer v6.0.3
- AutoMapper.Extensions.Microsoft.DependencyInjection
- System.IdentityModel.Tokens.Jwt
- Microsoft.AspNetCore.Authentication.Jwt

Besides those packets, it is necessary to install the dotnet-ef through a command line command:
```
dotnet tool install --global dotnet-ef --version 6.0.3
```
Other versions can be found on [nuget](https://www.nuget.org/packages/dotnet-ef/).

