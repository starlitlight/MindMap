using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration; // For IConfiguration
using System.IO; // For Path and Directory
using System.ComponentModel.DataAnnotations;

public class MindMapContext : DbContext
{
    public DbSet<MindMap> MindMaps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "Your_Default_Connection_String_If_Not_Set";
            
            optionsBuilder.UseSqlServer(connectionString)
                          .UseLazyLoadingProxies(); // Enables lazy loading
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MindMap>(entity =>
        {
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            // Optionally, you can limit the length here as well, e.g., .HasMaxLength(255);
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
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; } // Nullable to signify an optional field
}