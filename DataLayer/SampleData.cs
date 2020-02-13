using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Entityes;

namespace DataLayer
{
    public static class SampleData
    {
        public static void InitData(EFDBContext context)
        {
            Random rand = new Random();
            if (!context.Flights.Any())
            {
                context.Flights.Add(new Flight { From = "City1", To = "place1", Length = rand.Next() % 2500, PlacesCount = 10, PlacesReserved = 0, Price = rand.Next() % 1000, Start = new DateTime()});
                context.Flights.Add(new Flight { From = "City2", To = "place2", Length = rand.Next() % 2500, PlacesCount = 10, PlacesReserved = 0, Price = rand.Next() % 1000, Start = new DateTime() });
                context.Flights.Add(new Flight { From = "City3", To = "place3", Length = rand.Next() % 2500, PlacesCount = 10, PlacesReserved = 0, Price = rand.Next() % 1000, Start = new DateTime() });
                context.Flights.Add(new Flight { From = "City4", To = "place4", Length = rand.Next() % 2500, PlacesCount = 10, PlacesReserved = 0, Price = rand.Next() % 1000, Start = new DateTime() });
                context.Flights.Add(new Flight { From = "City5", To = "place5", Length = rand.Next() % 2500, PlacesCount = 10, PlacesReserved = 0, Price = rand.Next() % 1000, Start = new DateTime() });
                context.SaveChanges();
            }
        }
    }
}