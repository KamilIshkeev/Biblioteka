using System.ComponentModel.DataAnnotations;

namespace BooksService.Model
{
    public class Roles
    {
        [Key]
        public int Id_Role { get; set; }
        public string Name { get; set; }
    }
}
