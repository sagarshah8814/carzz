using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using carzz.Controllers.Resources;
using carzz.Core;
using carzz.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace carzz.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoRepository _photoRepository;
        private readonly PhotoSetting _photoSetting;

        public PhotosController(IHostingEnvironment host,IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSetting> options, IPhotoRepository photoRepository)
        {

            _host = host;
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoRepository = photoRepository;
            _photoSetting = options.Value;
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos = await _photoRepository.GetPhotos(vehicleId);
            
            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await _vehicleRepository.GetVehicle(vehicleId, includeRelated: false);
            if (vehicle == null)
            {
                return NotFound();
            }
            if (file == null) return BadRequest("Null File");
            if (file.Length == 0) return BadRequest("Empty File");
            if (file.Length > _photoSetting.Max_Size) return BadRequest("Max Size Exceeded.");
            if (!_photoSetting.IsAccepted(file.FileName)) return BadRequest("File Type Not Supported");
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolderPath, fileName);
                using (var stream=new FileStream(filePath,FileMode.Create))
                {
                   await file.CopyToAsync(stream);
                }
                var photo=new Photo{FileName = fileName};
                vehicle.Photos.Add(photo);
                await _unitOfWork.Complete();
            return Ok(_mapper.Map<Photo,PhotoResource>(photo));
        }

        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeleteVehicle(int photoId)
        {
            var photo = await _photoRepository.GetPhoto(photoId);
            if (photo == null) return NotFound();
            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadFolderPath, photo.FileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _photoRepository.RemovePhoto(photo);
            await _unitOfWork.Complete();

            return Ok(photoId);
        }
    }
}
