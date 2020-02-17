using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entityes
{
    public class Flight
    {
        [Key]
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Length { get; set; }
        public int PlacesCount { get; set; }
        public int PlacesReserved { get; set; }
        public int Price { get; set; }
        public DateTime Start { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<UserMark> UserMarks { get; set; }
    }
}
