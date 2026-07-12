using DavidGroup.Core.SwaggerSetup.Filters;

using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace DavidGroup.Core.SwaggerSetup.Configurations;

/// <summary>
/// Configures default Swagger generation options for an ASP.NET Core application, including API versioning, tagging,
/// action ordering, and schema filters.
/// </summary>
public class GeneralSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    /// <summary>
    /// Implemented method for configuration.
    /// </summary>
    public void Configure(SwaggerGenOptions options)
    {
        options.EnableAnnotations();

        options.TagActionsBy(api =>
        {
            if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                return [controllerActionDescriptor.ControllerName];

            IList<object> endpointMetadata = api.ActionDescriptor.EndpointMetadata;
            ITagsMetadata? tagMetadata = endpointMetadata.OfType<ITagsMetadata>().FirstOrDefault();
            if (tagMetadata != null)
                return tagMetadata.Tags.ToArray();

            return [api.RelativePath?.Split('/')[0] ?? "Default"];
        });

        options.SchemaFilter<SwaggerStronglyTypedIdSchemaFilter>();
    }
}
