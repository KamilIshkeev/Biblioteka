using Microsoft.AspNetCore.Http;
using PhotoService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoService.Interfaces
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhotoAsync(IFormFile file);
        Task<Photo> GetPhotoAsync(int id);
        Task<IEnumerable<Photo>> GetAllPhotosAsync();
    }
}



//using PhotoService.Model;
//using Microsoft.AspNetCore.Http;
//using PhotoService.Model;

//namespace PhotoService.Interfaces
//{
//    public interface IPhotoService
//    {
//        Task<IEnumerable<Photo>> GetAllPhotosAsync();
//        Task<Photo> GetPhotoByIdAsync(int id);
//        Task<Photo> UploadPhotoAsync(IFormFile file); // !!!
//        Task<bool> DeletePhotoAsync(int id);
//    }
//}