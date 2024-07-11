using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using RazorPagesMovie.Models;
using RazorPagesMovies.Data;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace RazorPagesMovies.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMoviesContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(RazorPagesMoviesContext context, IWebHostEnvironment environment, ILogger<CreateModel> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; }

        public IActionResult OnGet()
        {
            _logger.LogInformation("OnGet method called.");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("OnPostAsync started.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is not valid.");
                return Page();
            }

            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }

                Movie.ImageUrl = "/images/" + fileName;
                _logger.LogInformation("Image uploaded successfully: {ImageUrl}", Movie.ImageUrl);
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Movie created successfully: {MovieTitle}", Movie.Title);
            return RedirectToPage("./Index");
        }
    }
}
