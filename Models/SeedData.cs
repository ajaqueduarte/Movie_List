using Microsoft.EntityFrameworkCore;
using RazorPagesMovies.Data;
using System;

namespace RazorPagesMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new RazorPagesMoviesContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<RazorPagesMoviesContext>>()))
        {
            if (context == null || context.Movie == null)
            {
                throw new ArgumentNullException("Null RazorPagesMovieContext");
            }

            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                   
                    Rating = 3.5,
                    ImageUrl = "/images/Harry_met_Sally.jpg"

                },

                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                   
                    Rating = 2.5,
                    ImageUrl = "/images/Ghostbusters.jpg"
                },

                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                   
                    Rating = 6.2,
                    ImageUrl = "/images/Ghostbusters2.jpg"
                },

                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    
                    Rating = 6.2,
                    ImageUrl = "/images/Rio_Bravo.jpg"
                }
            );
            context.SaveChanges();
        }
    }
}