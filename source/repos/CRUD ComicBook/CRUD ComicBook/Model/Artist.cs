using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_ComicBook
{
    public class Artist
    {
        public Artist()
        {
            ComicBookArtists = new List<ComicBookArtist>();
        }

        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public ICollection<ComicBookArtist> ComicBookArtists { get; set; }
    }
}
