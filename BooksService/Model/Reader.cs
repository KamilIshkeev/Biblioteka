﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BooksService.Model
{
    public class Reader
    {
        [Key]
        public int Id_Reader { get; set; }
        public string Name { get; set; }
        public DateTime Date_Birth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [ForeignKey(nameof(Role))]
        public int? Id_Role { get; set; } = 2;
        public Roles? Role { get; set; }



    }
}
