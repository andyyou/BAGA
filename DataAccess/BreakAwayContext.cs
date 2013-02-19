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

            // modelBuilder.Entity<Person>().HasKey(p => p.SocialSecurityNumber).Property(p => p.SocialSecurityNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            

            modelBuilder.Entity<Person>().Property(p => p.RowVersion).IsRowVersion();

            modelBuilder.Entity<Trip>().Property(t => t.RowVersion).IsRowVersion();

            modelBuilder.Entity<Person>().Property(p => p.SocialSecurityNumber).IsConcurrencyToken();

            modelBuilder.Entity<Lodging>().Property(l => l.Owner).IsUnicode(false);

            // 設定 decimal 位數
            modelBuilder.Entity<Lodging>().Property(l => l.MilesFromNearestAirport).HasPrecision(8, 1);

            // Use complex type
            modelBuilder.ComplexType<Address>();

            modelBuilder.ComplexType<PersonalInfo>();
            modelBuilder.ComplexType<Address>().Property(a => a.StreetAddress).HasMaxLength(150);

            // 設定一對多關係
            // 預設
            modelBuilder.Entity<Destination>().HasMany(d => d.Lodgings).WithOptional(l => l.Destination);
            // 單個必須
            modelBuilder.Entity<Destination>().HasMany(d => d.Lodgings).WithRequired(l => l.Destination);

            // modelBuilder.Entity<Lodging>().Property(l => l.Name).IsRequired();

            modelBuilder.Entity<InternetSpecial>().HasRequired(i => i.Accommodation).WithMany(l => l.InternetSpecials).HasForeignKey(i => i.AccommodationId);

            // 當一個類別有兩個同樣屬性時設定
            modelBuilder.Entity<Lodging>().HasOptional(l => l.PrimaryContact).WithMany(p => p.PrimaryContactFor);
            modelBuilder.Entity<Lodging>().HasOptional(l => l.SecondaryContact).WithMany(p => p.SecondaryContactFor);

            // 不使用重複刪除
            // modelBuilder.Entity<Lodging>().HasRequired(l => l.Destination).WithMany(d => d.Lodgings).WillCascadeOnDelete(false);

            // 這邊要注意因為已經沒有Destination了
            // modelBuilder.Entity<Destination>().HasMany(d => d.Lodgings).WithRequired().HasForeignKey(l => l.LocationId);

            // 設定 1 對 1 或 1 對 0
            modelBuilder.Entity<PersonPhoto>().HasKey(p => p.PersonId);
            // modelBuilder.Entity<PersonPhoto>().HasRequired(p => p.PhotoOf).WithOptional(p => p.Photo);

            modelBuilder.Entity<PersonPhoto>().HasRequired(p => p.PhotoOf).WithRequiredDependent(p => p.Photo);
            modelBuilder.Entity<Person>().HasRequired(p => p.Photo).WithRequiredPrincipal(p => p.PhotoOf);
            
            
            // 修改資料庫 Table Name
            modelBuilder.Entity<PersonPhoto>().ToTable("PersonPhotots");

        }
    }
}
