using Biblioteka.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoService.Interfaces;

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
                _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "img");

                if (!Directory.Exists(_uploadPath))
                {
                    Directory.CreateDirectory(_uploadPath);
                }
            }

            [HttpPost("upload")]
            public async Task<IActionResult> UploadPhoto(IFormFile file)
            {
            return null; /*(IActionResult)await _photoInterface.UploadPhoto(file);*/
            }

            [HttpGet]
            public ActionResult<IEnumerable<Photo>> GetPhotos()
            {
            return null; /*_photoInterface.GetPhotos();*/
            }

            [HttpGet("{id}")]
            public ActionResult<Photo> GetPhoto(int id)
            {
            return null; /*_photoInterface.GetPhoto(id);*/
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdatePhoto(int id, [FromBody] Photo updatedPhoto)
            {
            return null; /*await _photoInterface.UpdatePhoto(id, updatedPhoto);*/
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePhoto(int id)
            {
            return null; /*await _photoInterface.DeletePhoto(id);*/
            }
        }
    }
