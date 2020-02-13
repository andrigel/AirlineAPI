using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class TicketViewModel
    {
        public string UserId { get; set; }
        public string FlightId { get; set; }
        public int TicketClass { get; set; }
        public int PremiumMarksUsedCount { get; set; }
        public int IsBought { get; set; }
        public int EndPrice { get; set; }
    }
}
