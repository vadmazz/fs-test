using Microsoft.EntityFrameworkCore;

namespace Fs.Entities;

/// <summary>
/// Основной контекст для работы с базой данных посредством EF
/// </summary>
public class CoreContext : DbContext
{
    public DbSet<FileMetaData> Files { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
        var port = Environment.GetEnvironmentVariable("DATABASE_PORT");
        var schema = Environment.GetEnvironmentVariable("DATABASE_SCHEMA");
        var host = Environment.GetEnvironmentVariable("DATABASE_SERVER");
        var user = Environment.GetEnvironmentVariable("DATABASE_USER");
        
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(schema) ||
            string.IsNullOrEmpty(host) || string.IsNullOrEmpty(user))
            throw new ArgumentException("Incorrect database configuration. Consider adding env variables");
        
        var connectionString = $"Host={host};Port={port};Username={user};Password={password};Database={schema}";
        optionsBuilder.UseNpgsql(connectionString);
    }
}