using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
        {
            // Look for any movies.
            if (!context.Movie.Any())
            {
                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Rating = "R",
                        Price = 7.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Rating = "PG",
                        Price = 8.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Rating = "PG-13",
                        Price = 9.99M
                    },
                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Rating = "G",
                        Price = 3.99M
                    }
                );
            }

            var favoriteMovies = new[]
            {
                new Movie
                {
                    Title = "The Matrix",
                    ReleaseDate = DateTime.Parse("1999-3-31"),
                    Genre = "Science Fiction",
                    Rating = "R",
                    Price = 10.99M
                },
                new Movie
                {
                    Title = "The Lord of the Rings: The Fellowship of the Ring",
                    ReleaseDate = DateTime.Parse("2001-12-19"),
                    Genre = "Fantasy",
                    Rating = "PG-13",
                    Price = 12.99M
                },
                new Movie
                {
                    Title = "Spider-Man: Into the Spider-Verse",
                    ReleaseDate = DateTime.Parse("2018-12-14"),
                    Genre = "Animation",
                    Rating = "PG",
                    Price = 11.99M
                }
            };

            foreach (var movie in favoriteMovies)
            {
                if (!context.Movie.Any(existingMovie => existingMovie.Title == movie.Title))
                {
                    context.Movie.Add(movie);
                }
            }

            context.SaveChanges();
        }
    }
}