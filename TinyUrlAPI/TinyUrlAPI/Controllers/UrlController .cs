using Microsoft.AspNetCore.Mvc;
using TinyUrlAPI.Models;
using TinyUrlAPI.Services;

namespace TinyUrl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;
    private readonly IShortCodeGenerator _codeGenerator;
    private readonly ILogService _logService;
    private readonly IConfiguration _configuration;

    public UrlController(
        IUrlService urlService,
        IShortCodeGenerator codeGenerator,
        ILogService logService,
        IConfiguration configuration)
    {
        _urlService = urlService;
        _codeGenerator = codeGenerator;
        _logService = logService;
        _configuration = configuration;
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateUrl([FromBody] CreateUrlRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.OriginalUrl))
        {
            return BadRequest(new { error = "Original URL is required" });
        }

        if (!Uri.TryCreate(request.OriginalUrl, UriKind.Absolute, out _))
        {
            return BadRequest(new { error = "Invalid URL format" });
        }

        var shortCode = _codeGenerator.Generate();
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var tinyUrl = new TinyUrlModel
        {
            Id = Guid.NewGuid(),
            OriginalUrl = request.OriginalUrl,
            ShortCode = shortCode,
            ShortUrl = $"{baseUrl}/{shortCode}",
            IsPrivate = request.IsPrivate ?? false,
            CreatedAt = DateTime.UtcNow,
            Clicks = 0
        };

        await _urlService.CreateAsync(tinyUrl);

        return Ok(new
        {
            id = tinyUrl.Id,
            originalUrl = tinyUrl.OriginalUrl,
            shortUrl = tinyUrl.ShortUrl,
            shortCode = tinyUrl.ShortCode,
            isPrivate = tinyUrl.IsPrivate,
            clicks = tinyUrl.Clicks,
            createdAt = tinyUrl.CreatedAt
        });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUrls()
    {
        var urls = await _urlService.GetAllAsync();
        return Ok(urls.Select(u => new
        {
            id = u.Id,
            originalUrl = u.OriginalUrl,
            shortUrl = u.ShortUrl,
            shortCode = u.ShortCode,
            isPrivate = u.IsPrivate,
            clicks = u.Clicks,
            createdAt = u.CreatedAt
        }));
    }

    [HttpGet("public")]
    public async Task<IActionResult> GetPublicUrls()
    {
        var urls = await _urlService.GetPublicUrlsAsync();
        return Ok(urls.Select(u => new
        {
            id = u.Id,
            originalUrl = u.OriginalUrl,
            shortUrl = u.ShortUrl,
            shortCode = u.ShortCode,
            isPrivate = u.IsPrivate,
            clicks = u.Clicks,
            createdAt = u.CreatedAt
        }));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUrls([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest(new { error = "Query parameter is required" });
        }

        var urls = await _urlService.SearchAsync(query);
        return Ok(urls.Select(u => new
        {
            id = u.Id,
            originalUrl = u.OriginalUrl,
            shortUrl = u.ShortUrl,
            shortCode = u.ShortCode,
            isPrivate = u.IsPrivate,
            clicks = u.Clicks,
            createdAt = u.CreatedAt
        }));
    }

    [HttpDelete("delete/{code}")]
    public async Task<IActionResult> DeleteUrl(string code)
    {
        var deleted = await _urlService.DeleteByCodeAsync(code);
        if (!deleted)
        {
            return NotFound(new { error = "URL not found" });
        }
        return Ok(new { message = "URL deleted successfully" });
    }

    [HttpDelete("delete-all")]
    public async Task<IActionResult> DeleteAllUrls([FromHeader(Name = "X-Secret-Token")] string? token)
    {
        var secretToken = _configuration["AppSettings:SecretToken"] ?? _configuration["SecretToken"];

        if (string.IsNullOrWhiteSpace(token) || token != secretToken)
        {
            return Unauthorized();
        }

        var count = await _urlService.DeleteAllAsync();
        return Ok(new { message = $"Deleted {count} URLs" });
    }
}

public record CreateUrlRequest(string OriginalUrl, bool? IsPrivate);