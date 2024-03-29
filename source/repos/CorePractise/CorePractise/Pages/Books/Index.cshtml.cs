using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorePractise.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorePractise.Pages.Books
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books =  await _context.Books.ToListAsync();
        }
    }
}
