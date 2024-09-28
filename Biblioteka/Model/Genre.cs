using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Biblioteka.Model
{
    public class Genre
    {
        [Key]
        public int Id_Genre { get; set; }
        public string? Name { get; set; }
       
    }
}
