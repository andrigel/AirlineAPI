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
        public List<UserModel> GetUsersAll();
        public Task<UserModel> GetUser(string id);
        public Task ModifyUser(UserModel u);
    }
}
