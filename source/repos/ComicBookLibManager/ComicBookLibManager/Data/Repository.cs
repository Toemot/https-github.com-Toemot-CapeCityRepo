using ComicBookLibManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookLibManager.Data
{
    public static class Repository
    {
        static Context GetContext() 
        {
            Context context = new Context();
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

        public static List<ComicBook> GetComicBooks() 
        {
            using(Context context = GetContext()) 
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
            using(Context context = GetContext()) 
            {
                return context.ComicBooks
                    .Include(cb => cb.Series)
                    .Include(cb => cb.ComicBookArtists.Select(a => a.Artist))
                    .Include(cb => cb.ComicBookArtists.Select(r => r.Role))
                    .Where(cb => cb.Id == comicBookId)
                    .SingleOrDefault();
            }
        }

        public static List<Series> GetSeries() 
        {
            using(Context context = GetContext()) 
            {
                return context.Series
                    .OrderBy(s => s.Title)
                    .ToList();
            }
        }

        public static Series GetSeries(int seriesId) 
        {
            using(Context context = GetContext())
            {
                return context.Series
                    .Where(s => s.Id == seriesId)
                    .SingleOrDefault();
            }
        }

        public static List<Artist> GetArtists() 
        {
            using(Context context = GetContext()) 
            {
                return context.Artists
                    .OrderBy(a => a.Name)
                    .ToList();
            }
        }
        
        public static List<Role> GetRoles() 
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

                if(comicBook.Series != null && comicBook.SeriesId > 0) 
                {
                    context.Entry(comicBook.Series).State = EntityState.Unchanged;
                }
                foreach (ComicBookArtist artist in comicBook.ComicBookArtists)
                {
                    if (artist.Artist != null && artist.Artist.Id > 0)
                    {
                        context.Entry(artist.Artist).State = EntityState.Unchanged;
                    }
                    if (artist.Role != null && artist.Role.Id > 0)
                    {
                        context.Entry(artist.Role).State = EntityState.Unchanged;
                    }
                }
                context.SaveChanges();
            }
        }

        public static void UpdateComicBook(ComicBook comicBook) 
        {
            using(Context context = GetContext()) 
            {
                context.ComicBooks.Attach(comicBook);
                var comicBookEntry = context.Entry(comicBook);
                comicBookEntry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void DeleteComicBook(int comicBookId) 
        {
            using(Context context = GetContext()) 
            {
                var comicBook = new ComicBook() { Id = comicBookId };
                context.Entry(comicBook).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
