# Web API
This API was built with .NET core 6.0.300.

## Required extensions and packets
To edit this API on vscode it is necessary to install the following extensions:
- C#
- Insert GUID
- NuGet Gallery

The NuGet extension is used to install the following packets:
- Microsoft.EntityFrameworkCore.SqlServer v6.0.3
- AutoMapper.Extensions.Microsoft.DependencyInjection v11.0.0
- System.IdentityModel.Tokens.Jwt v6.19.0
- Microsoft.AspNetCore.Authentication.Jwt v6.0.5
- Microsoft.AspNetCore.Identity.EntityFrameworkCore v6.0.5

Besides those packets, it is necessary to install the dotnet-ef through the command line:
```
dotnet tool install --global dotnet-ef --version 6.0.3
```

Other versions can be found on [NuGet](https://www.nuget.org/packages/dotnet-ef/).

## Deploying the database
To persist changes in entities or to add a new entity, delete the Migrations folder and run the following command:
```
dotnet ef migrations add InitialCreate -o Data/Migrations
```
To deploy the database, run the following command:
```
dotnet ef database update
```

## Custom domain
This API uses a custom domain (webapi.io) to use a certificate.

A new entry has to be added into the file C:\Windows\System32\drivers\etc\hosts, in order to map the custom domain to the IP. The entry has to be added in the following format:
```
xxx.xxx.xxx.xxx webapi.io
```

Where xxx.xxx.xxx.xxx is the server IP.

## Trusting the certificate
In order to be trusted, the certificate needs to be imported to the "Manage User Certificates" Windows tool.

So, inside the tool, select the "Trusted Root Certification Authorities" option, and in the "Certificates" folder click with the right mouse button and then "All Taks -> Import...".

Then Select the "webapi.pfx" file and enter the private key "!Pa55W0rd!".

## Using the certificate private key
This API uses the dotnet user-secrets to handle the private key. This mesure is adopted to not expose the private key in the source code. To set the password, issue the following command:
```
dotnet user-secrets set "CertPassword" "!Pa55W0rd!"
```

It will create a folder with the secret set on the <UserSecretsId> and will store the certificate private key.
