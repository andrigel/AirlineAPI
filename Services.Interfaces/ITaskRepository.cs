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
        public Task<TicketModel> ReserveTicket(string userId, Guid flightId, TicketClass ticketClass = TicketClass.econom,int PremiumMarksUsedCount = 0);
        public Task TryReturnTicket(Guid ticketId);
        public Task<int> GetKilometersInAir(string userId);
        public List<FlightModel> GetAdvice(string from, string to);
        public List<FlightModel> GetValidFlights();
    }
}
