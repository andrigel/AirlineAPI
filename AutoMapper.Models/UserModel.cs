using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapper.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int PremiumMarksCount { get; set; }
        public int Year { get; set; }
    }
}
