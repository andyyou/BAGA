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
        }
    }

    public class LodgingConfiguration : EntityTypeConfiguration<Lodging>
    {
        public LodgingConfiguration()
        {
            Property(l => l.Name).IsRequired().HasMaxLength(200);
            Property(l => l.Owner).IsUnicode(false);
            Property(l => l.MilesFromNearestAirport).HasPrecision(8, 1);
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
            HasKey(p => p.SocialSecurityNumber);
            Property(p => p.SocialSecurityNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.RowVersion).IsRowVersion();
            Property(p => p.SocialSecurityNumber).IsConcurrencyToken();
        }
    }

    public class AddressConfiguration : ComplexTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            Property(a => a.StreetAddress).HasMaxLength(150);
        }
    }

    public class PersonalInfoConfiguration : ComplexTypeConfiguration<PersonalInfo>
    {
        public PersonalInfoConfiguration()
        { 
        
        }
    }



}
