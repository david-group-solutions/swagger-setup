using System.Text.Json.Nodes;

using Microsoft.OpenApi;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace DavidGroup.Core.SwaggerSetup.Filters;

/// <summary>
/// Ensures every documented operation exposes the API version as a required query parameter,
/// pre-filled with the version of the document currently being generated.
/// </summary>
public class SwaggerApiVersionParameterFilter : IOperationFilter
{
    private const string VersionParameterName = "api-version";

    /// <summary>
    /// Implemented method for the operation filter.
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // GroupName is only set for endpoints that belong to a versioned Swagger doc.
        string? version = context.ApiDescription.GroupName?.TrimStart('v', 'V');
        if (string.IsNullOrEmpty(version)) return;

        operation.Parameters ??= [];

        IOpenApiParameter? parameter = operation.Parameters
            .FirstOrDefault(p => p.Name == VersionParameterName);

        if (parameter is null)
        {
            parameter = new OpenApiParameter
            {
                Name = VersionParameterName,
                In = ParameterLocation.Query
            };

            operation.Parameters.Add(parameter);
        }

        OpenApiParameter apiVersionParameter = (OpenApiParameter)parameter;

        apiVersionParameter.Required = true;
        apiVersionParameter.Description ??= "The API version to use, e.g. 1.0";
        apiVersionParameter.Schema ??= new OpenApiSchema { Type = JsonSchemaType.String };

        OpenApiSchema schema = apiVersionParameter.Schema as OpenApiSchema ?? new OpenApiSchema { Type = JsonSchemaType.String };
        schema.Default = JsonValue.Create(version);

        apiVersionParameter.Schema = schema;
    }
}
