using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using carzz.Controllers.Resources;
using carzz.Core;
using carzz.Core.Models;
using carzz.Persistence;
using Microsoft.EntityFrameworkCore;

namespace carzz.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController:Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VehiclesController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
            vehicle.LastUpdate=DateTime.Now;
             _vehicleRepository.AddVehicle(vehicle);
            await _unitOfWork.Complete();
            vehicle = await _vehicleRepository.GetVehicle(vehicle.Id, true);
            var result = _mapper.Map<Vehicle,VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id,[FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = await _vehicleRepository.GetVehicle(id,true);
            if (vehicle == null)
            {
                return NotFound();
            }
            _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource, vehicle);
            vehicle.LastUpdate=DateTime.Now;
            await _unitOfWork.Complete();
            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id, true);
            if (vehicle == null)
            {
                return NotFound();
            }
            _vehicleRepository.RemoveVehicle(vehicle);
            await _unitOfWork.Complete();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id, true);
            if (vehicle == null)
            {
                return NotFound();
            }
            var vehicleResouce = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vehicleResouce);
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleResource>> GetVehicles(FilterResource filterResource)
        {
            var filter = _mapper.Map<FilterResource, Filter>(filterResource);
            var vehicle = await _vehicleRepository.GetVehicles(filter);
            return _mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicle);
        }
    }
}
