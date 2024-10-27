using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoService.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        //Optional: Add a URL field for easier access to the photo
        [MaxLength(255)]
        public string DataUrl { get; set; } // Consider using a dedicated storage service like Azure Blob Storage for large files.
    }
}


//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;



//namespace PhotoService.Model
//{
//    public class Photo
//    {
//        [Key]
//        public int Id { get; set; }
//        public string FileName { get; set; }
//        public string ContentType { get; set; }
//        public DateTime UploadDate { get; set; }
//        public byte[] Data { get; set; } // Add this line
//    }
//}
