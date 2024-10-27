using Microsoft.AspNetCore.Mvc;
using PhotoService.Interfaces;
using Microsoft.AspNetCore.Http;
using PhotoService.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
using System.Text;





namespace PhotoService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            try
            {
                var uploadedPhoto = await _photoService.UploadPhotoAsync(file);
                return Ok(uploadedPhoto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading photo: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photo = await _photoService.GetPhotoAsync(id);
            return photo == null ? NotFound() : Ok(photo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhotos()
        {
            var photos = await _photoService.GetAllPhotosAsync();
            return Ok(photos);
        }
    }
}












//using Microsoft.AspNetCore.Mvc;
//using PhotoService.Interfaces;
//using PhotoService.Model;
//using Microsoft.AspNetCore.Http;

//namespace PhotoService.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class PhotoController : ControllerBase
//    {
//        private readonly IPhotoService _photoService;

//        public PhotoController(IPhotoService photoService)
//        {
//            _photoService = photoService;
//        }


//        [HttpGet]
//        public async Task<IActionResult> GetAllPhotos()
//        {
//            var photos = await _photoService.GetAllPhotosAsync();
//            return Ok(photos);
//        }


//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetPhotoById(int id)
//        {
//            var photo = await _photoService.GetPhotoByIdAsync(id);
//            if (photo == null)
//                return NotFound();



//            return File(photo.ContentType, photo.ContentType); // Return image data directly!!!


//        }

//        [HttpPost("upload")] // !!! Separate endpoint for upload
//        public async Task<IActionResult> UploadPhoto(IFormFile file)
//        {
//            var photo = await _photoService.UploadPhotoAsync(file);
//            if (photo == null)
//                return BadRequest("Invalid file.");

//            return CreatedAtAction(nameof(GetPhotoById), new { id = photo.Id }, photo);
//        }



//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePhoto(int id)
//        {
//            var deleted = await _photoService.DeletePhotoAsync(id);
//            if (!deleted)
//                return NotFound();

//            return NoContent();
//        }
//    }
//}
