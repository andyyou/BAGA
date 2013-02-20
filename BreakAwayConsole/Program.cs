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
            Database.SetInitializer(new DropCreateDatabaseAlways<BreakAwayContext>());
            InsertDestination();

            InsertTrip();
            UpdateTrip();

            InsertPerson();
            UpdatePerson();

            DeleteDestinationInMemoryAndDbCascade();
            Console.Read();
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
            var person = new Person
            {
                FirstName = "Andy",
                LastName = "You",
                SocialSecurityNumber = 134567230,
                Photo = new PersonPhoto { Photo = new byte[] { 0 } }
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

        private static void UpdatePerson()
        {
            using (var context = new BreakAwayContext())
            {
                var person = context.People.FirstOrDefault();
                person.FirstName = "Curz";
                context.SaveChanges();
            }
        }

        private static void DeleteDestinationInMemoryAndDbCascade()
        {
            int destinationId;

            using (var context = new BreakAwayContext())
            {
                var destination = new Destination
                {
                    Name = "Sample destination",
                    Lodgings = new List<Lodging> { 
                        new Lodging{ Name = "Lodging One"},
                        new Lodging{ Name = "Lodging Two"}
                    }
                };

                context.Destinations.Add(destination);
                context.SaveChanges();
                destinationId = destination.DestinationId;
            }

            using (var context = new BreakAwayContext())
            {
                var destination = context.Destinations.Include("Lodgings").Single(d => d.DestinationId == destinationId);
                var aLodging = destination.Lodgings.FirstOrDefault();
                var bLodging = destination.Lodgings.LastOrDefault();
                context.Destinations.Remove(destination);
                Console.WriteLine("State of one Lodging : {0}", context.Entry(aLodging).State.ToString());
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    context.Lodgings.Remove(aLodging);
                    context.Lodgings.Remove(bLodging);
                }
                finally
                {
                    context.SaveChanges();
                    Console.WriteLine("Save Completed");
                }
            }
        }

        private static void InsertLodging()
        {
            var lodging = new Lodging
            {
                Name = "Rainy Day Motel",
                Destination = new Destination { 
                    Name = "Seattle, Washington",
                    Country = "USA"
                }
            };

            using (var context = new BreakAwayContext())
            {
                context.Lodgings.Add(lodging);
                context.SaveChanges();
            }
        }
    }

    
}
