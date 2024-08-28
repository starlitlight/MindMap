using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.ComponentModel.DataAnnotations;

public class MindMapDbContext : DbContext
{
    public DbSet<MindMap> MindMaps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var dbConnectionString = configurationRoot.GetConnectionString("DefaultConnection") ?? 
                                    Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? 
                                    "Your_Default_Connection_String_If_Not_Set";
            
            optionsBuilder.UseSqlServer(dbConnectionString)
                          .UseLazyLoadingProxies(); 
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MindMap>(entity =>
        {
            entity.Property(e => e.Title)
                  .IsRequired();
            entity.Property(e => e.Description)
                  .IsRequired();
        });
    }
}

public class MindMap
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastModifiedDate { get; set; }
}