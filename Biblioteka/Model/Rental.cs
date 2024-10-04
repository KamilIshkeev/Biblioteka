using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Biblioteka.Model
{
    public class Rental
    {
        [Key]
        public int Id_Rental { get; set; }


        //[ForeignKey("Reader")]
        public int? ReaderId { get; set; }
        //public Reader Reader { get; set; }

        //[ForeignKey("Book")]
        public int? BookId { get; set; }
        //public Book Book { get; set; }
        public DateOnly RentalDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public bool Returned { get; internal set; }
    }
}
