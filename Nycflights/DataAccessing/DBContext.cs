using Microsoft.EntityFrameworkCore;
using Nycflights.Models;
using System.Collections.Generic;

namespace Nycflights.DataAccessing
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Airline>? Airlines { get; set; }
        public DbSet<Airport>? Airports { get; set; }
        public DbSet<Flight>? Flights { get; set; }
        public DbSet<Planes>? Planes { get; set; }
        public DbSet<Weather>? Weather { get; set; }
        public IEnumerable<object>? Flight { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .HasKey(f => new { f.Origin, f.Dest, f.Tailnum });

            modelBuilder.Entity<Weather>()
                .HasKey(w => new { w.Origin, w.Time_hour });

            modelBuilder.Entity<Flight>()
                .Property(f => f.FlightNumber).HasColumnName("Flight");
        }
<<<<<<< HEAD
        // test jwan 2
=======
>>>>>>> master
        // test jwan 3

    }
}
