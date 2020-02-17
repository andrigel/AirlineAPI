using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entityes
{
    public class UserMark
    {
        [Key]
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public Flight Flight { get; set; }
        public string UserId { get; set; }
        public Guid FlightId { get; set; }
    }
}