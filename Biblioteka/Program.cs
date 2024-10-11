using Biblioteka.DatabContext;
using Biblioteka.Interfaces;
using Biblioteka.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddDbContext<BiblioApiDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioDbString")), ServiceLifetime.Scoped);


//builder.Services.AddDbContext<BiblioApiDB>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioApiDB"))); //Or your DB connection string

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
