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
        private EFDBContext _context;
        public EFFlightRepository(EFDBContext context)
        {
            this._context = context;
        }

        public async Task<List<FlightModel>> GetAll()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<List<Flight>, List<FlightModel>>());
            var mapper = new Mapper(configuration);

            var flights = await _context.Flights.ToListAsync();
            return mapper.Map<List<Flight>, List<FlightModel>>(flights);
        }
    }
}
