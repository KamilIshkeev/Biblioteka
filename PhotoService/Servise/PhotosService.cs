using PhotoService.Interfaces;
using PhotoService.Models;
using PhotoService.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;



namespace PhotoService.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly PhotoDbContext _context;

        public PhotoService(PhotoDbContext context)
        {
            _context = context;
        }

        public async Task<Photo> UploadPhotoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            if (file.Length > 1024 * 1024 * 10) //Example Max file size 10MB
            {
                throw new ArgumentException("File size exceeds the limit.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var photo = new Photo
                {
                    FileName = Path.GetFileName(file.FileName), // Use Path.GetFileName for security
                    ContentType = file.ContentType,
                    Data = memoryStream.ToArray(),
                    UploadDate = DateTime.UtcNow // Use UtcNow for consistency
                };

                try
                {
                    _context.Photos.Add(photo);
                    await _context.SaveChangesAsync();
                    return photo;
                }
                catch (DbUpdateException ex)
                {
                    // Handle database errors more gracefully
                    string message = $"Database error uploading photo: {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        message += $" Inner Exception: {ex.InnerException.Message}";
                    }
                    throw new Exception(message, ex); //Re-throw for controller to handle
                }
            }
        }

        public async Task<Photo> GetPhotoAsync(int id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public async Task<IEnumerable<Photo>> GetAllPhotosAsync()
        {
            return await _context.Photos.ToListAsync();
        }
    }
}








//using PhotoService.Interfaces;
//using PhotoService.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using PhotoService.Model;
//using PhotoService.Data;

//namespace PhotoService.Services
//{
//    public class PhotoServiceImplementation : IPhotoService
//    {
//        private readonly PhotoDbContext _context;

//        public PhotoServiceImplementation(PhotoDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Photo>> GetAllPhotosAsync()
//        {
//            return await _context.Photos.ToListAsync();
//        }

//        public async Task<Photo> GetPhotoByIdAsync(int id)
//        {
//            return await _context.Photos.FindAsync(id);
//        }


//        public async Task<Photo> UploadPhotoAsync(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//                return null;

//            var photo = new Photo
//            {
//                FileName = file.FileName,
//                ContentType = file.ContentType, // Store the content type (e.g., "image/jpeg")
//                UploadDate = DateTime.UtcNow
//            };

//            using (var memoryStream = new MemoryStream())
//            {
//                await file.CopyToAsync(memoryStream);
//                photo.Data = memoryStream.ToArray(); // Store the image data in a new 'Data' property
//            }

//            _context.Photos.Add(photo);
//            await _context.SaveChangesAsync();
//            return photo;
//        }

//        public async Task<bool> DeletePhotoAsync(int id)
//        {
//            var photo = await _context.Photos.FindAsync(id);
//            if (photo == null)
//                return false;

//            _context.Photos.Remove(photo);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//    }
//}