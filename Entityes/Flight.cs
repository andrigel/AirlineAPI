using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entityes
{
    public class Flight
    {
        [Key]
        public int id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int length { get; set; }
        public int placesCount { get; set; }
        public int placesReserved { get; set; }
        public int price { get; set; }
        public List<int> ordersIds { get; set; }
        public DateTime start { get; set; }
    }
}
