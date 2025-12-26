using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    
    public DbSet<LogItem> LogItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogItem>(entity =>
        {
            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        
    }
    

}