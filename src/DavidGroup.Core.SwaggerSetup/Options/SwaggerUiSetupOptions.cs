using DavidGroup.Core.SwaggerSetup.Extensions;

using Swashbuckle.AspNetCore.SwaggerUI;

namespace DavidGroup.Core.SwaggerSetup.Options;

/// <summary>
/// Options for <see cref="WebApplicationExtensions.UseDefaultSwagger"/>.
/// </summary>
public sealed class SwaggerUiSetupOptions
{
    /// <summary>Path segment Swagger UI is served under. Defaults to "swagger".</summary>
    public string RoutePrefix { get; set; } = "swagger";

    /// <summary>Whether GET "/" redirects to Swagger UI. Set false if something else owns the root path.</summary>
    public bool RedirectRootToSwagger { get; set; } = true;

    /// <summary>Client id pre-filled into Swagger UI's OAuth2 dialog.</summary>
    public string? OAuthClientId { get; set; }

    /// <summary>Enables PKCE for the OAuth2 authorization code flow. Auto-enabled if OAuth2 was registered.</summary>
    public bool? UsePkce { get; set; }

    /// <summary>Escape hatch for any <see cref="SwaggerUIOptions"/> not exposed above.</summary>
    public Action<SwaggerUIOptions>? ConfigureUi { get; set; }
}
