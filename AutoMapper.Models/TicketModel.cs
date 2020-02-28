using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapper.Models
{
    public class TicketModel
    {
        public Guid Id { get; set; }
        public TicketClass ticketClass { get; set; }
        public int PremiumMarksUsedCount { get; set; }
        public bool IsBought { get; set; }
        public int CountReserved { get; set; }
        public int EndPrice { get; set; }
    }
}
