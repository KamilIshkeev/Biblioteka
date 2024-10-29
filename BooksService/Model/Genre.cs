using System.ComponentModel.DataAnnotations;

namespace BooksService.Model
{
    public class Genre
    {
        [Key]
        public int Id_Genre { get; set; }
        public string? Name { get; set; }

    }
}
