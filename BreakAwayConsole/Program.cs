﻿using System;
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
            Database.SetInitializer(new DropCreateDatabaseAlways<BreakAwayContext>());
            InsertDestination();
            InserTrip();
            UpdateTrip();

            InsertPerson();
            UpdatePerson();

            DeleteDestinationInMemoryAndDbCascade();

            InsertLodging();
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
            var person = new Person
            {
                SocialSecurityNumber = 125699259,
                FirstName = "Ken",
                LastName = "Miller",
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
                // var person = context.People.Include("Photo").FirstOrDefault();
                var person = context.People.FirstOrDefault();
                person.FirstName = "Curz";
                //if (person.Photo == null)
                //{
                //   person.Photo = new PersonPhoto { Photo = new Byte[] { 0 } }; 
                //}


                //try
                //{
                    context.SaveChanges();
                //}
                //catch (DbEntityValidationException dbEx)
                //{
                //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    {
                //        foreach (var validationError in validationErrors.ValidationErrors)
                //        {
                //            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                //        }
                //    }
                //}
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


        private static void InsertResort()
        {
            var resort = new Resort
            {
                Name = "Top Notch Resort and Spa",
                MilesFromNearestAirport = 30,
                Activities = "Spa, Hiking, Skiing, Ballooning",
                Destination = new Destination
                {
                    Name = "Stowe, Vermont",
                    Country = "USA"
                }
            };
            using (var context = new BreakAwayContext())
            {
                context.Lodgings.Add(resort);
                context.SaveChanges();
            }
        }
        private static void InsertHostel()
        {
            var hostel = new Hostel
            {
                Name = "AAA Budget Youth Hostel",
                MilesFromNearestAirport = 25,
                PrivateRoomsAvailable = false,
                Destination = new Destination
                {
                    Name = "Hanksville, Vermont",
                    Country = "USA"
                }
            };
            using (var context = new BreakAwayContext())
            {
                context.Lodgings.Add(hostel);
                context.SaveChanges();
            }
        }
        private static void GetAllLodgings()
        {
            var context = new BreakAwayContext();
            var lodgings = context.Lodgings.ToList();
            foreach (var lodging in lodgings)
            {
                Console.WriteLine("Name: {0}  Type: {1}",
                   lodging.Name, lodging.GetType().ToString());
            }
            Console.ReadKey();
        }
    }


    
}
