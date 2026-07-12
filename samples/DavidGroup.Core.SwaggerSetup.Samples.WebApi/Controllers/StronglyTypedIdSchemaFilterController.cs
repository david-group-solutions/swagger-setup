using System.ComponentModel.DataAnnotations;

using DavidGroup.Core.SwaggerSetup.Attributes;
using DavidGroup.Core.SwaggerSetup.Samples.WebApi.StronglyTypedIds;

using Microsoft.AspNetCore.Mvc;

namespace DavidGroup.Core.SwaggerSetup.Samples.WebApi.Controllers;

[SwaggerControllerOrder(0)]
[ApiController]
[Route("api/[controller]")]
public class StronglyTypedIdSchemaFilterController : ControllerBase
{
    [HttpGet]
    public IActionResult StronglyTypedIdSchemaFilterTest([FromQuery, Required] BookId bookId)
    {
        return Ok($"Received: {bookId}");
    }
}
