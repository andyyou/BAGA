using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;
using DataAccessForEntityType;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace BreakAwayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BreakAwayContext>());
            InsertDestination();
            InserTrip();
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

        private static void InserTrip()
        {
            var trip = new Trip { 
                CostUSD = 800,
                StartDate = new DateTime(2011,9,1),
                EndDate = new DateTime(2011, 9, 14)
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
                SocialSecurityNumber = 345699258,
                FirstName = "Ken",
                LastName = "Miller"
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
                
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
        }

        private static void DeleteDestinationInMemoryAndDbCascade()
        {
            int destinationId;
            using (var context = new BreakAwayContext())
            {
                var destination = new Destination { Name = "Sample Destination", Lodgings = new List<Lodging> { new Lodging{ Name = "Lodging One"}, new Lodging{ Name = "Lodging Two"} } };

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
                Console.WriteLine("State of one Lodging: {0}", context.Entry(aLodging).State.ToString());
                // 如果關閉聯集刪除就必須為Required的屬性作處理.
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
                }
            }
        }
    }

    
}
