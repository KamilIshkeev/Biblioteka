using System.ComponentModel.DataAnnotations;

namespace ReaderServ.Model
{
    public class Genre
    {
        [Key]
        public int Id_Genre { get; set; }
        public string? Name { get; set; }

    }
}
