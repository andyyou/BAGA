using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessForAnnotation;
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
            UpdateTrip();

            InsertPerson();
            UpdatePerson();

            DeleteDestinationInMemoryAndDbCascase();
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
                StartDate = new DateTime(2009,9,21),
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
                FirstName = "Rowane",
                LastName = "Miller",
                // 這個時候設值是沒用的。須加入[Key, DatabaseGenerated(DatabaseGeneratedOption.None)], 加入後如果沒有設值會帶入0
                SocialSecurityNumber = 12345678,
                Photo = new PersonPhoto { Photo = new Byte[] { 0 } } 
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
                trip.CostUSD = 600;
                Console.WriteLine(trip.RowVersion);
                context.SaveChanges();
                Console.WriteLine(trip.RowVersion);
            }
        }

        private static void UpdatePerson()
        {
            using (var context = new BreakAwayContext())
            {
                var person = context.People.Include("Photo").FirstOrDefault();
                person.FirstName = "Rowena!";
                if (person.Photo == null)
                {
                    person.Photo = new PersonPhoto { Photo = new byte[] { 0 } };
                }
                context.SaveChanges();
            }
        }

        private static void DeleteDestinationInMemoryAndDbCascase()
        {
            int destinationId;

            using (var context = new BreakAwayContext())
            {
                var destination = new Destination
                {
                    Name = "Sample Destination",
                    Lodgings =new List<Lodging> { 
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
                context.Destinations.Remove(destination);
                Console.WriteLine("State of one Lodging : {0}", context.Entry(aLodging).State.ToString());
                context.SaveChanges();
            }
        }

        #region Converter

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        #endregion
    }

    
}
