using LibraryAPI.Models;
using LibraryAPI.Services;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("v1", options =>
{
    options.ShouldInclude = apiDescription => apiDescription.GroupName is null or "v1";
});
builder.Services.AddControllers();

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<BookRepository>(); //BookRepository

builder.Services.AddAutoMapper(cfg => { }, typeof(Program));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    context.Database.EnsureCreated();
}


app.UseHttpsRedirection();
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.AddDocuments(["v1"]);
    options.WithTitle("Library API");
});
app.MapControllers();
app.Run();