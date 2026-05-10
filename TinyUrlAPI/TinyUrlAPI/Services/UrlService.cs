using Microsoft.EntityFrameworkCore;
using TinyUrlAPI.Data;
using TinyUrlAPI.Models;

namespace TinyUrlAPI.Services
{
    public class UrlService : IUrlService
    {
        private readonly TinyUrlDbContext _context;

        public UrlService(TinyUrlDbContext context)
        {
            _context = context;
        }

        public async Task<TinyUrlModel> CreateAsync(TinyUrlModel tinyUrl)
        {
            _context.TinyUrl.Add(tinyUrl);
            await _context.SaveChangesAsync();
            return tinyUrl;
        }

        public async Task<TinyUrlModel?> GetByCodeAsync(string shortCode)
        {
            return await _context.TinyUrl
                .FirstOrDefaultAsync(u => u.ShortCode == shortCode);
        }

        public async Task<List<TinyUrlModel>> GetAllAsync()
        {
            return await _context.TinyUrl
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<TinyUrlModel>> GetPublicUrlsAsync()
        {
            return await _context.TinyUrl
                .Where(u => !u.IsPrivate)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<TinyUrlModel>> SearchAsync(string query)
        {
            return await _context.TinyUrl
                .Where(u => u.OriginalUrl.Contains(query) || u.ShortCode.Contains(query))
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> DeleteByCodeAsync(string shortCode)
        {
            var url = await GetByCodeAsync(shortCode);
            if (url == null) return false;

            _context.TinyUrl.Remove(url);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> DeleteAllAsync()
        {
            var count = await _context.TinyUrl.CountAsync();
            _context.TinyUrl.RemoveRange(_context.TinyUrl);
            await _context.SaveChangesAsync();
            return count;
        }

        public async Task IncrementClicksAsync(string shortCode)
        {
            var url = await GetByCodeAsync(shortCode);
            if (url != null)
            {
                url.Clicks++;
                await _context.SaveChangesAsync();
            }
        }
    }
}
