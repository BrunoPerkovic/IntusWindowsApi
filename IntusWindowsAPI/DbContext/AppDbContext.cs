using IntusWindowsAPI.BL.Data;
using Microsoft.EntityFrameworkCore;

namespace IntusWindowsAPI.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private static string Host = "postgresintus.postgres.database.azure.com";
    private static string User = "amarant1995";
    private static string DBname= "postgres";
    private static string Password = "37miMehino!";
    private static string Port = "5432";
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetSection("ConnectionStrings:Intus")
                                          .Value ??
                                      "Server=postgresintus.postgres.database.azure.com;Database=;Port=5432;User Id=amarant1995;Password=37miMehino!;Ssl Mode=Require;";
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

            optionsBuilder.UseNpgsql(st ?? connectionString);
        }
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
}