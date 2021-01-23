﻿using ComicCodeFirst.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;

namespace ComicCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context()) 
            {
                context.Database.Log = (message) => Debug.WriteLine(message);

                var comicBooks = context.ComicBooks
                    .Include(cb => cb.Series)
                    .Where(cb => cb.IssueNumber == 1)
                    .ToList();

                foreach (var comicBook in comicBooks)
                {
                    Console.WriteLine(comicBook.DisplayText);
                }

                Console.WriteLine("# of comic books {0}", comicBooks.Count);

                //var comicBooks = context.ComicBooks
                //    .Include(cb => cb.Series)
                //    .Include(cb => cb.Artists.Select(a => a.Artist))
                //    .Include(cb => cb.Artists.Select(a => a.Role))
                //    .ToList();

                //foreach (var comicBook in comicBooks)
                //{
                //    var artistRoleNames = comicBook.Artists.Select(a => $"{a.Artist.Name}- {a.Role.Name}").ToList();
                //    var artistRoleDisplayText = string.Join(", ", artistRoleNames);

                //    Console.WriteLine(comicBook.DisplayText);
                //    Console.WriteLine(artistRoleDisplayText);
                //}
                Console.ReadLine();
            };
        }
    }
}
