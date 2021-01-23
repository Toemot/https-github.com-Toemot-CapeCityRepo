using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_ComicBook.Data
{
    public class Repository
    {
        public static Context GetContext() 
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

        public static IList<Series> GetSeries() 
        {
            using (Context context = GetContext()) 
            {
                return context.Series
                    .OrderBy(cb => cb.Title)
                    .ToList();
            }
        }

        public static Series GetSeries(int seriesId) 
        {
            using (Context context = GetContext()) 
            {
                return context.Series
                    .Where(s => s.Id == seriesId)
                    .SingleOrDefault();
            }
        }

        public static IList<Artist> GetArtists() 
        {
            using (Context context = GetContext()) 
            {
                return context.Artists
                    .OrderBy(a => a.Name)
                    .ToList();
            }
        }

        public static IList<Role> GetRoles() 
        {
            using (Context context = GetContext()) 
            {
                return context.Roles
                    .OrderBy(r => r.Name)
                    .ToList();
            }
        }

        public static void AddComicBook(ComicBook comicBook) 
        {
            using (Context context = GetContext()) 
            {
                context.ComicBooks.Add(comicBook);

                if (comicBook.Series != null && comicBook.Series.Id > 0)
                {
                    context.Entry(comicBook).State = EntityState.Unchanged;
                }

                foreach (ComicBookArtist artist in comicBook.Artists)
                {
                    if (artist.Artist != null && artist.Id > 0)
                    {
                        context.Entry(artist).State = EntityState.Unchanged;
                    }
                    if (artist.Role != null && artist.Id > 0)
                    {
                        context.Entry(artist).State = EntityState.Unchanged;
                    }
                }
                context.SaveChanges();
            }
        }

        public static void UpdateComicBook(ComicBook comicBook) 
        {
            using (Context context = GetContext()) 
            {
                var comicBookEntry = context.Entry(comicBook);
                comicBookEntry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

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
