using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entityes
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public string userId { get; set; }
        public int flightId{ get; set; }
        public bool withBack { get; set; }
        public TicketClass ticketClass { get; set; }
        public bool premiumMarksUsing { get; set; }
    }
    public enum TicketClass
    {
        vip = 1,
        econom = 2
    }

}
