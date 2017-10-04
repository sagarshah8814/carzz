using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using carzz.Core;
using carzz.Core.Models;
using carzz.Extensions;
using Microsoft.EntityFrameworkCore;

namespace carzz.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly CarzzDbContext _context;

        public VehicleRepository(CarzzDbContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await _context.Vehicles.FindAsync(id);
            }
            return await _context.Vehicles.Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model).ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v=>v.Id==id);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles(Filter filter)
        {
            var query = _context.Vehicles.Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make).AsQueryable();

            if (filter.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.Make.Id == filter.MakeId.Value);
            }
            if (filter.ModelId.HasValue)
            {
                query = query.Where(v => v.ModelId == filter.ModelId.Value);
            }
            if (filter.Year.HasValue)
            {
                query = query.Where(v => v.Year == filter.Year.Value);
            }
            
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v=>v.Model.Make.Name,
                ["model"] = v=>v.Model.Name,
                ["contactName"] = v=>v.ContactName,
                ["year"] = v=>v.Year
            };

            query = query.ApplyOrdering(filter, columnsMap);
            return await query.ToListAsync();
        }
            
        public void AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
        }
    }
}
