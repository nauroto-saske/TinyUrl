namespace TinyUrlAPI.Services
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;
        private readonly string _logDirectory;

        public LogService(ILogger<LogService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _logDirectory = configuration["LogDirectory"] ?? "logs";

            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        public async Task LogAccessAsync(string shortCode, string originalUrl)
        {
            try
            {
                var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} | Code: {shortCode} | URL: {originalUrl}";
                var logFile = Path.Combine(_logDirectory, $"access-{DateTime.UtcNow:yyyy-MM-dd}.log");

                await File.AppendAllTextAsync(logFile, logEntry + Environment.NewLine);
                _logger.LogInformation("URL accessed: {ShortCode} -> {OriginalUrl}", shortCode, originalUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to write access log");
            }
        }
    }
}
