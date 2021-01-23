using OlaFFProj.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OlaFFProj.Data
{
    public static class Repository
    {
        static Context GetContext() 
        {
            var context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
            return context;
        }

        public static int GetComicBookCount() 
        {
            using (Context context = GetContext()) 
            {
                return context.ComicBooks.Count();
            }
        }

        public static IList<ComicBook> GetComicBooks() 
        {
            using (Context context = GetContext()) 
            {
                return context.ComicBooks
                    .Include(cb => cb.Series)
                    .OrderBy(cb => cb.Series.Title)
                    .ThenBy(cb => cb.IssueNumber)
                    .ToList();
            }
        }
        //Returns a single comic book "comicBookId" The comic book ID to retrieve
        //A fully populated ComicBook entity instance
        public static ComicBook GetComicBook(int comicBookId) 
        {
            using (Context context = GetContext()) 
            {
                return context.ComicBooks
                    .Include(cb => cb.Series)
                    .Include(cb => cb.Artists.Select(a => a.Artist))
                    .Include(cb => cb.Artists.Select(a => a.Role))
                    .Where(cb => cb.Id == comicBookId)
                    .SingleOrDefault();
            }
        }
        //Returns a list of series ordered by title. An IList collection of Series entity instances.
        public static IList<Series> GetSeries() 
        {
            using (Context context = GetContext()) 
            {
                return context.Series
                    .OrderBy(s => s.Title)
                    .ToList();
            }
        }
        //Returns a single series "seriesId" The series ID to retrieve. A Series entity instance
        public static Series GetSeries(int seriesId) 
        {
            using (Context context = GetContext()) 
            {
                return context.Series
                    .Where(s => s.Id == seriesId)
                    .SingleOrDefault();
            }
        }
        //Returns a list of artists ordered by name. An IList collection of Artist entity instances
        public static IList<Artist> GetArtists() 
        {
            using (Context context = GetContext()) 
            {
                return context.Artists
                    .OrderBy(a => a.Name)
                    .ToList();
            }
        }
        //Returns a list of roles ordered by name. An IList collection of Role entity instances.
        public static IList<Role> GetRoles() 
        {
            using (Context context = GetContext()) 
            {
                return context.Roles
                    .OrderBy(r => r.Name)
                    .ToList();
            }
        }
        //Adds a comic book "comicBook" The ComicBook entity instance to add
        public static void AddComicBook(ComicBook comicBook) 
        {
            using (Context context = GetContext()) 
            {
                context.ComicBooks.Add(comicBook);

                if (comicBook.Series != null && comicBook.Series.Id > 0)
                {
                    context.Entry(comicBook.Series).State = EntityState.Unchanged;
                }

                foreach (ComicBookArtist artist in comicBook.Artists)
                {
                    if (artist.Artist != null && artist.Artist.Id > 0)
                    {
                        context.Entry(artist.Artist).State = EntityState.Unchanged;
                    }
                    if (artist.Role != null && artist.Role.Id > 0)
                    {
                        context.Entry(artist.Role).State = EntityState.Unchanged;
                    }
                    context.SaveChanges();
                }
            }
        }
        //Updates a comic book "comicBook" The ComicBook entity instance to update
        public static void UpdateComicBook(ComicBook comicBook) 
        {
            using (Context context = GetContext()) 
            {
                context.ComicBooks.Attach(comicBook);
                var comicBookEntry = context.Entry(comicBook);
                comicBookEntry.State = EntityState.Modified;

                context.SaveChanges();
            }
        }
        //Deletes a comic book. "comicBookId" The comic book ID to delete
        public static void DeleteComicBook(int comicBookId) 
        {
            using (Context context = GetContext()) 
            {
                var comicBook = new ComicBook() { Id = comicBookId };
                context.Entry(comicBook).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
    }
}
