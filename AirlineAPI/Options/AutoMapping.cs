using AutoMapper;
using AutoMapper.Models;
using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPI.Options
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Flight, FlightModel>();
            CreateMap<Ticket, TicketModel>();
            CreateMap<ApplicationUser, UserModel>();
            CreateMap<FlightModel, Flight>().ForMember(f => f.Id, opt => opt.Ignore());
        }
    }
}
