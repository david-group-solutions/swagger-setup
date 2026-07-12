using Asp.Versioning.ApiExplorer;

using DavidGroup.Core.SwaggerSetup.Options;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DavidGroup.Core.SwaggerSetup.Extensions;

/// <summary>
/// Provides extension methods for <see cref="WebApplication"/> to add Swagger middlewares.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Enables and configures Swagger middleware in the ASP.NET Core request pipeline with default UI and versioning support.
    /// </summary>
    public static WebApplication UseDefaultSwagger(
        this WebApplication app,
        Action<SwaggerUiSetupOptions>? configure = null)
    {
        SwaggerUiSetupOptions setup = new();
        configure?.Invoke(setup);

        OAuth2CodeFlowOptions? oauth = app.Services.GetService<IOptions<OAuth2CodeFlowOptions>>()?.Value;
        if (oauth is not null)
        {
            setup.UsePkce ??= true;
        }

        IApiVersionDescriptionProvider? apiVersionDescriptionProvider =
            app.Services.GetService<IApiVersionDescriptionProvider>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            if (apiVersionDescriptionProvider is not null)
            {
                foreach (ApiVersionDescription description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            }

            if (setup.UsePkce is true)
                options.OAuthUsePkce();

            if (setup.OAuthClientId is not null)
                options.OAuthClientId(setup.OAuthClientId);

            options.RoutePrefix = setup.RoutePrefix;

            setup.ConfigureUi?.Invoke(options);
        });

        if (setup.RedirectRootToSwagger && !string.IsNullOrEmpty(setup.RoutePrefix))
        {
            app.MapGet("/", () => Results.Redirect($"/{setup.RoutePrefix}"))
                .ExcludeFromDescription();
        }

        return app;
    }
}
