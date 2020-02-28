using DataLayer;
using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Models;

namespace Services.Implementations
{
    public class EFUserRepository : IUserRepository
    {
        private readonly EFDBContext _context;
        private readonly IMapper _mapper;

        public EFUserRepository(EFDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserModel> GetUser(string id)
        {
            return _mapper.Map<UserModel>(await _context.ApplicationsUsers.FindAsync(id));
        }

        public List<UserModel> GetUsersAll()
        {
            return _context.Users.AsEnumerable().Select(u => _mapper.Map<UserModel>(u)).ToList();
        }

        public async Task ModifyUser(UserModel u)
        {
            var user = await _context.ApplicationsUsers.FindAsync(u.Id);

            user.UserName = u.UserName;
            user.Year = u.Year;
            user.PremiumMarksCount = u.PremiumMarksCount;
            user.Email = u.Email;

            await _context.SaveChangesAsync();
        }
    }
}
