using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapper.Models
{
    public class FlightModel
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Length { get; set; }
        public int PlacesCount { get; set; }
        public int PlacesReserved { get; set; }
        public int Price { get; set; }
        public DateTime Start { get; set; }
    }
}
