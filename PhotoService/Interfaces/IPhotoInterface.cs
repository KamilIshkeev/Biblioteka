using Microsoft.AspNetCore.Mvc;
using PhotoService.Model;

namespace PhotoService.Interfaces
{
    public interface IPhotoInterface
    {
        Task<IActionResult> UploadPhoto(/*[FromForm]*/ IFormFile file);
        ActionResult<IEnumerable<Photo>> GetPhotos();
        ActionResult<Photo> GetPhoto(int id);
        Task<IActionResult> UpdatePhoto(int id, [FromBody] Photo updatedPhoto);
        Task<IActionResult> DeletePhoto(int id);
    }
}
