namespace DavidGroup.Core.SwaggerSetup.Options;

/// <summary>
/// Represents the configuration for an OAuth 2.0 Authorization Code flow
/// used by Swagger/OpenAPI authentication.
/// </summary>
public class OAuth2CodeFlowOptions
{
    /// <summary>
    /// Gets or sets the authorization endpoint URL where users are redirected
    /// to authenticate and grant access.
    /// </summary>
    public string AuthorizationUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the token endpoint URL used to exchange the authorization
    /// code for an access token.
    /// </summary>
    public string TokenUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the OAuth 2.0 scopes that should be requested by Swagger UI.
    /// </summary>
    public string[] Scopes { get; set; } = [];
}
