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
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ApplicationUser> ApplicationsUsers { get; set; }
        public DbSet<UserMark> UserMarks { get; set; }
        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>().HasKey(t => new { t.FlightId, t.UserId });
            modelBuilder.Entity<Ticket>().HasKey(t => t.Id);

            modelBuilder.Entity<Ticket>().HasOne(t => t.Flight).WithMany(f => f.Tickets).HasForeignKey(t => t.FlightId);
            modelBuilder.Entity<Ticket>().HasOne(t => t.User).WithMany(u => u.Tickets).HasForeignKey(t => t.UserId);

            modelBuilder.Entity<UserMark>().HasKey(t => new { t.FlightId, t.UserId });

            modelBuilder.Entity<UserMark>().HasOne(t => t.User).WithMany(u => u.UserMarks).HasForeignKey(um => um.UserId);
            modelBuilder.Entity<UserMark>().HasOne(t => t.Flight).WithMany(f => f.UserMarks).HasForeignKey(um => um.FlightId);

            /*    modelBuilder.Entity<UserFlight>()
                    .HasOne(uf => uf.flight)
                    .WithMany(f => f.UserFlights)
                    .HasForeignKey(uf => uf.FlightId)
                    .OnDelete(DeleteBehavior.SetNull);


                modelBuilder.Entity<UserFlight>()
                    .HasOne(uf => uf.user)
                    .WithMany(u => u.UserFlight)
                    .HasForeignKey(uf => uf.UserId)
                    .OnDelete(DeleteBehavior.SetNull);*/
        }
    }

    public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
    {
        public EFDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
            optionsBuilder.UseSqlServer("Server=COMPUTER;Database=AirlineAPI;User Id=IIS;Password=12346789;MultipleActiveResultSets=true", b => b.MigrationsAssembly("DataLayer"));
            return new EFDBContext(optionsBuilder.Options);
        }
    }
}