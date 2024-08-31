using Microsoft.EntityFrameworkCore;

namespace CribQuest.Backend.Models;

public class CribQuestDbContext : DbContext
{
    public CribQuestDbContext(DbContextOptions<CribQuestDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
}