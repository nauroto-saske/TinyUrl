using TinyUrlAPI.Models;

namespace TinyUrlAPI.Services
{
    public interface IUrlService
    {
        Task<TinyUrlModel> CreateAsync(TinyUrlModel tinyUrl);
        Task<TinyUrlModel?> GetByCodeAsync(string shortCode);
        Task<List<TinyUrlModel>> GetAllAsync();
        Task<List<TinyUrlModel>> GetPublicUrlsAsync();
        Task<List<TinyUrlModel>> SearchAsync(string query);
        Task<bool> DeleteByCodeAsync(string shortCode);
        Task<int> DeleteAllAsync();
        Task IncrementClicksAsync(string shortCode);  
    }
}
