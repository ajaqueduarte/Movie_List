using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovies.Data;
using RazorPagesMovie.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RazorPagesMoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RazorPagesMoviesContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesMoviesContext' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Add this line to redirect the root URL to /Movies/Index
app.MapGet("/", context => {
    context.Response.Redirect("/Movies/Index");
    return Task.CompletedTask;
});

app.Run();
