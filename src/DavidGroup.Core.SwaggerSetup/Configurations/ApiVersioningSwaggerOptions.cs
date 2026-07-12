using Asp.Versioning.ApiExplorer;

using DavidGroup.Core.SwaggerSetup.Filters;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace DavidGroup.Core.SwaggerSetup.Configurations;

/// <summary>
/// Define one or more documents to be created by the Swagger generator.
/// </summary>
/// <param name="serviceProvider">The default IServiceProvider.</param>
/// <param name="title">The title to display in Swagger UI for all API versions.</param>
public class ApiVersioningSwaggerOptions(IServiceProvider serviceProvider, string title)
    : IConfigureOptions<SwaggerGenOptions>
{
    /// <summary>
    /// Implemented method for configuration.
    /// </summary>
    public void Configure(SwaggerGenOptions options)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        IApiVersionDescriptionProvider? apiVersionDescriptionProvider = scope.ServiceProvider.GetService<IApiVersionDescriptionProvider>();
        if (apiVersionDescriptionProvider is null) return;

        foreach (ApiVersionDescription description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description, title));

        options.OperationFilter<SwaggerApiVersionParameterFilter>();
    }

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription desc, string title)
    {
        string version = desc.ApiVersion.ToString();

        OpenApiInfo info = new()
        {
            Title = $"{title} - v{version}",
            Version = version
        };

        if (desc.IsDeprecated)
            info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";

        return info;
    }
}
