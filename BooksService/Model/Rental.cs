using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BooksService.Model
{
    public class Rental
    {
        [Key]
        public int id_Rent { get; set; }
        public DateTime Rental_Start { get; set; }
        public int Rental_Time { get; set; }


        [ForeignKey(nameof(Reader))]
        public int? Id_Reader { get; set; }
        public Reader? Reader { get; set; }

        [ForeignKey(nameof(Book))]
        public int? Id_Book { get; set; }
        public Book? Book { get; set; }
        public DateTime Rental_End { get; set; }
        public string Rental_Status { get; set; }


    }
}
