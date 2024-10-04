using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Biblioteka.Model
{
    public class Reader
    {
        [Key]
        public int Id_Reader { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string ContactDetails { get; set; }
        
       

    }
}
