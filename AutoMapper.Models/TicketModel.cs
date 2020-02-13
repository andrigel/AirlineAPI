using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapper.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public TicketClass ticketClass { get; set; }
        public int PremiumMarksUsedCount { get; set; }
        public int IsBought { get; set; }
        public int EndPrice { get; set; }
    }
}
