using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace RazorPagesMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorPagesMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovieContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Book = "2 Nephi 33:12",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Scripture = " And I pray the Father in the name of Christ that many of us, if not all, may be saved in his kingdom at that great and last day",
                
                    }

                    
                );
                context.SaveChanges();
            }
        }
    }
}
