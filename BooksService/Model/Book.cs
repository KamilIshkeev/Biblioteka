﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BooksService.Model
{
    public class Book
    {
        [Key]
        public int Id_Book { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        [Required]
        [ForeignKey(nameof(Genre))]
        public int? GenreID { get; set; }
        public Genre? Genre { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int AvailableCopies { get; set; }

    }
}
