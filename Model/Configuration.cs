using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model
{
    public class DestinationConfiguration:EntityTypeConfiguration<Destination>
    {
        public DestinationConfiguration()
        {
            Property(d => d.Name).IsRequired();
            Property(d => d.Description).HasMaxLength(500);
            Property(d => d.Photo).HasColumnType("image");
            HasMany(d => d.Lodgings).WithRequired(l => l.Destination);
            // HasMany(d => d.Lodgings).WithRequired().HasForeignKey(l => l.LocationId);
            ToTable("Locations");
            Property(d => d.DestinationId).HasColumnName("LocationId");
            Property(d => d.Name).HasColumnName("LocationName");
        }
    }

    public class LodgingConfiguration : EntityTypeConfiguration<Lodging>
    {
        public LodgingConfiguration()
        {
            Property(l => l.Name).IsRequired().HasMaxLength(200);
            Property(l => l.Owner).IsUnicode(false);
            Property(l => l.MilesFromNearestAirport).HasPrecision(8, 1);
            HasOptional(l => l.PrimaryContact).WithMany(p => p.PrimaryContactFor);
            HasOptional(l => l.SecondaryContact).WithMany(p => p.SecondaryContactFor);
            HasRequired(l => l.Destination).WithMany(d => d.Lodgings).WillCascadeOnDelete(false);
           
        }
    }

    public class TripConfiguration : EntityTypeConfiguration<Trip>
    {
        public TripConfiguration()
        {
            HasKey(t => t.Identifier).Property(t => t.Identifier).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.RowVersion).IsRowVersion();
        }
    }

    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            // HasKey(p => p.SocialSecurityNumber);
            Property(p => p.SocialSecurityNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.RowVersion).IsRowVersion();
            Property(p => p.SocialSecurityNumber).IsConcurrencyToken();
            HasRequired(p => p.Photo);
            ToTable("People");
           

        }
    }

    public class AddressConfiguration : ComplexTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            Property(a => a.StreetAddress).HasMaxLength(150);
            Property(a => a.City).HasColumnName("City");
            Property(a => a.State).HasColumnName("State");
            Property(a => a.StreetAddress).HasColumnName("StreetAddress");
            Property(a => a.ZipCode).HasColumnName("ZipCode");
        }
    }

    public class PersonalInfoConfiguration : ComplexTypeConfiguration<PersonalInfo>
    {
        public PersonalInfoConfiguration()
        { 
        
        }
    }

    public class InternetSpecialConfiguration : EntityTypeConfiguration<InternetSpecial>
    {
        public InternetSpecialConfiguration()
        {
            HasRequired(i => i.Accommodation).WithMany(l => l.InternetSpecials).HasForeignKey(i => i.AccommodationId);
        }
    }

    public class ActivityConfiguration : EntityTypeConfiguration<Activity>
    {
        public ActivityConfiguration()
        {
            Property(a => a.Name).IsRequired().HasMaxLength(150);
        }
    }

    public class PersonPhotoConfiguration : EntityTypeConfiguration<PersonPhoto>
    {
        public PersonPhotoConfiguration()
        {
            HasKey(p => p.PersonId);
            // HasRequired(p => p.PhotoOf).WithOptional(p => p.Photo);
            HasRequired(p => p.PhotoOf).WithRequiredDependent(p => p.Photo);
            // HasEntitySetName("PersonPhotos");
            ToTable("PersonPhotos");
            Property(p => p.Photo).HasColumnType("image");
            ToTable("People");

        }
    }


}
