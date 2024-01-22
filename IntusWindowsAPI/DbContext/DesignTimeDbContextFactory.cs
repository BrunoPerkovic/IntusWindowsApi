using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IntusWindowsAPI.DbContext;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private static string Host = "postgresintus.postgres.database.azure.com";
    private static string User = "amarant1995";
    private static string DBname= "postgres";
    private static string Password = "37miMehino!";
    private static string Port = "5432";
    public AppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration.GetSection("ConnectionStrings:IntusDb").Value;
        string connString =
            String.Format(
                "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                Host,
                User,
                DBname,
                Port,
                Password);
        var st =
            "Server=postgresintus.postgres.database.azure.com;Database=postgres;Port=5432;User Id=amarant1995;Password=37miMehino!;SSL Mode=Require;Trust Server Certificate=True";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(st);

        return new AppDbContext(optionsBuilder.Options);
    }
}