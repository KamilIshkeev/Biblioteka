using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PhotoService.Model
{
    public class PhotoUploadRequest
    {
        [Required]
        public IFormFile File { get; set; }
       
    }
}
