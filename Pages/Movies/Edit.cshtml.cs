using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RazorPagesMovie.Models;
using RazorPagesMovies.Data;

namespace RazorPagesMovies.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesMoviesContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<EditModel> _logger;

        public EditModel(RazorPagesMoviesContext context, IWebHostEnvironment environment, ILogger<EditModel> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var movieToUpdate = await _context.Movie.FindAsync(Movie.Id);

            if (movieToUpdate == null)
            {
                return NotFound();
            }

            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }

                movieToUpdate.ImageUrl = "/images/" + fileName;
            }

            movieToUpdate.Title = Movie.Title;
            movieToUpdate.ReleaseDate = Movie.ReleaseDate;
            movieToUpdate.Genre = Movie.Genre;
            movieToUpdate.Rating = Movie.Rating;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
