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
        public Task AddMark(string userId, Guid flightId);
        public Task AddMark(ApplicationUser user, Guid flightId);
        public Task AddMarksMany(string userId, List<Guid> flightIds);
        public Task DeleteMark(Guid markId);
        public void DeleteMarksMany(List<Guid> markIds);
        public List<FlightModel> GetMarksFromUser(string userId);
    }
}
