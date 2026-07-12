using DavidGroup.Core.SwaggerSetup.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApiVersioning().AddApiExplorer(opt =>
{
    opt.GroupNameFormat = "'v'VVV";
    opt.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDefaultSwagger(swagger => swagger
    .WithApiVersioning("Samples API")
    .WithControllerOrdering()
    .WithBearerAuth()
    .WithOAuth2(o =>
    {
        o.AuthorizationUrl = "https://idp.example.com/authorize";
        o.TokenUrl = "https://idp.example.com/token";
        o.Scopes = ["api.read", "api.write"];
    }));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDefaultSwagger(configure =>
    {
        configure.RoutePrefix = "swagger";
        configure.RedirectRootToSwagger = true;
        configure.OAuthClientId = "SamplesApi";
        configure.UsePkce = true;
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("api/hello-world", () => "Hello World!")
    .WithTags("HelloWorld");

app.Run();
