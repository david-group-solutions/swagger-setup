using System.Reflection;

using DavidGroup.Core.SwaggerSetup.Attributes;
using DavidGroup.Core.SwaggerSetup.Configurations;
using DavidGroup.Core.SwaggerSetup.Options;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace DavidGroup.Core.SwaggerSetup.Builders;

/// <summary>
/// Builder for configuring Swagger options.
/// </summary>
/// <param name="services"><see cref="IServiceCollection"/> to add configured options to.</param>
public sealed class SwaggerSetupBuilder(IServiceCollection services)
{
    /// <summary>
    /// Applies ordering to controllers which are decorated with <see cref="SwaggerControllerOrderAttribute"/>.
    /// </summary>
    /// <param name="assembly">Assembly to look for controllers.</param>
    /// <returns><see cref="SwaggerSetupBuilder"/> for chaining.</returns>
    public SwaggerSetupBuilder WithControllerOrdering(Assembly? assembly = null)
    {
        services.AddSingleton<IConfigureOptions<SwaggerGenOptions>>(
            new ControllerOrderingSwaggerOptions(assembly));

        return this;
    }

    /// <summary>
    /// Define document to be created by the Swagger generator for each API version.
    /// </summary>
    /// <param name="title">The title of the API to be displayed in Swagger UI.</param>
    /// <returns><see cref="SwaggerSetupBuilder"/> for chaining.</returns>
    public SwaggerSetupBuilder WithApiVersioning(string title)
    {
        services.AddSingleton<IConfigureOptions<SwaggerGenOptions>>(sp =>
            new ApiVersioningSwaggerOptions(sp, title));

        return this;
    }

    /// <summary>
    /// Adds buttons for Bearer authentication.
    /// </summary>
    /// <returns><see cref="SwaggerSetupBuilder"/> for chaining.</returns>
    public SwaggerSetupBuilder WithBearerAuth()
    {
        services.ConfigureOptions<BearerAuthenticationSwaggerOptions>();

        return this;
    }

    /// <summary>
    /// Adds buttons for OAuth2 authentication.
    /// </summary>
    /// <returns><see cref="SwaggerSetupBuilder"/> for chaining.</returns>
    public SwaggerSetupBuilder WithOAuth2(Action<OAuth2CodeFlowOptions> configureOAuth)
    {
        services.Configure(configureOAuth);
        services.ConfigureOptions<OAuth2AuthenticationSwaggerOptions>();

        return this;
    }
}
