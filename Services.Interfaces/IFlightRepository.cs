﻿using AutoMapper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFlightRepository
    {
        public Task<List<FlightModel>> GetAll();
    }
}
