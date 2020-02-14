using AutoMapper;
using AutoMapper.Models;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EFTicketRepository : ITicketRepository
    {
        private readonly EFDBContext _context;
        public EFTicketRepository(EFDBContext context)
        {
            _context = context;
        }
        public async Task<List<TicketModel>> GetTicketsFromUser(string userId)
        {
            var user = await _context.ApplicationsUsers.Where(u => u.Id == userId).Include(u => u.Tickets).FirstOrDefaultAsync();
            if (user == null) return null;

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<TicketModlel, TicketModel>());
            var mapper = new Mapper(configuration);

            List<TicketModel> ticketModels = new List<TicketModel>();
            var tickets = user.Tickets.ToList();

            foreach (var t in tickets)
            {
                ticketModels.Add(mapper.Map<TicketModlel, TicketModel>(t));
            }

            return ticketModels;
        }
    }
}
