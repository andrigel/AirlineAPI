using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entityes
{
    public class ApplicationUser : IdentityUser
    {
        public int PremiumMarksCount { get; set; }
        public int Year { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<UserMark> UserMarks { get; set; }
    }
}
