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
        public ValueTask<Ticket> ReserveTicket(string userId, int flightId, TicketClass ticketClass = TicketClass.econom,int PremiumMarksUsedCount = 0, bool save = true);
        public ValueTask<bool> TryReturnTicket(int ticketId);
        public ValueTask<int> GetKilometersInAir(string userId);
    }
}
