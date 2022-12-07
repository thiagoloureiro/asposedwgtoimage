using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace ConversionService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VersionController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public string Get()
    {
        var version = Assembly.GetEntryAssembly()?.GetName().Version;
        return version!.ToString();
    }
}