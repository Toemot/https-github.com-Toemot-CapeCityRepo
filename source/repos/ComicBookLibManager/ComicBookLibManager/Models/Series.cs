using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookLibManager.Models
{
    public class Series
    {
        public Series()
        {
            ComicBooks = new List<ComicBook>();
        }

        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<ComicBook> ComicBooks { get; set; }
    }
}
