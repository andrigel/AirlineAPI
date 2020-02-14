using AutoMapper;
using AutoMapper.Models;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EFFlightRepository : IFlightRepository
    {
        private readonly EFDBContext _context;
        public EFFlightRepository(EFDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFlight(FlightModel flightModel)
        {
            var flight = new Flight
            {
                From = flightModel.From,
                To = flightModel.To,
                Length = flightModel.Length,
                PlacesCount = flightModel.PlacesCount,
                PlacesReserved = flightModel.PlacesReserved,
                Price = flightModel.Price,
                Start = flightModel.Start
            };

            try
            {
                await _context.Flights.AddAsync(flight);
                await _context.SaveChangesAsync();
                return true;
            }

            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteFlight(int flightId)
        {
            try
            {
                var flight = await _context.Flights.FindAsync(flightId);
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<FlightModel>> GetAll()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Flight, FlightModel>());
            var mapper = new Mapper(configuration);

            var flights = await _context.Flights.ToListAsync();

            List<FlightModel> flightModels = new List<FlightModel>();
            foreach (var f in flights)
            {
                flightModels.Add(mapper.Map<Flight, FlightModel>(f));
            }
            return flightModels;
        }

        public async Task<bool> ModifyFlight(FlightModel flightModel)
        {
            try
            {
                var flight = await _context.Flights.FindAsync(flightModel.Id);
                if (flight == null) return false;

                flight.From = flightModel.From;
                flight.Length = flightModel.Length;
                flight.PlacesCount = flightModel.PlacesCount;
                flight.PlacesReserved = flightModel.PlacesReserved;
                flight.Price = flightModel.Price;
                flight.Start = flightModel.Start;
                flight.To = flightModel.To;

                _context.Entry(flight).State = EntityState.Modified;
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
