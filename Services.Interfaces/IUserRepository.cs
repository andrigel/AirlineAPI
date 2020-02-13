using AutoMapper.Models;
using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> GetUsersAll();
        public ValueTask<ApplicationUser> GetUser(string id,bool IncludeTickets = false);
    }
}
