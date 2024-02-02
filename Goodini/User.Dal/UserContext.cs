using Microsoft.EntityFrameworkCore;

namespace User.Dal;

public class UserContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}