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
        private readonly IMapper _mapper;
        public EFTicketRepository(EFDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<TicketModel> GetTicketsFromUser(string userId)
        {
            var tickets = _context.Tickets.Where(t => t.UserId == userId).AsEnumerable();
            return tickets.Select(t => _mapper.Map<TicketModel>(t)).ToList();
        }
    }
}
