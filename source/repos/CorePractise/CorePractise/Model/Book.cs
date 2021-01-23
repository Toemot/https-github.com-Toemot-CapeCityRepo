using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorePractise.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display]
        public string Name { get; set; }

        public string Author { get; set; }
    }
}
