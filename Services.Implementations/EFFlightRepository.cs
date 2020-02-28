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
    public class EFFlightRepository : IFlightRepository
    {
        private readonly EFDBContext _context;
        private readonly IMapper _mapper;
        public EFFlightRepository(EFDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddFlight(FlightModel flightModel)
        {
            var flight = _mapper.Map<Flight>(flightModel);

            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlight(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);
            _context.Flights.Remove(flight);

            await _context.SaveChangesAsync();
        }
        public async Task<List<FlightModel>> GetAll()
        {
            var flights = _context.Flights.AsAsyncEnumerable();

            return await (_context.Flights.Select(x => _mapper.Map<FlightModel>(x))).ToListAsync();
        }

        public async Task ModifyFlight(FlightModel flightModel)
        {
            var flight = await _context.Flights.FindAsync(flightModel.Id);
            if (flight == null)
                throw new Exception("Can't find flight with id: " + flightModel.Id.ToString());

            flight.From = flightModel.From;
            flight.Length = flightModel.Length;
            flight.PlacesCount = flightModel.PlacesCount;
            flight.PlacesReserved = flightModel.PlacesReserved;
            flight.Price = flightModel.Price;
            flight.Start = flightModel.Start;
            flight.To = flightModel.To;

            await _context.SaveChangesAsync();
        }
    }
}
