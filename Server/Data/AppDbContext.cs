using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuintrixFullstack.Server.Data;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string appsettingsFilename;
        string connectionName;

        switch (Environment.GetEnvironmentVariable("ASPNET_ENVIRONMENT"))
        {
        case "production":
            appsettingsFilename = "appsettings.json";
            connectionName = "ProductionConnection";
            break;
        default:
            appsettingsFilename = "appsettings.Development.json";
            connectionName = "DevelopmentConnection";
            break;
        }

        var config = new ConfigurationBuilder()
            .AddJsonFile(appsettingsFilename)
            .Build();

        options.UseSqlite(config.GetConnectionString(connectionName) ??
            throw new KeyNotFoundException($"Could not retrieve {connectionName} from configuration."));
    }
}