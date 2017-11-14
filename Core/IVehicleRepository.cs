using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carzz.Core.Models;

namespace carzz.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated);
        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> GetVehicles(Filter filter);
        Task<IEnumerable<Vehicle>> GetSoldVehicles();
    }
}
