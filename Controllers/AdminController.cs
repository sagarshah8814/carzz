using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using carzz.Controllers.Resources;
using carzz.Core;
using carzz.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace carzz.Controllers
{
    [Route("/api/admin")]
    public class AdminController:Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IMapper mapper,IVehicleRepository vehicleRepository,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<VehicleResource>> GetSoldVehicles()
        {
            var vehicle = await _vehicleRepository.GetSoldVehicles();
           return _mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicle);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateVehicleStatus(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id,false);
            if (vehicle == null)
            {
                return NotFound();
            }
            vehicle.Status = "Sold";
            vehicle.LastUpdate=DateTime.Now;
            await _unitOfWork.Complete();
            var result=_mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpGet("chart")]
        [Authorize]
        public async Task<IActionResult> GetVehiclesForChart()
        {
            var vehicle = await _vehicleRepository.GetSoldVehicles();
            var count = vehicle.GroupBy(v => v.Model.Make.Name).Select(v => new {Name = v.Key, Number = v.Count()});
            return Ok(count);
        }
    }
}
