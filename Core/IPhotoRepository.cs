using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carzz.Core.Models;

namespace carzz.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
        Task<Photo> GetPhoto(int id);
        void RemovePhoto(Photo photo);
    }
}
