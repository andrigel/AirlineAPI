using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

//: IdentityDbContext<User>
namespace DataLayer
{
    public class EFDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<TicketModlel> Tickets { get; set; }
        public DbSet<ApplicationUser> ApplicationsUsers { get; set; }
        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TicketModlel>().HasOne(t => t.Flight).WithMany(f=>f.Tickets).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<TicketModlel>().HasOne(t => t.User).WithMany(u => u.Tickets).OnDelete(DeleteBehavior.SetNull);
        }
    }

    public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
    {
        public EFDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AirlineDb;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("DataLayer"));
            return new EFDBContext(optionsBuilder.Options);
        }
    }
}