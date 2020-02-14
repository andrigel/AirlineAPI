using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entityes
{
    public class TicketModlel
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Flight Flight { get; set; }
        public TicketClass TicketClass { get; set; }
        public int PremiumMarksUsedCount { get; set; }
        public bool IsBought { get; set; }
        public int EndPrice { get; set; }
    }
    public enum TicketClass
    {
        vip = 2,
        econom = 1
    }

}
