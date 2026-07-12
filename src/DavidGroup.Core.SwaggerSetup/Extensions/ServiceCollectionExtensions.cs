using DavidGroup.Core.SwaggerSetup.Builders;
using DavidGroup.Core.SwaggerSetup.Configurations;

using Microsoft.Extensions.DependencyInjection;

namespace DavidGroup.Core.SwaggerSetup.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to registering and configuring Swagger.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers Swashbuckle and configures it using different variety Swagger conventions.
    /// </summary>
    public static IServiceCollection AddDefaultSwagger(
        this IServiceCollection services,
        Action<SwaggerSetupBuilder> configure)
    {
        services.AddSwaggerGen(options =>
        {
            options.UseAllOfToExtendReferenceSchemas();
            options.SupportNonNullableReferenceTypes();
        });

        services.ConfigureOptions<GeneralSwaggerOptions>();

        configure(new SwaggerSetupBuilder(services));

        return services;
    }
}
