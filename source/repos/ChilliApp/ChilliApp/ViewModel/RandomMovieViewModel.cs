using ChilliApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChilliApp.ViewModel
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customer { get; set; }
    }
}