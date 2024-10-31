using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoService.Interfaces;
using PhotoService.Model;
using PhotoService.Services;
using System;

namespace Biblioteka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
        private readonly IPhotoInterface _photoInterface;
        private readonly string _uploadPath;

        public PhotoController(IPhotoInterface photoInterface)
        {
            _photoInterface = photoInterface;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Img");

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            return (IActionResult)await _photoInterface.UploadPhoto(file);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Photo>> GetPhotos()
        {
            return _photoInterface.GetPhotos();
        }

        [HttpGet("{id}")]
        public ActionResult<Photo> GetPhoto(int id)
        {
            return _photoInterface.GetPhoto(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhoto(int id, [FromBody] Photo updatedPhoto)
        {
            return await _photoInterface.UpdatePhoto(id, updatedPhoto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            return await _photoInterface.DeletePhoto(id);
        }
    }
}
