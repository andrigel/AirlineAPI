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
    public class EFTaskRepository : ITaskRepository
    {
        private EFDBContext _context;
        public EFTaskRepository(EFDBContext context)
        {
            this._context = context;
        }

        public List<FlightModel> GetFlightsByDate(DateTime from, DateTime to)
        {
            var flights = _context.Flights.Where(f => ((f.Start > from) && (f.Start < to))).ToList();
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Flight, FlightModel>());
            var mapper = new Mapper(configuration);
            List<FlightModel> flightsModels = new List<FlightModel>();
            foreach(var f in flights)
            {
                flightsModels.Add(mapper.Map<Flight, FlightModel>(f));
            }
            return flightsModels;
        }

        public List<FlightModel> GetFlightsToday()
        {
            var from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var to = from.AddDays(1);
            return GetFlightsByDate(from,to);
        }

        public async ValueTask<int> GetKilometersInAir(string userId)
        {
            int result = 0;
            var user = await _context.ApplicationsUsers.Where(u => u.Id == userId).Include(u => u.Tickets).FirstOrDefaultAsync();
            if ((user == null) || (user.Tickets.Count == 0)) return result;

            foreach (var t in user.Tickets)
            {
                await _context.Entry(t).Reference(t => t.Flight).LoadAsync();
            }

            foreach (var t in user.Tickets)
            {
                result += t.Flight.Length;
            }
            return result;
        }

        public async ValueTask<Ticket> ReserveTicket(string userId, int flightId, TicketClass ticketClass = TicketClass.econom, int premiumMarksUsedCount = 0, bool save = true)
        {
            var user = await _context.ApplicationsUsers.FindAsync(userId);
            var flight = await _context.Flights.FindAsync(flightId);
            if ((user == null) || (flight == null)) return null;

            int endPrice = flight.Price;
            if (ticketClass == TicketClass.vip) endPrice = (int)(endPrice * 1.5f);

            if ( (user.PremiumMarksCount < premiumMarksUsedCount)||(premiumMarksUsedCount > endPrice) ) return null;
            try
            {             
                endPrice -= premiumMarksUsedCount;
                user.PremiumMarksCount -= premiumMarksUsedCount;
                var ticket = new Ticket { Flight = flight, User = user, PremiumMarksUsedCount = premiumMarksUsedCount, ticketClass = ticketClass, EndPrice = endPrice, IsBought = false };
                if (!save) return ticket;
                var DBticket = await _context.Tickets.AddAsync(ticket);

                await _context.Entry(DBticket.Entity).Reference(t => t.Flight).LoadAsync();
                DBticket.Entity.Flight.PlacesReserved += 1;
                await _context.SaveChangesAsync();
                return ticket;
            }
            catch
            {
                return null;
            }
        }

        public async ValueTask<bool> TryReturnTicket(int ticketId)
        {
            try
            {
                DateTime date = DateTime.Now;
                var date2 = date.AddDays(3);
                var ticket = _context.Tickets.Find(ticketId);
                if (ticket == null) return false;
                await _context.Entry(ticket).Reference(t => t.Flight).LoadAsync();
                await _context.Entry(ticket).Reference(t => t.User).LoadAsync();
                if (ticket.Flight.Start < date2) return false;
                ticket.User.PremiumMarksCount += ticket.PremiumMarksUsedCount;
                ticket.Flight.PlacesReserved -= 1;
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
