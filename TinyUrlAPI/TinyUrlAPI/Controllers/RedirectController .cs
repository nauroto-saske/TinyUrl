using Microsoft.AspNetCore.Mvc;
using TinyUrlAPI.Services;


namespace TinyUrl.Controllers;

[ApiController]
public class RedirectController : ControllerBase
{
    private readonly IUrlService _urlService;
    private readonly ILogService _logService;

    public RedirectController(IUrlService urlService, ILogService logService)
    {
        _urlService = urlService;
        _logService = logService;
    }

    [HttpGet("/{code}")]
    public async Task<IActionResult> RedirectToUrl(string code)
    {
        var tinyUrl = await _urlService.GetByCodeAsync(code);

        if (tinyUrl == null)
        {
            return NotFound("URL not found");
        }


        await _urlService.IncrementClicksAsync(code);

        await _logService.LogAccessAsync(code, tinyUrl.OriginalUrl);

        return Redirect(tinyUrl.OriginalUrl);
    }

    [HttpGet("/health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }
}