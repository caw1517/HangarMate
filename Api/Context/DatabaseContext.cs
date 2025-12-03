using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Context;

public class DatabaseContext : DbContext
{
    public DbSet<LogItem> LogItems { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            @"
        Host=db.eivuljqjdgtcwbudexmm.supabase.co;
        Port=5432;
        Database=postgres;
        Username=postgres;
        Password=tt9t2a06YOWST4NS;
        SslMode=Require;
        Trust Server Certificate=true");

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