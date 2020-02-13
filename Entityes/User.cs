using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entityes
{
    public class User : IdentityUser
    {
        public string password { get; set; }
        public string role { get; set; }
        public int premiumMarksCount { get; set; }
        public int year { get; set; }
        public List<int> ordersIds { get;set; }
    }
}
