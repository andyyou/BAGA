using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessForFluent
{
    public class BreakAwayContext:DbContext
    {
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>().Property(d => d.Name).IsRequired();
            modelBuilder.Entity<Destination>().Property(d => d.Description).HasMaxLength(500);
            modelBuilder.Entity<Destination>().Property(d => d.Photo).HasColumnType("image");
            modelBuilder.Entity<Lodging>().Property(l => l.Name).IsRequired().HasMaxLength(200);

            // 設定Key
            modelBuilder.Entity<Trip>().HasKey(t => t.Identifier).Property(t => t.Identifier).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Person>().HasKey(p => p.SocialSecurityNumber).Property(p => p.SocialSecurityNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Person>().Property(p => p.RowVersion).IsRowVersion();

            modelBuilder.Entity<Trip>().Property(t => t.RowVersion).IsRowVersion();

            modelBuilder.Entity<Person>().Property(p => p.SocialSecurityNumber).IsConcurrencyToken();

            modelBuilder.Entity<Lodging>().Property(l => l.Owner).IsUnicode(false);

            // 設定 decimal 位數
            modelBuilder.Entity<Lodging>().Property(l => l.MilesFromNearestAirport).HasPrecision(8, 1);

            // Use complex type
            modelBuilder.ComplexType<Address>();
        }
    }
}
