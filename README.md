# DavidGroup.Core.SwaggerSetup

#### [![Release](https://github.com/david-group-solutions/swagger-setup/actions/workflows/release.yml/badge.svg)](https://github.com/david-group-solutions/swagger-setup/actions/workflows/release.yml) [![Nuget](https://img.shields.io/nuget/v/DavidGroup.Core.SwaggerSetup)](https://www.nuget.org/packages/DavidGroup.Core.SwaggerSetup/)

Ready-to-use Swagger configuration for fast and effortless API documentation setup.

---

## 🚀 Getting Started

### Install NuGet Package

Using the .NET CLI:

```bash
dotnet add package DavidGroup.Core.SwaggerSetup
```

Or via the Package Manager Console:

```bash
Install-Package DavidGroup.Core.SwaggerSetup
```

### How to use it?

Feel free to explore the [samples](https://github.com/david-group-solutions/swagger-setup/tree/main/samples) to find
practical examples for each feature.
New samples are added continuously as more features are developed.

## 📦 Key Features

### How to add Swagger

```csharp
builder.Services.AddDefaultSwagger(swagger => swagger
    .WithApiVersioning("Samples API") // add multiple Swagger documents based on available versions
    .WithControllerOrdering()         // adds support for [SwaggerControllerOrder(0)] attribute
    .WithBearerAuth()                 // configures Swagger to support Bearer authentication
    .WithOAuth2(o =>
    {
        o.AuthorizationUrl = "https://idp.example.com/authorize";
        o.TokenUrl = "https://idp.example.com/token";
        o.Scopes = ["api.read", "api.write"];
    })); // configures Swagger to support OAuth2 authentication

app.UseDefaultSwagger(configure =>
{
    configure.RoutePrefix = "swagger";      // default value
    configure.RedirectRootToSwagger = true; // default value
    configure.OAuthClientId = "SamplesApi"; // default is null
    configure.UsePkce = true;               // automatically is set to true when .WithOAuth2() is called, can be overriden
});
```

## 🤝 Contributing

Found a bug? Have an idea? Want to contribute?

* Submit an issue:
  https://github.com/david-group-solutions/swagger-setup/issues
* Create a pull request:
  https://github.com/david-group-solutions/swagger-setup/pulls

Contributions of any size are appreciated!

## 📝 License

Distributed under the **MIT license**.
See [License](https://github.com/david-group-solutions/swagger-setup/blob/main/LICENSE.txt) for more information.

Copyright © 2025-2026 David Khachatryan (David Group Solutions)
