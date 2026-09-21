using Asp.Versioning;

using DavidGroup.Core.SwaggerSetup.Attributes;

using Microsoft.AspNetCore.Mvc;

namespace DavidGroup.Core.SwaggerSetup.Samples.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0", Deprecated = true)]
[ApiVersion("1.1")]
[ApiVersion("2.0")]
[Tags("Examples / API Versioning")]
[SwaggerControllerOrder(200 + 2)]
public class ApiVersioningController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult V1Point0()
    {
        return Ok("Version v1.0");
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    public IActionResult V1Point1()
    {
        return Ok("Version v1.1");
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult V2Point0()
    {
        return Ok("Version v2.0");
    }
}
