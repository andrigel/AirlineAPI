using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entityes
{
    public class ApplicationUser : IdentityUser
    {
    /*    public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }*/
        public int PremiumMarksCount { get; set; }
        public int Year { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
