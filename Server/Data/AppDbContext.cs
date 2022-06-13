using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuintrixFullstack.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User>? Users { get; set; }
}