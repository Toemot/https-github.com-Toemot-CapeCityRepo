using StoryBook.Data;
using StoryBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoryBook.Controllers
{
    public class ComicController : Controller
    {
        private ComicBookRepository _repository;

        public ComicController()
        {
            _repository = new ComicBookRepository();
        }
        // GET: Comic
        public ActionResult Index()
        {
            var comicBooks = _repository.GetComicBooks();

            return View(comicBooks);
        }

        public ActionResult Detail(int? id) 
        {
            var comicBook = _repository.GetComicBook(id.Value);

            return View(comicBook);
        }
    }
}