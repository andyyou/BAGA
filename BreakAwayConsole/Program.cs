using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessForFluent;
using System.Data.Entity;

namespace BreakAwayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BreakAwayContext>());
            InsertDestination();
            InsertTrip();
            InsertPerson();
            UpdateTrip();
        }

        private static void InsertDestination()
        {
            var destination = new Destination { 
                Country = "Indonesia",
                Description = "EcoTourism at its best in exquisite Bali",
                Name = "Bali"
            };

            using (var context = new BreakAwayContext())
            {
                context.Destinations.Add(destination);
                context.SaveChanges();
            }
        }

        private static void InsertTrip()
        {
            var trip = new Trip { 
                CostUSD = 800,
                StartDate = new DateTime(2009,1,1),
                EndDate = new DateTime(2012,12,21)
            };

            using (var context = new BreakAwayContext())
            {
                context.Trips.Add(trip);
                context.SaveChanges();
            }
        }

        private static void InsertPerson()
        {
            var person = new Person { 
                FirstName = "Andy",
                LastName = "You",
                SocialSecurityNumber = 123456789
            };

            using (var context = new BreakAwayContext())
            {
                context.People.Add(person);
                context.SaveChanges();
            }

        }

        private static void UpdateTrip()
        {
            using (var context = new BreakAwayContext())
            {
                var trip = context.Trips.FirstOrDefault();
                trip.CostUSD = 750;
                context.SaveChanges();
            }
        }
    }

    
}
