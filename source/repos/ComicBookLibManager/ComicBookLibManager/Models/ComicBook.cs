using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookLibManager.Models
{
    public class ComicBook
    {
        public ComicBook()
        {
            ComicBookArtists = new List<ComicBookArtist>();
        }

        public int Id { get; set; }
        public Series Series { get; set; }
        public int SeriesId { get; set; }
        public int IssueNumber { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public decimal? AverageRating { get; set; }

        public string DisplayText { 
            get
            {
                return $"{Series?.Title} # {IssueNumber}";
            }
        }

        public ICollection<ComicBookArtist> ComicBookArtists { get; set; }

        public void AddArtist(Artist artist, Role role) 
        {
            ComicBookArtists.Add(new ComicBookArtist 
            {
                Artist = artist,
                Role = role
            });
        }
    }
}
