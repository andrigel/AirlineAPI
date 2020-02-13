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
            if (!context.flights.Any())
            {
                context.flights.Add(new Flight { from = "City1", to = "place1", length = rand.Next() % 2500, placesCount = 10, placesReserved = 0, price = rand.Next() % 1000, ordersIds = new List<int>(), start = new DateTime()});
                context.flights.Add(new Flight { from = "City2", to = "place2", length = rand.Next() % 2500, placesCount = 10, placesReserved = 0, price = rand.Next() % 1000, ordersIds = new List<int>(), start = new DateTime() });
                context.flights.Add(new Flight { from = "City3", to = "place3", length = rand.Next() % 2500, placesCount = 10, placesReserved = 0, price = rand.Next() % 1000, ordersIds = new List<int>(), start = new DateTime() });
                context.flights.Add(new Flight { from = "City4", to = "place4", length = rand.Next() % 2500, placesCount = 10, placesReserved = 0, price = rand.Next() % 1000, ordersIds = new List<int>(), start = new DateTime() });
                context.flights.Add(new Flight { from = "City5", to = "place5", length = rand.Next() % 2500, placesCount = 10, placesReserved = 0, price = rand.Next() % 1000, ordersIds = new List<int>(), start = new DateTime() });
                context.SaveChanges();
            }
        }
    }
}