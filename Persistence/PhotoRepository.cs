using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carzz.Core;
using carzz.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace carzz.Persistence
{
    public class PhotoRepository:IPhotoRepository
    {
        private readonly CarzzDbContext _context;

        public PhotoRepository(CarzzDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await _context.Photos.Where(p => p.VehicleId == vehicleId).ToListAsync();
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _context.Photos.SingleOrDefaultAsync(p => p.Id == id);
        }

        public void RemovePhoto(Photo photo)
        {
            _context.Photos.Remove(photo);
        }
    }
}
