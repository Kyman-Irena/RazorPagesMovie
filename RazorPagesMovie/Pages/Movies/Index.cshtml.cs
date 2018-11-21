using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Models.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public PaginatedList<Movie> Movie { get; set; }
        public string SearchString { get; set; }
        public string Scripture { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder,  string currentFilter, string searchString, int? pageIndex)
         {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Movie> bookIQ = from s in _context.Movie
                                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookIQ = bookIQ.Where(s => s.Book.Contains(searchString)
                                       || s.Scripture.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    bookIQ = bookIQ.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    bookIQ = bookIQ.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    bookIQ = bookIQ.OrderByDescending(s => s.ReleaseDate);
                    break;
                default:
                    bookIQ = bookIQ.OrderBy(s => s.Book);
                    break;
            }
            int pageSize = 5;
            Movie = await PaginatedList<Movie>.CreateAsync(
        bookIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }



        // using System.Linq;
        //var movies = from m in _context.Movie
        //                 select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        movies = movies.Where(s => s.Book.Contains(searchString));
        //    }

        //    if (!String.IsNullOrEmpty(movieGenre))
        //    {
        //        movies = movies.Where(x => x.Scripture == movieGenre);
        //    }
        //    Movie = await movies.ToListAsync();
        //    SearchString = searchString;
        //}
    }
}
