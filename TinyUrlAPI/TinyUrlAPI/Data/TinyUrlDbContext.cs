using System;
using Microsoft.EntityFrameworkCore;
using TinyUrlAPI.Models;

namespace TinyUrlAPI.Data
{
    public class TinyUrlDbContext: DbContext
    {
        public TinyUrlDbContext(DbContextOptions<TinyUrlDbContext> options)
     : base(options)
        {

        }
        public DbSet<TinyUrlModel> TinyUrl { get; set; }
    }
}
