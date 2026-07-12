using System.Reflection;

using DavidGroup.Core.SwaggerSetup.Attributes;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace DavidGroup.Core.SwaggerSetup.Configurations;

/// <summary>
/// Applies options for custom controller ordering via <see cref="SwaggerControllerOrder"/>.
/// </summary>
/// <param name="assembly">Assembly to look for controllers.</param>
public class ControllerOrderingSwaggerOptions(Assembly? assembly = null) : IConfigureOptions<SwaggerGenOptions>
{
    /// <summary>
    /// Implemented method for configuration.
    /// </summary>
    public void Configure(SwaggerGenOptions options)
    {
        SwaggerControllerOrder swaggerControllerOrder = new(assembly ?? Assembly.GetEntryAssembly() ??
            throw new NullReferenceException("Assembly is not provided to determine controllers order."));
        options.OrderActionsBy(apiDesc => swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"]));
    }
}
