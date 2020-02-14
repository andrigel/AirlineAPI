using AutoMapper.Models;
using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITaskRepository
    {
        public List<FlightModel> GetFlightsToday();
        public List<FlightModel> GetFlightsByDate(DateTime from, DateTime to);
        public Task<TicketModel> ReserveTicket(string userId, int flightId, TicketClass ticketClass = TicketClass.econom,int PremiumMarksUsedCount = 0, bool save = true);
        public Task<bool> TryReturnTicket(string userId, int ticketId);
        public Task<int> GetKilometersInAir(string userId);
    }
}
