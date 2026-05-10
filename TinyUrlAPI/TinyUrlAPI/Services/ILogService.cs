namespace TinyUrlAPI.Services
{
    public interface ILogService
    {
        Task LogAccessAsync(string shortCode, string originalUrl);  
    }
}
