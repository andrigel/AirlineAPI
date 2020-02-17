using AutoMapper.Models;
using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMarkRepository
    {
        public Task<bool> AddMark(string userId, Guid flightId, ApplicationUser readyUser = null);
        public Task<int> AddMarksMany(string userId, List<Guid> flightIds);
        public Task<bool> DeleteMark(string userId, Guid flightId, ApplicationUser readyUser = null);
        public Task<int> DeleteMarksMany(string userId, List<Guid> flightIds);
        public Task<List<FlightModel>> GetMarksFromUser(string userId);
    }
}
