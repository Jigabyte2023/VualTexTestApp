using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VaultexTestApp.Models;

namespace Vaultex.DataAccess
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Organisations> Organisations { get; set; }
        public DbSet<Employees> Employees { get; set; }
    }
}
