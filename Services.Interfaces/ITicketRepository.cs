using AutoMapper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITicketRepository
    {
        public Task<List<TicketModel>> GetTicketsFromUser(string userId);
    }
}
