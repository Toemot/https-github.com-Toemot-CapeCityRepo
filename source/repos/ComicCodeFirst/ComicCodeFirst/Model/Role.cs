using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicCodeFirst.Model
{
    public class Role
    {
        public int Id { get; set; }

        [Required, StringLength(50), Column("NoName")]
        public string Name { get; set; }
    }
}
