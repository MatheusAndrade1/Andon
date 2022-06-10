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

Besides those packets, it is necessary to install the dotnet-ef through a command line command:
```
dotnet tool install --global dotnet-ef --version 6.0.3
```

Other versions can be found on [NuGet](https://www.nuget.org/packages/dotnet-ef/).

## Custom domain
This API uses a custom domain (webapi.io) to use a certificate

First, a new entry has to be added into the file C:\Windows\System32\drivers\etc\hosts, so the Windows can map the custom domain to the IP. The entry has to be in the following format:
```
xxx.xxx.xxx.xxx webapi.io
```

Where xxx.xxx.xxx.xxx is the server IP.

## Trusting the certificate
In order to be trusted, the certificate needs to be imported to the "Manage User Certificates" Windows tool.

Select the "Trusted Root Certification Authorities" option, and in the "Certificates" folder click with the mouse right button, and then "All Taks -> Import...".

Then Select the "webapi.pfx" file and enter the private key "!Pa55W0rd!".

## Using the certificate private key
This API uses the dotnet user-secrets to access the private key. This mesure is adopted to not expose the private key in the source code. To set the password, issue the following command:
```
dotnet user-secrets set "CertPassword" "!Pa55W0rd!"
```

It will create a folder with the secret set on the <UserSecretsId> and will store the certificate private key.
