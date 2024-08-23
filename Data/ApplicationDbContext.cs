using Microsoft.EntityFrameworkCore;
using System;

public class MindMapContext : DbContext
{
    public DbSet<MindMap> MindMaps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "Your_Default_Connection_String_If_Not_Set";

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

public class MindMap
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}